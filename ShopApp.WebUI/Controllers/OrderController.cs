using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.DataAccess.Identity;
using ShopApp.Services.Abstract;
using ShopApp.WebUI.Factories;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(
            IOrderService orderService, 
            UserManager<ApplicationUser> userManager,
            IOrderModelFactory orderModelFactory)
        {
            _orderModelFactory = orderModelFactory;
            _userManager = userManager;
            _orderService = orderService;
        }

        public IActionResult List()
        {
            string userId = _userManager.GetUserId(User);
            var orders = _orderService.GetOrderList(userId);
            var listOfOrderModelList = _orderModelFactory.PrepareListModel(orders);

            return View(listOfOrderModelList);
        }
    }
}