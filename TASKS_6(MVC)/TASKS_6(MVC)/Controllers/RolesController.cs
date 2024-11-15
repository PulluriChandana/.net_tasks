using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TASKS_6_MVC_.Models;

namespace TASKS_6_MVC_.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<SampleRole> _roleManager;
        public RolesController(RoleManager<SampleRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SampleRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SampleRole model)
        {
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(model);
            }
            return RedirectToAction("Index");
        }
    }
}
