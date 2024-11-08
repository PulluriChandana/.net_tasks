using Microsoft.AspNetCore.Mvc;
using Task5_RESTAPI.Db;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Task5_RESTAPI.Db.Filter;
using Microsoft.EntityFrameworkCore;
using Task5_RESTAPI.Db.DTO;

namespace Task5_RESTAPI.Services
{
    public class RoleService : IRoleService
    {
        private static int _id = 106;
        public bool Create(Role role)
        {
            var result=Validate(role);
            if(!string.IsNullOrEmpty(result))
            {
                throw new BadHttpRequestException(result);
            }
            role.RoleId = _id++;
            Roles.RolesList.Add(role);
            return true;
        }
        public bool Update(int roleid,Role role)
        {
            var result = Validate(role);
            if(!string.IsNullOrEmpty(result))
            {
                throw new BadHttpRequestException(result);
            }
            var existingRole = Roles.RolesList.FirstOrDefault(x => x.RoleId == roleid);
            if (existingRole == null)
            {
                throw new ArgumentNullException("Id not found");
            }
            existingRole.RoleName = role.RoleName;
            return true;
        }
        public bool DeleteById(int roleid)
        {
            var role=Roles.RolesList.FirstOrDefault(x => x.RoleId == roleid);
            if(role == null)
            {
                throw new ArgumentNullException("Id not found");
            }
            return Roles.RolesList.Remove(role);
        }
        public Role GetById(int roleid)
        {
            return Roles.RolesList.FirstOrDefault(x=>x.RoleId == roleid);
        }
        public List<Role> GetAll()
        {
            return Roles.RolesList;
        }

        private static string Validate(Role role)
        {
            if(string.IsNullOrEmpty(role.RoleName))
            {
                return "Role Name should not be empty";
            }
            return string.Empty;
        }
    }
}
