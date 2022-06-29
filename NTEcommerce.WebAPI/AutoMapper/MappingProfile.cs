using AutoMapper;
using NTEcommerce.SharedDataModel;
using NTEcommerce.WebAPI.Model;

namespace NTEcommerce.WebAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryModel, Category>();

            CreateMap<Category, CategoryModel>()
                .ForMember(d => d.CategoryParent, opt => opt.MapFrom(s => s.ParentCategory != null ? s.ParentCategory.Name : null));
        }
    }
}
