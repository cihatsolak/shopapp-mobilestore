using ShopApp.Core.Domain.Orders;
using System.Collections.Generic;

namespace ShopApp.Services.Abstract
{
    public interface IOrderService
    {
        void Create(Order order);
        List<Order> GetOrderList(string userId);
    }
}
