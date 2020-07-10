using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Domain.Products;
using ShopApp.Services.Abstract;
using ShopApp.WebUI.Factories;
using ShopApp.WebUI.Models.Products;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;

        public ShopController(IProductService productService, IProductModelFactory productModelFactory)
        {
            _productService = productService;
            _productModelFactory = productModelFactory;
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            Product product = _productService.GetProductDetails((int)id);

            if (product == null)
                return NotFound();

            var productModel = _productModelFactory.PrepareProductDetailsModel(product);

            return View(productModel);
        }

        [HttpGet]
        //products/telefon?page=1
        public IActionResult List(string categoryName, int page = 1)
        {
            const int pageSize = 3;

            var products = _productService.GetProductsByCategoryName(categoryName, page, pageSize);

            PageInfo pageInfo = new PageInfo
            {
                CurrentCategory = categoryName,
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = _productService.GetCountByCategoryName(categoryName)
            };

            return View(new ProductListModel()
            {
                PageInfo = pageInfo,
                Products = products
            });
        }
    }
}