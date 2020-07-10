using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Domain.Orders;
using ShopApp.DataAccess.Abstract.Implements;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    /// <summary>
    /// Order data access layer
    /// </summary>
    public class EfCoreOrderDal : EfCoreGenericRepository<Order, ShopContext>, IOrderDal
    {
        public List<Order> GetOrders(string userId)
        {
            using (var context = new ShopContext())
            {
                var orders = context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).AsQueryable();

                if (!string.IsNullOrEmpty(userId))
                    orders = orders.Where(x => x.UserId == userId);

                return orders.ToList();
            }
        }
    }
}
