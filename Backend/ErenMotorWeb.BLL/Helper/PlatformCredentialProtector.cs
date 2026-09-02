using Microsoft.AspNetCore.DataProtection;

namespace ErenMotorWeb.BLL.Helper
{
    public class PlatformCredentialProtector(
        IDataProtectionProvider dataProtectionProvider)
    {
        private readonly IDataProtector _protector = dataProtectionProvider.CreateProtector(
                "ErenMotorWeb.PlatformCredentials.v1");

        public string Protect(string value)
        {
            return _protector.Protect(value);
        }

        public string Unprotect(string value)
        {
            return _protector.Unprotect(value);
        }
    }
}