using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;
using TASKS_6_MVC_.Models;
using TASKS_6_MVC_.Services;

namespace TASKS_6_MVC_.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService idepartmentService;
        public DepartmentController(IDepartmentService idepartmentService)
        {
            this.idepartmentService = idepartmentService;
        }

        // GET: Department
        public IActionResult Index()
        {
            var departments = idepartmentService.GetAll();
            return View(departments);
        }

        [Authorize(Roles = "Admin")]
        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    idepartmentService.Create(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the department: " + ex.Message);
                }
            }
            
            return View(department);
        }

        // GET: Department/Details/5
        public IActionResult Details(int id)
        {
            var department = idepartmentService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [Authorize(Roles = "Admin")]
        // GET: Department/Edit/{id}
        public IActionResult Edit(int id)
        {
            var department = idepartmentService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [Authorize(Roles = "Admin")]
        // POST: Department/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    idepartmentService.Update(id, department);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex) when(ex is ArgumentNullException || ex is BadHttpRequestException)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        [Authorize(Roles = "Admin")]
        // GET: Department/Delete/{id}
        public IActionResult Delete(int id)
        {
            var department = idepartmentService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [Authorize(Roles = "Admin")]
        // POST: Department/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                idepartmentService.DeleteById(id);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}