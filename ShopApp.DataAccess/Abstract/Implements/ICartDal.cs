using ShopApp.Core.Domain.Carts;

namespace ShopApp.DataAccess.Abstract.Implements
{
    public interface ICartDal : IRepository<Cart>
    {
        Cart GetByUserId(string userId);

        void DeleteCartItem(int cartId, int productId);
        void ClearCart(int cartId);
    }
}
