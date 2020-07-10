using ShopApp.Core.Domain.Orders;
using System.Collections.Generic;

namespace ShopApp.DataAccess.Abstract.Implements
{
    public interface IOrderDal : IRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
