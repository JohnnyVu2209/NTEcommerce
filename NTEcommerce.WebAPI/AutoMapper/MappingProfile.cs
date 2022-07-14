using AutoMapper;
using NTEcommerce.SharedDataModel.Category;
using NTEcommerce.SharedDataModel.Product;
using NTEcommerce.SharedDataModel.User;
using NTEcommerce.WebAPI.Model;
using NTEcommerce.WebAPI.Model.Identity;

namespace NTEcommerce.WebAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDetailModel>();

            CreateMap<CreateCategoryModel, Category>();

            CreateMap<Category, CategorySimpleModel>();

            CreateMap<Category, CategoryModel>()
                .ForMember(d => d.CategoryParent, opt => opt.MapFrom(s => s.ParentCategory ?? null));

            CreateMap<Category, CategoryDetailModel>()
                .ForMember(d => d.Products, opt => opt.MapFrom(s => s.Products))
                .ForMember(d => d.CategoryParent, opt => opt.MapFrom(s => s.ParentCategory ?? null));


            CreateMap<Category, Category>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.TotalProducts, opt => opt.Ignore())
                .ForMember(d => d.Products, opt => opt.Ignore())
                ;

            CreateMap<CreateProductModel, Product>()
                .ForMember(d => d.Images, opt => opt.Ignore());
            
            CreateMap<UpdateProductModel, Product>()
                .ForMember(d => d.Images, opt => opt.Ignore());

            CreateMap<ProductImage, ProductImageModel>();

            CreateMap<Product, ProductModel>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s  => s.Category != null ? s.Category.Name : null))
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images != null ? s.Images.Select(x => x.Link) : null))
                .ForMember(d => d.AvgRating, opt => opt.MapFrom(s => s.Reviews.Count != 0 ? Math.Round(s.Reviews.Select(x => x.Rating).Average().Value,1) : 0));
            
            CreateMap<Product, ProductDetailModel>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s  => s.Category ?? s.Category))
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images ?? s.Images))
                .ForMember(d => d.Reviews, opt => opt.MapFrom(s => s.Reviews ?? s.Reviews))
                .ForMember(d => d.AvgRating, opt => opt.MapFrom(s => s.Reviews.Count != 0 ? s.Reviews.Select(x => x.Rating).Average() : 0));

            CreateMap<Product, Product>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Reviews, opt => opt.Ignore())
                .ForMember(d => d.Images, opt => opt.Ignore())
                ;

            CreateMap<ProductReview, ProductReviewModel>()
                .ForMember(d => d.Reviewer, opt => opt.MapFrom(s => s.User != null ? (s.User.FullName ?? s.User.UserName) : "anonymous"));

            CreateMap<AddProductReviewModel, ProductReview>();
        }
    }
}
