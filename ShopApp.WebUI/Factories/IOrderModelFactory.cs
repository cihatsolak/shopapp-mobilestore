using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using ShopApp.Core.Domain.Orders;
using ShopApp.WebUI.Models.Orders;
using System;
using System.Collections.Generic;

namespace ShopApp.WebUI.Factories
{
    public interface IOrderModelFactory
    {
        OrderModel PrepareOrderModel(string userId = null);
        Tuple<CreatePaymentRequest, Options> PrepareIyzipay(OrderModel orderModel = null);
        Order PrepareOrderEntity(OrderModel orderModel = null, Payment payment = null, string userId = null);
        List<OrderListModel> PrepareListModel(List<Order> orders = null);
    }
}
