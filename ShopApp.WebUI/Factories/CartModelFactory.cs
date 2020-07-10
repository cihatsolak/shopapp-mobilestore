using ShopApp.Core.Domain.Carts;
using ShopApp.Services.Abstract;
using ShopApp.WebUI.Models.Carts;
using System;
using System.Linq;

namespace ShopApp.WebUI.Factories
{
    public class CartModelFactory : ICartModelFactory
    {
        private readonly ICartService _cartService;

        public CartModelFactory(ICartService cartService)
        {
            _cartService = cartService;
        }

        public Cart PrepareCartEntity(string userId = null, int productId = 0, int quantity = 0)
        {
            if (productId == 0)
                throw new ArgumentNullException(nameof(productId));

            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var cart = _cartService.GetCardByUserId(userId);

            if (cart == null)
            {
                //Todı: after.
                return null;
            }

            //Eklenmek istenen ürün sepette var mı?
            if (cart.CartItems.Any(x => x.ProductId == productId))
            {
                //Var olan ürüne ekleme olacağı için index numarasını alıyorum.
                var index = cart.CartItems.FindIndex(x => x.ProductId == productId);
                cart.CartItems[index].Quantity += quantity;

                return cart;
            }

            cart.CartItems.Add(new CartItem
            {
                ProductId = productId,
                CartId = cart.Id,
                Quantity = quantity
            });

            return cart;
        }

        public virtual CartModel PrepareCartModel(Cart cart = null, int quantity = 0)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));

            if (quantity == 0)
                throw new ArgumentNullException(nameof(quantity));

            var cartModel = new CartModel();
            cartModel.CartId = cart.Id;

            cartModel.CartItemModels = cart.CartItems.Select(x => new CartItemModel
            {
                CartItemId = x.Id,
                ImageUrl = x.Product.ImageUrl,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                ProductPrice = (decimal)x.Product.Price,
                Quantity = x.Quantity
            }).ToList();

            return cartModel;
        } 
    }
}
