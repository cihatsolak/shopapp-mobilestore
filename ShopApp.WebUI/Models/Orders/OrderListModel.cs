using ShopApp.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.WebUI.Models.Orders
{
    public class OrderListModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderState OrderState { get; set; }
        public PaymentType PaymentType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }

        public List<OrderItemModel> OrderItemModels { get; set; }

        public decimal TotalPrice()
        {
            return OrderItemModels.Sum(x => x.Price * x.Quantity);
        }
    }

    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
