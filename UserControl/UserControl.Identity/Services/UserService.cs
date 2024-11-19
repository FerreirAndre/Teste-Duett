using Microsoft.AspNetCore.Identity;
using UserControl.Application.Contracts.Identity;
using UserControl.Application.Models.Identity;
using UserControl.Identity.Models;

namespace UserControl.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<User> GetUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            return users.Select(q => new User
            {
                Id = q.Id,
                Email = q.Email,
                Name = q.Name,
            }).ToList();
        }
    }
}
