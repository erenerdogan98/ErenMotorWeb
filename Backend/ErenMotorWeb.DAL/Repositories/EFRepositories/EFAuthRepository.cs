using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFAuthRepository(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        SignInManager<AppUser> signInManager)
        : IAuthDAL
    {
        public async Task<bool> CreateUserAsync(
            AppUser user,
            string password)
        {
            var result = await userManager.CreateAsync(user, password);

            return result.Succeeded;
        }

        public async Task<bool> AddUserToRoleAsync(
            AppUser user,
            string role)
        {
            var result = await userManager.AddToRoleAsync(user, role);

            return result.Succeeded;
        }

        public async Task<bool> RoleExistsAsync(
            string roleName)
        {
            return await roleManager.RoleExistsAsync(roleName);
        }

        public async Task<bool> CreateRoleAsync(
            string roleName)
        {
            var result = await roleManager.CreateAsync(
                new AppRole
                {
                    Name = roleName
                });

            return result.Succeeded;
        }

        public async Task<AppUser?> GetUserByEmailAsync(
            string email)
        {
            return await userManager.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<AppUser?> GetUserByUserNameAsync(
            string userName)
        {
            return await userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<AppUser?> GetUserByIdAsync(
            int id)
        {
            return await userManager.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<string>> GetUserRolesAsync(
            AppUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<bool> CheckPasswordAsync(
            AppUser user,
            string password)
        {
            return await userManager.CheckPasswordAsync(
                user,
                password);
        }

        public async Task<SignInResult> SignInAsync(
            string userName,
            string password,
            bool isPersistent,
            bool lockoutOnFailure)
        {
            return await signInManager.PasswordSignInAsync(
                userName,
                password,
                isPersistent,
                lockoutOnFailure);
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(
            string email)
        {
            return !await userManager.Users
                .AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsUserNameUniqueAsync(
            string username)
        {
            return !await userManager.Users
                .AnyAsync(x => x.UserName == username);
        }

        public async Task<bool> UpdateUserAsync(
            AppUser user)
        {
            var result = await userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(
            AppUser user)
        {
            var result = await userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await userManager.Users.CountAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await userManager.Users.ToListAsync();
        }
    }
}