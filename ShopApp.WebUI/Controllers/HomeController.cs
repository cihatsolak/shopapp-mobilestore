using Microsoft.AspNetCore.Mvc;
using ShopApp.Services.Abstract;
using ShopApp.WebUI.Factories;

namespace ShopApp.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;

        public HomeController(IProductService productManager, IProductModelFactory productModelFactory)
        {
            _productModelFactory = productModelFactory;
            _productService = productManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            var productListModel = _productModelFactory.PrepareProductListModel(products);
            return View(productListModel);
        }
    }
}