using Microsoft.AspNetCore.Mvc;

namespace ShopApp.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}