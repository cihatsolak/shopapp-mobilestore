using System;
using System.Collections.Generic;

namespace ShopApp.Core.Domain.Orders
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        //Order properties
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }

        //Sipariş ve Ödeme Yöntemi
        public OrderState OrderState { get; set; }
        public PaymentType PaymentType { get; set; }
        public List<OrderItem> OrderItems { get; set; }


        //Sipariş Bilgileri
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }


        //IYZICO Ödeme Yöntemi
        public string PaymentId { get; set; }
        public string PaymentToken { get; set; }
        public string ConversationId { get; set; }
    }
}