namespace Task5_RESTAPI.Db
{
    public static class Roles
    {
        public static List<Role> RolesList = new List<Role>
        {
            new Role { RoleId = 101, RoleName = "Engineer" },
            new Role { RoleId = 102, RoleName = "Senior Manager"},
            new Role { RoleId = 103, RoleName = "Junior Developer"},
            new Role { RoleId = 104, RoleName = "General Manager" },
            new Role { RoleId = 105, RoleName = "HR" }
        };
    }
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

    }
}
