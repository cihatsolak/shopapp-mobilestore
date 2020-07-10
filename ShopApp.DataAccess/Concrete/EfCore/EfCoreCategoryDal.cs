using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Domain.Categories;
using ShopApp.DataAccess.Abstract.Implements;
using System.Linq;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    /// <summary>
    /// Category data access layer
    /// </summary>
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, ShopContext>, ICategoryDal
    {
        public void DeleteFromCategory(int categoryId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from ProductCategory where ProductId=@p0 And CategoryId=@p1";
                context.Database.ExecuteSqlCommand(cmd, productId, categoryId);
            }
        }

        public Category GetByIdWithProducts(int id)
        {
            using (var context = new ShopContext())
            {
                var query = context.Categories.Where(x => x.Id == id);

                query = query.Include(x => x.ProductCategories).ThenInclude(x => x.Product);

                return query.FirstOrDefault();
            }
        }
    }
}
