using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NTEcommerce.SharedDataModel.Product;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Exceptions;
using NTEcommerce.WebAPI.Model;
using NTEcommerce.WebAPI.Repository.Interface;
using NTEcommerce.WebAPI.Services.Interface;
using static NTEcommerce.WebAPI.Constant.MessageCode;

namespace NTEcommerce.WebAPI.Services.Implement
{
    public class ProductServices : IProductServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostEnvironment environment;
        private readonly ILogger<ProductServices> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ProductServices
            (
                IMapper mapper,
                IUnitOfWork unitOfWork,
                IHostEnvironment environment,
                ILogger<ProductServices> logger,
                IHttpContextAccessor httpContextAccessor
            )
        {
            this.mapper = mapper;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.environment = environment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductModel> CreateProduct(CreateProductModel productModel)
        {
            try
            {
                var newProduct = mapper.Map<Product>(productModel);

                if (productModel.Images != null && productModel.Images.Count != 0)
                {
                    var imageName = productModel.Images.Select(x => x.FileName).ToList();
                    var productImages = new List<ProductImage>();
                    foreach (var image in imageName)
                    {
                        productImages.Add(new ProductImage
                        {
                            Name = image,
                            Link = String.Format($"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Product/{image}")
                        });
                    }
                    newProduct.Images = productImages;
                }

                if(productModel.CategoryId != null)
                {
                    var category = await unitOfWork.Category.GetById((Guid)productModel.CategoryId);
                    if (category == null)
                        throw new BadRequestException(ErrorCode.CATEGORY_NOT_FOUNDED);
                    newProduct.Category = category;
                }    

                await unitOfWork.Product.AddAsync(newProduct);
                await unitOfWork.SaveAsync();

                if(newProduct.Images.Count != 0)
                {
                    SaveImages(productModel.Images);
                }

                var newProductModel = mapper.Map<ProductModel>(newProduct);
                return newProductModel;
            }
            catch (Exception ex)
            {
                logger.LogError($"***SOMETHING WENT WRONG IN ProductSerivces: {ex.Message}");
                throw new BadRequestException(ErrorCode.CREATE_PRODUCT_FAILED);
            }

        }

        private async Task<List<string>> SaveImages(List<IFormFile> images)
        {
            var rootPath = Path.Combine(environment.ContentRootPath, "Resource", "ProductImages");

            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            var listImageName = new List<string>();

            foreach (var image in images)
            {
                var imgPath = Path.Combine(rootPath, image.FileName);

                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                listImageName.Add(image.FileName);
            }
            return listImageName;
        }

        private List<string> GetImages(ICollection<ProductImage> productImages)
        {
            var listImage = new List<string>();
            foreach (var image in productImages)
            {
                var linkImg = String.Format($"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Product/{image.Name}");
                listImage.Add(linkImg);
            }
            return listImage;
        }

        public async Task<PagedList<Product, ProductModel>> GetList(ProductParameters parameters)
        {
            var products = unitOfWork.Product.FindAll().Include(x => x.Reviews).Include(x => x.Images).Include(x => x.Category).AsQueryable();

            if (products.Any() && !string.IsNullOrWhiteSpace(parameters.CategoryName))
                products = products.Where(p => p.Category.Name.Contains(parameters.CategoryName));

            if (products.Any() && !string.IsNullOrWhiteSpace(parameters.ProductName))
                products = products.Where(p => p.Name.Contains(parameters.ProductName));

            return PagedList<Product, ProductModel>.ToPageList(products.OrderBy(p => p.UpdatedDate),
                parameters.PageNumber,
                parameters.PageSize,
                mapper);
        }

        public async Task<ProductDetailModel> GetProduct(Guid id)
        {
            var product = await unitOfWork.Product.FindById(id);

            if (product == null)
                throw new NotFoundException(ErrorCode.PRODUCT_NOT_FOUNDED);

            var productModel = mapper.Map<ProductDetailModel>(product);
            return productModel;
        }

        public async Task<ProductDetailModel?> AddReview(Guid productId, AddProductReviewModel reviewModel)
        {
            var product = await unitOfWork.Product.FindById(productId);

            product.Reviews.Add(mapper.Map<ProductReview>(reviewModel));

            unitOfWork.Product.Update(product);
            await unitOfWork.SaveAsync();

            var productModel = mapper.Map<ProductDetailModel>(product);
            return productModel;
        }
    }
}
