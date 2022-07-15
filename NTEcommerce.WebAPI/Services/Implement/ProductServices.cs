using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Dynamic.Core;
using NTEcommerce.SharedDataModel;
using NTEcommerce.SharedDataModel.Product;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Exceptions;
using NTEcommerce.WebAPI.Model;
using NTEcommerce.WebAPI.Repository.Interface;
using NTEcommerce.WebAPI.Services.Interface;
using System.Reflection;
using System.Text;
using static NTEcommerce.WebAPI.Constant.MessageCode;
using System.Linq;
using Newtonsoft.Json;

namespace NTEcommerce.WebAPI.Services.Implement
{
    public class ProductServices : IProductServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostEnvironment environment;
        private readonly ISortHelper<Product> sortHelper;
        private readonly ILogger<ProductServices> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ProductServices
            (
                IMapper mapper,
                IUnitOfWork unitOfWork,
                IHostEnvironment environment,
                ISortHelper<Product> sortHelper,
                ILogger<ProductServices> logger,
                IHttpContextAccessor httpContextAccessor
            )
        {
            this.mapper = mapper;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.sortHelper = sortHelper;
            this.environment = environment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductModel> CreateProduct(CreateProductModel productModel)
        {
            try
            {
                var newProduct = mapper.Map<Product>(productModel);


                if (productModel.CategoryId != null)
                {
                    var category = await unitOfWork.Category.GetById((Guid)productModel.CategoryId);
                    if (category == null)
                        throw new BadRequestException(ErrorCode.CATEGORY_NOT_FOUNDED);
                    newProduct.Category = category;
                }

                if (productModel.Images != null && productModel.Images.Count != 0)
                {
                    newProduct.Images = await SaveImages(productModel.Images);
                }
                await unitOfWork.Product.AddAsync(newProduct);
                await unitOfWork.SaveAsync();

                var newProductModel = mapper.Map<ProductModel>(newProduct);
                return newProductModel;
            }
            catch (Exception ex)
            {
                logger.LogError($"***SOMETHING WENT WRONG IN ProductSerivces: {ex.Message}");
                throw new BadRequestException(ErrorCode.CREATE_PRODUCT_FAILED);
            }

        }

        private async Task<List<ProductImage>> SaveImages(List<IFormFile> images)
        {

            var rootPath = Path.Combine(environment.ContentRootPath, "Resource", "ProductImages");

            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            var listImageName = new List<string>();

            foreach (var image in images)
            {
                var myUniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                var imgPath = Path.Combine(rootPath, myUniqueFileName);

                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                listImageName.Add(myUniqueFileName);
            }
            var listProductImg = new List<ProductImage>();
            foreach (var name in listImageName)
            {
                listProductImg.Add(new ProductImage
                {
                    Name = name,
                    Link = String.Format($"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Product/{name}")
                });
            }
            return listProductImg;
        }

        private List<string> GetImages(ICollection<ProductImage> productImages)
        {
            var listImage = new List<string>();
            foreach (var image in productImages)
            {
                var linkImg = String.Format(
                    $"{httpContextAccessor.HttpContext.Request.Scheme}://" +
                    $"{httpContextAccessor.HttpContext.Request.Host}" +
                    $"{httpContextAccessor.HttpContext.Request.PathBase}/Product/{image.Name}");
                listImage.Add(linkImg);
            }
            return listImage;
        }

        public async Task<PagedList<Product, ProductModel>> GetList(ProductParameters parameters)
        {
            var products = unitOfWork.Product.FindAll().Include(x => x.Reviews).Include(x => x.Images).Include(x => x.Category).AsQueryable();

            if (products.Any() && !string.IsNullOrWhiteSpace(parameters.Name))
                products = products.Where(p => p.Name.Contains(parameters.Name) || p.Category.Name.Contains(parameters.Name));

            var sortProduct = sortHelper.ApplySort(products, parameters.OrderBy);

            var productsModel = PagedList<Product, ProductModel>.ToPageList(sortProduct,
                                                               parameters.PageNumber,
                                                               parameters.PageSize,
                                                               mapper);
            return productsModel;
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

        public async Task<ProductDetailModel?> UpdateProduct(Guid productId, UpdateProductModel updateProductModel)
        {
            var product = await unitOfWork.Product.FindById(productId);

            if (product == null)
                throw new NotFoundException(ErrorCode.PRODUCT_NOT_FOUNDED);

            var updateProduct = mapper.Map<Product>(updateProductModel);

            mapper.Map(updateProduct, product);

            if (updateProductModel.ImageLink != null)
            {
                var updateImage = JsonConvert.DeserializeObject<List<ProductImageModel>>(updateProductModel.ImageLink);
                if (updateImage.Count > 0)
                {
                    var deleteFile = FindMissing(product.Images.Select(x => x.Name).ToList(), updateImage.Select(x => x.Name).ToList());
                    if (deleteFile.Count != 0)
                    {
                        foreach (var item in deleteFile)
                        {
                            DeleteDoc(item);
                            product.Images.Remove(product.Images.Single(i => i.Name == item));
                        }
                    }
                }
            }
            else if (product.Images.Count != 0)
            {
                foreach (var item in product.Images)
                {
                    DeleteDoc(item.Name);
                    product.Images.Remove(item);
                }
            }

            if (updateProductModel.Images != null && updateProductModel.Images.Count != 0)
            {
                var image = await SaveImages(updateProductModel.Images);
                foreach (var file in image)
                {
                    product.Images.Add(file);
                }
            }

            if (updateProductModel.CategoryId != null)
            {
                var category = await unitOfWork.Category.GetById((Guid)updateProductModel.CategoryId);

                if (category == null)
                    throw new NotFoundException(ErrorCode.CATEGORY_NOT_FOUNDED);

                product.Category = category;

            }

            unitOfWork.Product.Update(product);
            await unitOfWork.SaveAsync();

            if (updateProductModel.Images != null && updateProductModel.Images.Count != 0)
            {
                await SaveImages(updateProductModel.Images);
            }

            var productModel = mapper.Map<ProductDetailModel>(product);
            return productModel;


        }
        List<string> FindMissing(List<string> a, List<string> b)
        {
            var missingList = new List<string>();
            for (int i = 0; i < a.Count; i++)
            {
                int j;

                for (j = 0; j < b.Count; j++)
                    if (a[i] == b[j])
                        break;

                if (j == b.Count)
                    missingList.Add(a[i]);
            }
            return missingList;
        }

        public void DeleteDoc(string fileName)
        {
            var filePath = Path.Combine(environment.ContentRootPath, "Resources", "ProductImages", fileName);
            if (File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

        public async Task DeleteProduct(Guid id)
        {
            var product = await unitOfWork.Product.FindById(id);

            if (product == null)
                throw new NotFoundException(ErrorCode.PRODUCT_NOT_FOUNDED);

            product.IsDeleted = true;

            unitOfWork.Product.Update(product);
            await unitOfWork.SaveAsync();
        }
    }
}
