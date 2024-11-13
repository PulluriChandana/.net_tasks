using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TASKS_6_MVC_.Models;
using TASKS_6_MVC_.Services;

namespace TASKS_6_MVC_.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService iemployeeService;
        private readonly IDepartmentService departmentService;
        public EmployeeController(IEmployeeService iemployeeService,IDepartmentService departmentService)
        {
            this.iemployeeService = iemployeeService;
            this.departmentService=departmentService;
        }
     
        // GET: Employee
        public IActionResult Index()
        {
           var employees = iemployeeService.GetAll();
            //ViewBag.Departments = departments;
            return View(employees);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            ViewBag.Departments=this.departmentService.GetAll();
            return View();
            
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    iemployeeService.Create(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (BadHttpRequestException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Departments = this.departmentService.GetAll();
            return View(employee);
        }

        // GET: Employee/Details/5
        public IActionResult Details(int id)
        {
            Employee employee = iemployeeService.GetByEmpNo(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Employee/Edit/{id}
        public IActionResult Edit(int id)
        {
            var employee = iemployeeService.GetByEmpNo(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Departments = this.departmentService.GetAll();
            return View(employee);
        }
 
        // POST: Employee/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    iemployeeService.Upadte(id, employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (BadHttpRequestException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Departments = this.departmentService.GetAll();
            return View(employee);
        }

        // GET: Employee/Delete/{id}
        public IActionResult Delete(int id)
        {
            Employee employee=iemployeeService.GetByEmpNo(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                iemployeeService.DeleteByNo(id);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Employee/ByGender
        public IActionResult ByGender(Gender gender)
        {
            var employees = iemployeeService.GetByGender(gender);
            return View(employees);
        }
    }
}