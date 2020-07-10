using ShopApp.Core.Domain.Products;
using System.Collections.Generic;

namespace ShopApp.WebUI.Models.Categories
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
