using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task5_RESTAPI.Services;
using Task5_RESTAPI.Db;
using static Task5_RESTAPI.Db.Role;
using Task5_RESTAPI.Db.Filter;
using Task5_RESTAPI.Db.DTO;
namespace Task5_RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;
        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost]
        public IActionResult Create(Role role)
        {
            try
            {
                roleService.Create(role);
                return CreatedAtAction(nameof(Create), new { roleid = role.RoleId }, role);
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
        public List<Role> GetRoles()
        {
            return Roles.RolesList;
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(int roleid)
        {
            try
            {
                var result = roleService.GetById(roleid);
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
                return NotFound("Role not found");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int roleid, Role role)
        {
            try
            {
                var updated = roleService.Update(roleid, role);
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
                return NotFound("Role not found");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int roleid)
        {
            try
            {
                var deleted = roleService.DeleteById(roleid);
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
                return NotFound("Role not found");
            }
        }
    }
}

