using ShopApp.Core.Domain.Carts;

namespace ShopApp.Services.Abstract
{
    public partial interface ICartService
    {
        void ClearCart(int cartId);
        void InitializeCart(string userId);
        Cart GetCardByUserId(string userId);
        Cart GetByCartId(string userId);
        void Update(Cart cart);
        void Delete(int cartId, int productId);
    }
}
