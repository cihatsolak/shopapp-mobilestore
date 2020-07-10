using ShopApp.Core.Domain.Categories;
using System.Collections.Generic;

namespace ShopApp.Services.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Category GetById(int id);
        void Create(Category category);
        void Update(Category category);
        void Delete(Category category);
        Category GetByIdWithProducts(int id);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
