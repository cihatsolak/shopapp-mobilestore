using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Factories;

namespace ShopApp.WebUI.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryModelFactory _categoryModelFactory;

        public CategoryListViewComponent(ICategoryModelFactory categoryModelFactory)
        {
            _categoryModelFactory = categoryModelFactory;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryModelFactory.PrepareCategoryListViewModel();
            categories.SelectedCategoryName = RouteData.Values["categoryName"]?.ToString();
            return View(categories);
        }
    }
}
