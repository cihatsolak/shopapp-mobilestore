using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Services.Abstract;
using ShopApp.WebUI.Factories;
using ShopApp.WebUI.Models.Categories;
using ShopApp.WebUI.Models.Products;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : BaseController
    {
        #region Fields
        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryModelFactory _categoryModelFactory;
        #endregion

        #region Ctor
        public AdminController(
            IProductModelFactory productModelFactory,
            IProductService productService,
            ICategoryService categoryService,
            ICategoryModelFactory categoryModelFactory)
        {
            _categoryService = categoryService;
            _productService = productService;
            _productModelFactory = productModelFactory;
            _categoryModelFactory = categoryModelFactory;
        }
        #endregion

        #region Product Controller
        [HttpGet]
        public IActionResult ProductList()
        {
            var products = _productService.GetAllProducts();
            var productListModel = _productModelFactory.PrepareProductListModel(products);
            return View(productListModel);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = _productModelFactory.PrepareProductEntity(productModel);
                _productService.Create(product);
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
                return NotFound();

            var product = _productService.GetByIdWithCategories((int)id);

            if (product == null)
                return NotFound();

            var productModel = _productModelFactory.PrepareProductModel(product);

            ViewBag.Categories = _categoryService.GetAllCategories();

            return View(productModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel productModel, int[] categoryIds, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var product = _productModelFactory.PrepareProductEntity(productModel);

                if (file != null)
                {
                    product.ImageUrl = $"{Guid.NewGuid()}{file.FileName}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", product.ImageUrl);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                _productService.UpdateWithCategory(product, categoryIds);

                return RedirectToAction("ProductList");
            }

            ViewBag.Categories = _categoryService.GetAllCategories();

            return View(productModel);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            if (productId == 0)
                return NotFound();

            var product = _productService.GetById(productId);

            if (product == null)
                return NotFound();

            _productService.Delete(product);

            return RedirectToAction("PRoductList");
        }
        #endregion

        #region Category Controller
        public IActionResult CategoryList()
        {
            var categories = _categoryService.GetAllCategories();
            var categoryListModel = _categoryModelFactory.PrepareCategoryListModel(categories);
            return View(categoryListModel);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var category = _categoryModelFactory.PrepareCategoryEntity(categoryModel);
                _categoryService.Create(category);
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
                return NotFound();

            var category = _categoryService.GetByIdWithProducts((int)id);

            if (category == null)
                return NotFound();

            var categoryModel = _categoryModelFactory.PrepareCategoryModel(category);
            return View(categoryModel);
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var category = _categoryModelFactory.PrepareCategoryEntity(categoryModel);
                _categoryService.Update(category);
            }

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (categoryId == 0)
                return NotFound();

            var category = _categoryService.GetById(categoryId);

            if (category == null)
                return NotFound();

            _categoryService.Delete(category);

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int categoryId, int productId)
        {
            _categoryService.DeleteFromCategory(categoryId, productId);
            return RedirectToAction("EditCategory", new { id = categoryId });
        }

        #endregion
    }
}