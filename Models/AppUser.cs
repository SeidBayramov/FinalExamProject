using Microsoft.AspNetCore.Identity;

namespace FinalExamProject.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
