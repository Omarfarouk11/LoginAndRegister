using Microsoft.AspNetCore.Identity;

namespace TestAPIJwtAgain.Model
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
