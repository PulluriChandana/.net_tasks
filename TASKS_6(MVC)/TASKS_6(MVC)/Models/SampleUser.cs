using Microsoft.AspNetCore.Identity;

namespace TASKS_6_MVC_.Models
{
    public class SampleUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
