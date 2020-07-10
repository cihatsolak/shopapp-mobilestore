using ShopApp.Core.Domain.Products;
using ShopApp.WebUI.Models.Products;
using System.Collections.Generic;

namespace ShopApp.WebUI.Factories
{
    public partial interface IProductModelFactory
    {
        ProductDetailsModel PrepareProductDetailsModel(Product product = null);
        ProductListModel PrepareProductListModel(List<Product> products = null);
        Product PrepareProductEntity(ProductModel productModel = null);
        ProductModel PrepareProductModel(Product product = null);
    }
}
