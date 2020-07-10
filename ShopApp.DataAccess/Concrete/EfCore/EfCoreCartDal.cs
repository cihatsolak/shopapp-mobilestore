using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Domain.Carts;
using ShopApp.DataAccess.Abstract.Implements;
using System;
using System.Linq;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCartDal : EfCoreGenericRepository<Cart, ShopContext>, ICartDal
    {
        [Obsolete("Bu metot ödeme yapıldıktan sonra kullanıcıya ait sepette ki ürünleri siler.")]
        public void ClearCart(int cartId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0";
                context.Database.ExecuteSqlCommand(cmd, cartId);
            }
        }

        [Obsolete("Alışveriş sepeti içerisinde bulunan ürünlerin id'ye göre sişinme işlemidir.")]
        public void DeleteCartItem(int cartId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0 And ProductId=@p1";
                context.Database.ExecuteSqlCommand(cmd, cartId, productId);
            }
        }

        public Cart GetByUserId(string userId)
        {
            using (var context = new ShopContext())
            {
                var query = context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product);
                return query.FirstOrDefault(x => x.UserId == userId);
            }
        }

        public override void Update(Cart entity)
        {
            //Repository içerisinde bulunan update method'u ilişkili tabloları güncellemediği için,
            //override etmek zorunda kaldık.
            using (var context = new ShopContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
