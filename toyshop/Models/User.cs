using Microsoft.AspNetCore.Identity;

namespace toyshop.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}