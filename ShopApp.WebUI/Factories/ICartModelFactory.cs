using ShopApp.Core.Domain.Carts;
using ShopApp.WebUI.Models.Carts;

namespace ShopApp.WebUI.Factories
{
    public partial interface ICartModelFactory
    {
        CartModel PrepareCartModel(Cart cart = null, int quantity = 0);
        Cart PrepareCartEntity(string userId = null, int productId = 0, int quantity = 0);
    }
}
