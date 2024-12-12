using Microsoft.AspNetCore.Identity;

namespace WebAppPMRC.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
