using Microsoft.AspNetCore.Identity;

namespace Authentication.Core.Model
{
    public class UserApp : IdentityUser
    {
        public string City { get; set; }
    }
}