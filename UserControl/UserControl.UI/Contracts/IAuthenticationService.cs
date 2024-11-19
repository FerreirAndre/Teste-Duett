using UserControl.UI.Models;

namespace UserControl.UI.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> RegisterAsync(string Name, string Email, string cpf, string userName,string Password);
        Task<bool> DeleteAsync(string userId);
        Task<bool> ChangePassword(string email, string currentPassword, string newPassword);
        Task<List<UserVM>> GetUsers();
        Task Logout();
    }
}
