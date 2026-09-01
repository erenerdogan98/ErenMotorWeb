using Microsoft.AspNetCore.Identity;

namespace ErenMotorWeb.DAL.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole() : base()
        {
        }

        public AppRole(string roleName) : base(roleName)
        {
        }
    }
}
