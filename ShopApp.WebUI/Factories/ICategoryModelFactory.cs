using ShopApp.Core.Domain.Categories;
using ShopApp.WebUI.Models.Categories;
using System.Collections.Generic;

namespace ShopApp.WebUI.Factories
{
    public interface ICategoryModelFactory
    {
        CategoryListViewModel PrepareCategoryListViewModel();
        CategoryListModel PrepareCategoryListModel(List<Category> categories = null);
        Category PrepareCategoryEntity(CategoryModel categoryModel = null);
        CategoryModel PrepareCategoryModel(Category category = null);
    }
}
