using ShopApp.Core.Domain.Categories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.Products
{
    public class ProductModel : BaseEntityModel<int>
    {
        [Display(Name = "Ürün Görseli")]
        public string ImageUrl { get; set; }

        [Display(Name = "Ürün Adı")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Ürün Açıklaması")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Ürün Fiyatı")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal Price { get; set; }

        public List<Category> SelectedCategories { get; set; }
    }
}
