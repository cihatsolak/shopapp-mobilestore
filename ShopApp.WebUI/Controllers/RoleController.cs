using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.DataAccess.Identity;
using ShopApp.Web.Framework.Extensions;
using ShopApp.WebUI.Factories;
using ShopApp.WebUI.Models.ResultMessages;
using ShopApp.WebUI.Models.Roles;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public partial class RoleController : BaseController
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRoleModelFactory _roleModelFactory;

        public RoleController(RoleManager<ApplicationRole> roleManager, IRoleModelFactory roleModelFactory)
        {
            _roleManager = roleManager;
            _roleModelFactory = roleModelFactory;
        }

        [HttpGet]
        public IActionResult List()
        {
            var applicationRoleList = _roleManager.Roles.ToList();

            var roles = _roleModelFactory.PrepareRoleModelList(applicationRoleList);

            return View(roles);
        }

        [HttpPost]
        public IActionResult List(string asd)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
                return View(roleModel);

            var role = await _roleManager.FindByNameAsync(roleModel.Name);

            if (role != null)
            {
                ModelState.AddModelError(nameof(roleModel.Name), $"{role.Name} rol adı sistemde kayıtlıdır.");
                return View(roleModel);
            }

            var applicationRole = _roleModelFactory.PrepareApplicationRole(roleModel);
            var result = await _roleManager.CreateAsync(applicationRole);

            if (result.Succeeded)
            {
                TempData.Put("message", new ResultMessage
                {
                    Title = "Başarılı",
                    Message = $"{role.Name} adlı rol ekleme işlemi tamamlandı.",
                    Type = "success"
                });

                return RedirectToAction("List");
            }

            return View(roleModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("List");

            var user = await _roleManager.FindByIdAsync(id);

            if (user == null)
                return RedirectToAction("List");

            var roleModel = _roleModelFactory.PrepareRoleModel(user);

            return View(roleModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModel roleModel)
        {
            if (roleModel == null)
                RedirectToAction("List");

            var role = await _roleManager.FindByIdAsync(roleModel.Id);

            role.Name = roleModel.Name;

            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
                return View(roleModel);

            TempData.Put("message", new ResultMessage
            {
                Title = "Rol Düzenlendi.",
                Message = $"{role.Name} adlı rol düzenleme işlemi başarıyla tamamlandı.",
                Type = "success"
            });

            return RedirectToAction("List");
        }
    }
}