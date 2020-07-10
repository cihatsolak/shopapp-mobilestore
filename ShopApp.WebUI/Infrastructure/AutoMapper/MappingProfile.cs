using AutoMapper;
using ShopApp.Core.Domain.Categories;
using ShopApp.Core.Domain.Products;
using ShopApp.WebUI.Models.Categories;
using ShopApp.WebUI.Models.Products;

namespace ShopApp.WebUI.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();

            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
        }
    }
}
