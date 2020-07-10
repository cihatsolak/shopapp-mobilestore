using ShopApp.Core.Domain.Products;
using System.Collections.Generic;

namespace ShopApp.DataAccess.Abstract.Implements
{
    /// <summary>
    /// Product data access layer
    /// </summary>
    public interface IProductDal : IRepository<Product>
    {
        List<Product> GetProductsByCategory(string category, int page, int pageSize);

        Product GetProductDetails(int id);

        int GetCountByCategoryName(string categoryName);
        Product GetByIdWithCategories(int id);
        void UpdateWithCategory(Product product, int[] categoryIds);
    }
}
