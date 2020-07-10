using IyzipayCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.DataAccess.Identity;
using ShopApp.Services.Abstract;
using ShopApp.WebUI.Factories;
using ShopApp.WebUI.Models.Carts;
using ShopApp.WebUI.Models.Orders;
using System.Linq;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartModelFactory _cartModelFactory;
        private readonly IOrderModelFactory _orderModelFactory;

        public CartController(
            ICartService cartService,
            UserManager<ApplicationUser> userManager,
            ICartModelFactory cartModelFactory,
            IOrderModelFactory orderModelFactory,
            IOrderService orderService)
        {
            _orderService = orderService;
            _orderModelFactory = orderModelFactory;
            _cartModelFactory = cartModelFactory;
            _userManager = userManager;
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            //Todo : Get user id
            string userId = _userManager.GetUserId(User);

            var cart = _cartService.GetCardByUserId(userId);
            var cartModel = _cartModelFactory.PrepareCartModel(cart, 1);

            return View(cartModel);
        }

        [HttpPost]
        public IActionResult Add(int productId, int quantity = 1)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            string userId = _userManager.GetUserId(User);
            var cart = _cartModelFactory.PrepareCartEntity(userId, productId, quantity);

            _cartService.Update(cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            string userId = _userManager.GetUserId(User);
            var cart = _cartService.GetCardByUserId(userId);

            _cartService.Delete(cart.Id, productId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CheckOut()
        {
            string userId = _userManager.GetUserId(User);
            var orderModel = _orderModelFactory.PrepareOrderModel(userId);
            return View(orderModel);
        }

        [HttpPost]
        public IActionResult CheckOut(OrderModel model)
        {
            //Iyzco ödeme yöntemi.

            if (!ModelState.IsValid)
                return View(model);

            string userId = _userManager.GetUserId(User);
            var cart = _cartService.GetCardByUserId(userId);

            model.CartModel = new CartModel()
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

            var resultPayment = _orderModelFactory.PrepareIyzipay(model);

            var payment = Payment.Create(resultPayment.Item1, resultPayment.Item2);

            if (string.Equals(payment.Status, "success"))
            {
                //save order
                var order = _orderModelFactory.PrepareOrderEntity(model, payment, userId);
                _orderService.Create(order);

                //Clear Cart
                _cartService.ClearCart(cart.Id);
                return View("Success", order.ConversationId);
            }

            return View(model);
        }
    }
}