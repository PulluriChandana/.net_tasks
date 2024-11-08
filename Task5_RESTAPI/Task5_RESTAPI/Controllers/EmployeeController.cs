using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task5_RESTAPI.Services;
using Task5_RESTAPI.Db;
using static Task5_RESTAPI.Db.Employee;
using Task5_RESTAPI.Db.Filter;
using Task5_RESTAPI.Db.DTO;

namespace Task5_RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //private EmployeeService employeeService;
        private readonly IEmployeeService employeeService;

        public EmployeeController(HrDbContext hrDbContext)
        {
            //employeeService = new EmployeeService();
            employeeService = new EmployeeServiceWithEF(hrDbContext);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            try
            {
                employeeService.Create(employee);
                return CreatedAtAction(nameof(Create), new { empno = employee.Employeeno }, employee);
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
        public List<Employee> GetEmployees([ FromQuery]EmployeeFilter filter)
        {
            /* var employees = employeeService.GetAll();
             return Employees.employees;*/
            var employeesFromDb = employeeService.GetAll(filter);
            //var combinedEmployees = Employees.employees.Concat(employeesFromDb).ToList();
            return employeesFromDb;  
        }
        //ceate end point to get emplyess by gender
        // create endpoint get eaplues ny departmnend id

        [HttpGet("{id}")]
        public IActionResult GetByNo(int employeeno)
        {
            try
            {
                var result = employeeService.GetByNo(employeeno);
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
        }

        [HttpGet("with-department")]
        public IActionResult GetAllWithEmployeeDepartment(string location)
        {
            try
            {
                var result = employeeService.GetAllEmployeeWithDepartment(location);
                if (result == null && result.Any())
                {
                    return NotFound("No employees found for the specified location");
                }
                return Ok(result);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException) 
            {
                return NotFound("Employee not found");
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int employeeno, Employee employee)
        {
            try
            {
                var updated = employeeService.Upadte(employeeno, employee);
                if (updated)
                {
                    return Ok();
                }
                return NotFound("An unexpected error occured");
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Employee not found");
            }
        }

        [HttpGet("gender/{gender}")]
        public IActionResult GetEmployeeByGender(Gender gender)
        {
            try
            {
                var employees = employeeService.GetByGender(gender);
                if(employees == null || !employees.Any())
                {
                    return NotFound("No employees found for the specified gender.");
                }
                return Ok(employees);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Employee not found");
            }
        }

        [HttpGet("departmentId{departmentId}")]
        public IActionResult GetEmployeesByDepartmentId(int departmentId)
        {
            try
            {
                var employees = employeeService.GetByDepartmentId(departmentId);
                if (employees == null || !employees.Any())
                {
                    return NotFound("No employees found for the specified department ID.");
                }
                return Ok(employees);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Employee not found");
            }
        }

        [HttpGet("roleId{roleId}")]
        public IActionResult GetEmployeesByRoleId(int roleId)
        {
            try
            {
                var role = employeeService.GetByRoleID(roleId);
                if (role == null || !role.Any())
                {
                    return NotFound("No roles found for the specified role ID.");
                }
                return Ok(role);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Employee not found");
            }
        }
        
        [HttpGet("department-report")]
        public IActionResult GetEmployeeDepartmentReport()
        {
            try
            {
                var report = employeeService.GetEmployeeDepartmentReport();
                return Ok(report);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Employee not found");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int employeeno)
        {
            try
            {
                var deleted = employeeService.DeleteByNo(employeeno);
                if (deleted)
                {
                    return Ok();
                }
                return NotFound("An unexpected error occured");

            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Employee not found");
            }
        }
    }
}