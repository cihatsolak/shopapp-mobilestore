using ShopApp.Core.Domain.Products;
using System.Collections.Generic;

namespace ShopApp.Services.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        Product GetProductDetails(int id = 0);
        int GetCountByCategoryName(string categoryName);
        List<Product> GetAllProducts();
        List<Product> GetProductsByCategoryName(string categoryName, int page, int pageSize);
        void Create(Product product);
        void Update(Product product);
        void Delete(Product product);
        Product GetByIdWithCategories(int id);
        void UpdateWithCategory(Product product, int[] categoryIds);
    }
}
