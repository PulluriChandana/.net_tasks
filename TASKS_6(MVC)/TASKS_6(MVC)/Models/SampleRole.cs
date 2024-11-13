using Microsoft.AspNetCore.Identity;

namespace TASKS_6_MVC_.Models
{
    public class SampleRole:IdentityRole
    {
        public string Description { get; set; }
    }
}
