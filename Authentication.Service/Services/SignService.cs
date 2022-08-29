using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Service.Services
{
    internal static class SignService
    {
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}