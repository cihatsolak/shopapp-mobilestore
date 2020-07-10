using ShopApp.Core.Domain.Carts;
using ShopApp.DataAccess.Abstract.Implements;
using ShopApp.Services.Abstract;
using System;

namespace ShopApp.Services.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartDal _cartDal;

        public CartService(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void Update(Cart cart)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));

            _cartDal.Update(cart);
        }

        public Cart GetByCartId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            return _cartDal.Get(x => x.UserId == userId);
        }

        public Cart GetCardByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            return _cartDal.GetByUserId(userId);
        }

        //Kullanıcı ilk kayıt olduğunda cart item oluşturmak için.
        public void InitializeCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            _cartDal.Insert(new Cart() { UserId = userId });
        }

        public void Delete(int cartId, int productId)
        {
            if (cartId == 0)
                throw new ArgumentNullException(nameof(cartId));

            if (productId == 0)
                throw new ArgumentNullException(nameof(productId));


            _cartDal.DeleteCartItem(cartId, productId);
        }

        public void ClearCart(int cartId)
        {
            if (cartId == 0)
                throw new ArgumentNullException(nameof(cartId));

            _cartDal.ClearCart(cartId);
        }
    }
}
