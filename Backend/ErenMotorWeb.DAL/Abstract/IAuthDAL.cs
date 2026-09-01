using ErenMotorWeb.DAL.Identity;
using Microsoft.AspNetCore.Identity;

namespace ErenMotorWeb.DAL.Abstract
{
    public interface IAuthDAL
    {
        Task<bool> CreateUserAsync(AppUser user, string password);

        Task<bool> AddUserToRoleAsync(AppUser user, string role);

        Task<bool> RoleExistsAsync(string roleName);

        Task<bool> CreateRoleAsync(string roleName);

        Task<AppUser?> GetUserByEmailAsync(string email);

        Task<AppUser?> GetUserByUserNameAsync(string userName);

        Task<AppUser?> GetUserByIdAsync(int id);

        Task<IList<string>> GetUserRolesAsync(AppUser user);

        Task<bool> CheckPasswordAsync(
            AppUser user,
            string password);

        Task<SignInResult> SignInAsync(
            string userName,
            string password,
            bool isPersistent,
            bool lockoutOnFailure);

        Task SignOutAsync();

        Task<bool> IsEmailUniqueAsync(string email);

        Task<bool> IsUserNameUniqueAsync(string username);

        Task<bool> UpdateUserAsync(AppUser user);

        Task<bool> DeleteUserAsync(AppUser user);

        //Task<int> GetUsersCount();

        Task<IEnumerable<AppUser>> GetAllUsersAsync();
    }
}
