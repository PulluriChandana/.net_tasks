using Task5_RESTAPI.Db.DTO;
using Task5_RESTAPI.Db.Filter;
using Task5_RESTAPI.Db;
using Microsoft.AspNetCore.Mvc;

namespace Task5_RESTAPI.Services
{
    public class RoleServiceWithEF : IRoleService
    {
        private readonly HrDbContext hrDbContext;
        public RoleServiceWithEF(HrDbContext hrDbContext)
        {
            this.hrDbContext = hrDbContext;
        }
        public bool Create(Role role)
        {
            var result = Validate(role, hrDbContext);
            if (!string.IsNullOrEmpty(result))
            {
                throw new BadHttpRequestException(result);
            }
            this.hrDbContext.Add(role);
            return hrDbContext.SaveChanges()>0;
        }

        public bool DeleteById(int roleid)
        {
            var role=hrDbContext.roles.Find(roleid);
            if(role== null)
            {
                throw new ArgumentNullException("Id not found");
            }
            this.hrDbContext.Remove(role);
            return hrDbContext.SaveChanges()>0;
        }
       
        public List<Role> GetAll()
        {
            return hrDbContext.roles.ToList();
        }

        public Role GetById(int roleid)
        {
            return hrDbContext.roles.Find(roleid);
        }

        public bool Update(int roleid,Role role)
        {
            var result=Validate(role,hrDbContext);
            if (!string.IsNullOrEmpty(result))
            {
                throw new BadHttpRequestException(result);
            }
            var existingrole = this.hrDbContext.roles.Find(roleid);
            if(existingrole== null)
            {
                throw new ArgumentNullException("Role not found");
            }
            existingrole.RoleName=role.RoleName;
            return hrDbContext.SaveChanges() > 0;
        }
        private static string Validate(Role role,HrDbContext hrDbContext)
        {
            if (string.IsNullOrEmpty(role.RoleName))
            {
                return "Role Name should not be empty";
            }

            // chek is name is uniq
            bool isNameUnique=!hrDbContext.roles.Any(r=>r.RoleName.ToLower()==
            role.RoleName.ToLower() && r.RoleId!=role.RoleId);
            if (!isNameUnique)
            {
                return "Role name must be unique";
            }

            return string.Empty;
        }
    }
}
