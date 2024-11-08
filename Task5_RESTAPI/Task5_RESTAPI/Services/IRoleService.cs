using System.Collections.Generic;
using Task5_RESTAPI.Db;

namespace Task5_RESTAPI.Services
{
    public interface IRoleService
    {
        bool Create(Role role);
        bool Update(int roleid, Role role);
        bool DeleteById(int roleid);
        Role GetById(int roleid);
        List<Role> GetAll();

    }
}
