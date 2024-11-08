using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TASKS_6_MVC_.Models;
using TASKS_6_MVC_.Models.ViewModels;

namespace TASKS_6_MVC_.Controllers
{
    public class HomeController : Controller
    {
        private readonly HrDbContext hrDbContext;

        public HomeController(HrDbContext hrDbContext)
        {
            this.hrDbContext = hrDbContext;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Employees = hrDbContext.Employees.Include(e => e.Department).ToList(),
                Departments = hrDbContext.Departments.Include(d=>d.Employees).ToList()
            };

            return View(viewModel);
        }
    }
}
