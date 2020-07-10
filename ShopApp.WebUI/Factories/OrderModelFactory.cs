using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopApp.Core.Domain.Orders;
using ShopApp.DataAccess.Concrete.Memory;
using ShopApp.Services.Abstract;
using ShopApp.WebUI.Models.Carts;
using ShopApp.WebUI.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.WebUI.Factories
{
    public class OrderModelFactory : IOrderModelFactory
    {
        private const string IyziPayApiKey = "sandbox-2aUQSWZlee3TPxIGTqfjUkH4zvapP1Nf";
        private const string IyziPaySecretKey = "sandbox-XRi6OGf55YLPzNrghEd3w4O9D45IRQe2";

        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderModelFactory(ICartService cartService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartService = cartService;
        }

        public OrderModel PrepareOrderModel(string userId = null)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var cart = _cartService.GetCardByUserId(userId);

            var orderModel = new OrderModel();

            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItemModels = cart.CartItems.Select(x => new CartItemModel()
                {
                    CartItemId = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    ProductPrice = (decimal)x.Product.Price,
                    ImageUrl = x.Product.ImageUrl,
                    Quantity = x.Quantity
                }).ToList()
            };

            var defaultListItem = new SelectListItem() { Text = "Seçiniz", Value = "0" };

            orderModel.Cities.Add(defaultListItem);
            orderModel.Years.Add(defaultListItem);
            orderModel.Months.Add(defaultListItem);

            foreach (var city in FakeDatabase.Cities)
                orderModel.Cities.Add(new SelectListItem() { Text = city.Name, Value = city.Id.ToString() });

            foreach (var year in FakeDatabase.Years)
                orderModel.Years.Add(new SelectListItem() { Text = year.ToString(), Value = year.ToString() });

            foreach (var month in FakeDatabase.Months)
                orderModel.Months.Add(new SelectListItem() { Text = month.Name, Value = month.Id.ToString() });

            return orderModel;
        }

        public Tuple<CreatePaymentRequest, Options> PrepareIyzipay(OrderModel orderModel = null)
        {
            Options options = new Options();
            options.ApiKey = IyziPayApiKey;
            options.SecretKey = IyziPaySecretKey;
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = orderModel.CartModel.TotalPrice().ToString().Replace(",", ".");
            request.PaidPrice = orderModel.CartModel.TotalPrice().ToString().Replace(",", ".");
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = orderModel.CartModel.CartId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = orderModel.CartName;
            paymentCard.CardNumber = orderModel.CartNumber;
            paymentCard.ExpireMonth = orderModel.ExpirationMonthId.ToString();
            paymentCard.ExpireYear = orderModel.ExpirationYearId.ToString();
            paymentCard.Cvc = orderModel.CVV;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = orderModel.FirstName;
            buyer.Surname = orderModel.LastName;
            buyer.GsmNumber = orderModel.Phone;
            buyer.Email = orderModel.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            buyer.City = FakeDatabase.Cities.FirstOrDefault(x => x.Id == orderModel.CityId).Name;
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            foreach (var item in orderModel.CartModel.CartItemModels)
            {
                BasketItem firstBasketItem = new BasketItem();
                firstBasketItem.Id = item.ProductId.ToString();
                firstBasketItem.Name = item.ProductName;
                firstBasketItem.Category1 = "Phone";
                firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                firstBasketItem.Price = item.ProductPrice.ToString().Replace(",", ".");

                basketItems.Add(firstBasketItem);
            }

            request.BasketItems = basketItems;

            return Tuple.Create(request, options);
        }

        public Order PrepareOrderEntity(OrderModel orderModel = null, Payment payment = null, string userId = null)
        {
            if (orderModel == null)
                throw new ArgumentNullException(nameof(orderModel));

            if (payment == null)
                throw new ArgumentNullException(nameof(payment));

            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var order = new Order
            {
                OrderNumber = new Random().Next(111111, 999999).ToString(),
                OrderState = OrderState.Tamamlandı,
                PaymentType = PaymentType.KrediKartı,
                ConversationId = payment.ConversationId,
                OrderDate = new DateTime(),
                FirstName = orderModel.FirstName,
                LastName = orderModel.LastName,
                Email = orderModel.Email,
                Phone = orderModel.Phone,
                Address = orderModel.Address,
                UserId = userId
            };

            foreach (var item in orderModel.CartModel.CartItemModels)
            {
                var orderItem = new OrderItem
                {
                    Price = item.ProductPrice,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                };

                order.OrderItems.Add(orderItem);
            }

            return order;
        }

        public List<OrderListModel> PrepareListModel(List<Order> orders = null)
        {
            if (orders == null)
                throw new ArgumentNullException(nameof(orders));

            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;

            foreach (var order in orders)
            {
                orderModel = new OrderListModel();
                orderModel.OrderId = order.Id;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.OrderNote = order.OrderNote;
                orderModel.Phone = order.Phone;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.Email = order.Email;
                orderModel.Address = order.Email;
                orderModel.City = order.City;

                orderModel.OrderItemModels = order.OrderItems.Select(x => new OrderItemModel
                {
                    OrderItemId = x.Id,
                    Name = x.Product.Name,
                    Price = (decimal)x.Product.Price,
                    ImageUrl = x.Product.ImageUrl,
                    Quantity = x.Quantity
                }).ToList();

                orderListModel.Add(orderModel);
            }

            return orderListModel;
        }
    }
}