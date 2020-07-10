using ShopApp.Core.Domain.Categories;
using ShopApp.Core.Domain.Products;
using System.Collections.Generic;

namespace ShopApp.WebUI.Models.Products
{
    public class ProductDetailsModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
