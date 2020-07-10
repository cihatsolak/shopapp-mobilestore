using ShopApp.Core.Domain.Orders;
using ShopApp.DataAccess.Abstract.Implements;
using ShopApp.Services.Abstract;
using System;
using System.Collections.Generic;

namespace ShopApp.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderService(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void Create(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _orderDal.Insert(order);
        }

        public List<Order> GetOrderList(string userId)
        {
            if (userId == null)
                userId = string.Empty;

            return _orderDal.GetOrders(userId);
        }
    }
}
