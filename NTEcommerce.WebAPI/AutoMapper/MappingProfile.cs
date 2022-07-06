using AutoMapper;
using NTEcommerce.SharedDataModel.Category;
using NTEcommerce.SharedDataModel.Product;
using NTEcommerce.WebAPI.Model;

namespace NTEcommerce.WebAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryModel, Category>();

            CreateMap<Category, CategoryModel>()
                .ForMember(d => d.CategoryParent, opt => opt.MapFrom(s => s.ParentCategory ?? null));

            CreateMap<Category, CategoryDetailModel>()
                .ForMember(d => d.Products, opt => opt.MapFrom(s => s.Products));

            CreateMap<CreateProductModel, Product>()
                .ForMember(d => d.Images, opt => opt.Ignore());

            CreateMap<Product, ProductModel>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s  => s.Category != null ? s.Category.Name : null))
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images != null ? s.Images.Select(x => x.Link) : null))
                .ForMember(d => d.AvgRating, opt => opt.MapFrom(s => s.Reviews.Count != 0 ? s.Reviews.Select(x => x.Rating).Average() : 0));
            
            CreateMap<Product, ProductDetailModel>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s  => s.Category != null ? s.Category.Name : null))
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images != null ? s.Images.Select(x => x.Link) : null))
                .ForMember(d => d.Reviews, opt => opt.MapFrom(s => s.Reviews ?? s.Reviews))
                .ForMember(d => d.AvgRating, opt => opt.MapFrom(s => s.Reviews.Count != 0 ? s.Reviews.Select(x => x.Rating).Average() : 0));

            CreateMap<ProductReview, ProductReviewModel>()
                .ForMember(d => d.Reviewer, opt => opt.MapFrom(s => s.User != null ? (s.User.FullName ?? s.User.UserName) : "anonymous"));

            CreateMap<AddProductReviewModel, ProductReview>();
        }
    }
}
