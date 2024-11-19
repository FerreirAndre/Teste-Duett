using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserControl.Application.Models.Identity;

namespace UserControl.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
        Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request);
        Task DeleteUser(string UserId);
    }
}
