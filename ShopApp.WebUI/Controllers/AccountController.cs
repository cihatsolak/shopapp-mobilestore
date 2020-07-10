using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.DataAccess.Identity;
using ShopApp.Services.Abstract;
using ShopApp.Web.Framework.Extensions;
using ShopApp.WebUI.Factories;
using ShopApp.WebUI.Models.Accounts;
using ShopApp.WebUI.Models.ResultMessages;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        //UserManager -> Identity Property
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountModelFactory _accountModelFactory;
        private readonly IEmailService _emailService;
        private readonly ICartService _cartService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAccountModelFactory accountModelFactory,
            IEmailService emailService,
            ICartService cartService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountModelFactory = accountModelFactory;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var applicationUser = _accountModelFactory.PrepareApplicationUserEntity(model);

            var result = await _userManager.CreateAsync(applicationUser, model.Password);

            if (result.Succeeded)
            {
                /*
                 * Token oluşturulacak ve kullanıcıya mail atılacak. (Generate token and sent email token)
                 * Kullanıcıya gönderdiğimiz mail(içerisinde url var) kullanıcısı hesabını onaylayacak.
                */
                var generatedToken = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                var callBackUrl = Url.Action("ConfirmEmail", "Account", new { userId = applicationUser.Id, token = generatedToken });

                //send mail
                await _emailService.SendEmailAsync(model.Email, "Kullanıcı Doğrulaması", callBackUrl);

                TempData.Put("message", new ResultMessage
                {
                    Title = "Hesap Onayı",
                    Message = "Lütfen e-posta adresinize gönderilen içerik ile üyelik işleminizi tamamlayınız.",
                    Type = "warning"
                });

                return RedirectToAction("Login", "Account");
            }

            ViewBag.ErrorMessage = "Bilinmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            //todo: Kişi giriş yapmışsa daha önce, yönlendir.
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.ReturlUrl = returnUrl ?? "~/Home/Index";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var applicationUser = await _userManager.FindByNameAsync(model.Username);

            if (applicationUser == null)
            {
                ViewBag.ErrorMessage = "* Girilen bilgilere ait kullanıcı bulunamadı.";
                return View(model);
            }

            //Kullanıcı oluşturduğu hesabı onayladı mı? onaylamadı mı?
            bool isAccountApproved = await _userManager.IsEmailConfirmedAsync(applicationUser);

            if (!isAccountApproved)
            {
                ViewBag.ErrorMessage = "* Lütfen mail adresinize gönderilen link ile kullanıcı onaylama işlemini gerçekleştiriniz.";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(applicationUser, model.Password, model.RememberMe, false);

            if (result.Succeeded)
                return Redirect(model.RedirectUrl);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            TempData.Put("message", new ResultMessage
            {
                Title = "Oturum Kapatıldı",
                Message = "Çıkış işleminiz güvenli bir şekilde gerçekleştirildi.",
                Type = "success"
            });

            return RedirectToAction("List", "Shop");
        }

        [HttpGet]
        //kayıt sonrasında kullanıcıya gönderilen email ile hesabını onaylaması.
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
                return Redirect("~/");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Redirect("~/");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                TempData.Put("message", new ResultMessage
                {
                    Title = "İşlem başarısız.",
                    Message = "Kullanıcı doğrulama işlemi başarısız olarak gerçekleşti.",
                    Type = "danger"
                });

                return Redirect("~/");
            }

            //Sisteme kaydını doğrulayan kullanıcı için cart item ekliyoruz.
            _cartService.InitializeCart(user.Id);

            TempData.Put("message", new ResultMessage
            {
                Title = "Hesabınız Doğrulandı.",
                Message = "Doğrulama işlemi başarıyla gerçekleşti.",
                Type = "success"
            });

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                return View();

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Kullanıcı bulunamadı.";
                return View(email);
            }

            string generatedToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callBackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = generatedToken });

            //Send Mail
            await _emailService.SendEmailAsync(user.Email, "Şifre Sıfırlama Talebi", callBackUrl);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token) || token == null)
                return RedirectToAction("Index", "Home");

            var model = new ResetPasswordModel { Token = token };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return RedirectToAction("Index", "Home");

            //Reset Password
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
                return RedirectToAction("Login");

            ViewBag.ErrorMessage = "Şifre sıfırlanırken bir hata ile karşılaşıldı.";
            return View(model);
        }
    }
}