using ShopApp.Core.Domain.Categories;

namespace ShopApp.DataAccess.Abstract.Implements
{
    public interface ICategoryDal : IRepository<Category>
    {
        Category GetByIdWithProducts(int id);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
