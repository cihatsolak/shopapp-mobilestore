using ShopApp.Core.Domain.Categories;
using System.Collections.Generic;

namespace ShopApp.WebUI.Models.Categories
{
    public class CategoryListViewModel
    {
        public string SelectedCategoryName { get; set; }
        public List<Category> Categories { get; set; }
    }
}
