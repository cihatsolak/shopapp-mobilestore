using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Domain.Products;
using ShopApp.Core.Domain.Relationships;
using ShopApp.DataAccess.Abstract.Implements;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    /// <summary>
    /// Product data access layer
    /// </summary>
    public class EfCoreProductDal : EfCoreGenericRepository<Product, ShopContext>, IProductDal
    {
        public Product GetByIdWithCategories(int id)
        {
            using (var context = new ShopContext())
            {
                var query = context.Products.Where(x => x.Id == id);
                query = query.Include(x => x.ProductCategories).ThenInclude(x => x.Category);
                return query.FirstOrDefault();
            }
        }

        public int GetCountByCategoryName(string categoryName)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(categoryName))
                {
                    products = products.Include(x => x.ProductCategories).ThenInclude(x => x.Category);
                    products = products.Where(x => x.ProductCategories.Any(y => y.Category.Name.ToLower() == categoryName.ToLower()));
                }

                return products.Count();
            }
        }

        public Product GetProductDetails(int id)
        {
            using (var context = new ShopContext())
            {
                var query = context.Products.Where(x => x.Id == id);
                query = query.Include(x => x.ProductCategories).ThenInclude(x => x.Category);
                return query.FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Include(x => x.ProductCategories).ThenInclude(x => x.Category);
                    products = products.Where(x => x.ProductCategories.Any(y => y.Category.Name.ToLower() == category.ToLower()));
                }

                products = products.Skip((page - 1) * pageSize).Take(pageSize);

                return products.ToList();
            }
        }

        public void UpdateWithCategory(Product product, int[] categoryIds)
        {
            using (var context = new ShopContext())
            {
                var entity = context.Products.Include(x => x.ProductCategories).FirstOrDefault(x => x.Id == product.Id);

                if (entity != null)
                {
                    entity.Name = product.Name;
                    entity.Description = product.Description;
                    entity.ImageUrl = product.ImageUrl;
                    entity.Price = product.Price;

                    entity.ProductCategories = categoryIds.Select(categoryId => new ProductCategory
                    {
                        CategoryId = categoryId,
                        ProductId = product.Id
                    }).ToList();

                    context.SaveChanges();
                }
            }
        }
    }
}
