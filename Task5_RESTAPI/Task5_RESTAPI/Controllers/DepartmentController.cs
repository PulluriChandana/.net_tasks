using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task5_RESTAPI.Db;
using Task5_RESTAPI.Services;
using static Task5_RESTAPI.Db.Department;

namespace Task5_RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
       // private DepartmentService departmentService;
       private readonly IDepartmentService departmentService;
        public DepartmentController(HrDbContext hrDbContext)
        {
            //DepartmentService = new DepartmentService();
            departmentService=new DepartmentServiceWithEF(hrDbContext);
        }
        
        [HttpPost]
        public IActionResult Create(Department department)
        {
            try
            {
                this.departmentService.Create(department);
                return CreatedAtAction(nameof(Create), new { id = department.DepartmentId }, department);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Unhandled exception occured");
            }

        }
        [HttpGet]
        public List<Department> GetDepartments()
        {
            /*var departments = departmentService.GetAll();
            return Departments.departments;*/
            var departmentsFromDb = departmentService.GetAll();
            var combinedDepartments = Departments.departments.Concat(departmentsFromDb).ToList();
           // departmentsFromDb.AddRange(Departments.departments);
            return combinedDepartments;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = departmentService.GetById(id);
                if (result == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(result);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Employee not found");
            }
            return NotFound("An unexpected error occured");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Department updateddepartment)
        {
            try
            {
                var updated = departmentService.Update(id, updateddepartment);
                if (updated)
                {
                    return Ok();
                }
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Employee not found");
            }
            return NotFound("An unexpected error occured");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleted = departmentService.DeleteById(id);
                if(deleted)
                {
                    return Ok();
                }
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Employee not found");
            }
            return NotFound("An unexpected error occured");
        }
    }
}
