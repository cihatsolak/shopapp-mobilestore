using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApp.DataAccess.Abstract.Implements;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.DataAccess.Identity;
using ShopApp.Services.Abstract;
using ShopApp.Services.Concrete;
using ShopApp.WebUI.Factories;
using ShopApp.WebUI.Middlewares;
using ShopApp.WebUI.Models.Accounts;
using ShopApp.WebUI.Models.Categories;
using ShopApp.WebUI.Models.Orders;
using ShopApp.WebUI.Models.Products;
using ShopApp.WebUI.Models.Roles;
using ShopApp.WebUI.Validators.Accounts;
using ShopApp.WebUI.Validators.Categories;
using ShopApp.WebUI.Validators.Orders;
using ShopApp.WebUI.Validators.Products;
using ShopApp.WebUI.Validators.Roles;
using System;

namespace ShopApp.WebUI
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ////Identity;
            services.AddDbContext<ApplicationIdentityDbContext>();

            /*
             * ApplicationUser -> IdentityUser'dan türettik. 
             * Ayný þekilde IdentityRole'ü de türetebilirdik ama burada direk kullandýk.
             * AddEntityFrameworkStores -> Datalarý nerede tutacaðým?
             * AddDefaultTokenProviders -> Parola deðiþikliði yaptýðýmýzda kullanýcýya token göndermemiz gerekiyor.
            */
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            //Identity Configure : Özelleþtirme
            services.Configure<IdentityOptions>(options =>
            {
                //Password: Sayýsal deðer gerekli
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6; //Password: Minimum 6 Karakter
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;

                //Login : Kullanýcý giriþ yaparken
                options.Lockout.MaxFailedAccessAttempts = 3; // 3 yanlýþ giriþ denemesinde sistemi kilitleyecek.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Kilitleme süresi 5 dakika.
                options.Lockout.AllowedForNewUsers = true; // Yeni bir kullanýcý içinde kilitleme geçerli olacak.

                //Username: Kullanýcý adý kurallarý
                options.User.RequireUniqueEmail = true; //Email adresleri unique yapar 

                //Sýgn In
                options.SignIn.RequireConfirmedEmail = true; //Kullanýcý hesap oluþturduðunda mail adresi ile onay yapmasý gereklidir.(true iken)
                options.SignIn.RequireConfirmedPhoneNumber = false; //True dersen onayý telefon ile yapmasý gereklidir.

            });

            //Identity Cookie Ayarlarý
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Base/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); //Cookie ile 30 dakika kullanýcý bilgilerini tutuyorum.

                /* Örneðin cookie'nin süresi 30 dakika. Kullanýcý 15'inci dakika da uygulamaya talep 
                 * gönderdiðine direk 30 dakikaya tekrar eþitlenecek. Eðer 20 dakikalýk süre aþýlýrsa tekrar giriþ gerektirir.
                 */
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".ShopApp.Security.Cookie",
                    /* CSRF Attacklrýný engellemek için. Server'in bana verdiði id ile (cookie'de) tuttuðum.
                     * bir baþka kullanýcý bu id'yi alýp sistemde gezmemesi için yaptýðýmýz bir ayar.
                     * Yani baþka bir kullanýcý bizim cookie'yi alýp sistemde gezemez.
                     */
                    SameSite = SameSiteMode.Strict
                };
            });


            //Data Access Layer
            services.AddScoped<IProductDal, EfCoreProductDal>();
            services.AddScoped<ICategoryDal, EfCoreCategoryDal>();
            services.AddScoped<ICartDal, EfCoreCartDal>();
            services.AddScoped<IOrderDal, EfCoreOrderDal>();

            //Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartService, CartService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IOrderService, OrderService>();

            //Model Factories
            services.AddScoped<IProductModelFactory, ProductModelFactory>();
            services.AddScoped<ICategoryModelFactory, CategoryModelFactory>();
            services.AddScoped<IAccountModelFactory, AccountModelFactory>();
            services.AddScoped<IRoleModelFactory, RoleModelFactory>();
            services.AddScoped<ICartModelFactory, CartModelFactory>();
            services.AddScoped<IOrderModelFactory, OrderModelFactory>();

            //Validator
            services.AddMvc(setup =>
            {
                //...mvc setup...
            }).AddFluentValidation();

            services.AddTransient<IValidator<ProductModel>, ProductModelValidator>();
            services.AddTransient<IValidator<CategoryModel>, CategoryModelValidator>();
            services.AddTransient<IValidator<RegisterModel>, RegisterModelValidator>();
            services.AddTransient<IValidator<LoginModel>, LoginModelValidator>();
            services.AddTransient<IValidator<ResetPasswordModel>, ResetPasswordModelValidator>();
            services.AddTransient<IValidator<RoleModel>, RoleModelValidator>();
            services.AddTransient<IValidator<OrderModel>, OrderModelValidator>();

            //Auto Mapper
            services.AddAutoMapper(typeof(Startup));



            services.AddMvc();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> usr, RoleManager<ApplicationRole> rol)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Development time
                SeedDatabase.Seed();
            }

            app.UseRouting();

            //Identity
            app.UseAuthentication(); // Must be after UseRouting()
            app.UseAuthorization(); // Must be after UseAuthentication()

            // App static file open //wwwrooot
            app.UseStaticFiles();
            app.CustomStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "checkout",
                pattern: "siparisler",
                defaults: new { controller = "Order", action = "List" });

                endpoints.MapControllerRoute(
                name: "checkout",
                pattern: "odeme",
                defaults: new { controller = "Cart", action = "Checkout" });

                endpoints.MapControllerRoute(
                name: "adminProducts",
                pattern: "admin/products/{id?}",
                defaults: new { controller = "Admin", action = "EditProduct" });

                endpoints.MapControllerRoute(
                name: "adminProductsList",
                pattern: "admin/urun-listesi",
                defaults: new { controller = "Admin", action = "ProductList" });

                endpoints.MapControllerRoute(
                name: "cart",
                pattern: "sepet",
                defaults: new { controller = "Cart", action = "Get" });

                endpoints.MapControllerRoute(
                name: "products",
                pattern: "products/{categoryName?}",
                defaults: new { controller = "Shop", action = "List" });

                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            SeedIdentity.Seed(usr, rol, Configuration).Wait();
        }
    }
}
