using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using UserControl.UI.Contracts;
using UserControl.UI.Models;
using UserControl.UI.Providers;

namespace UserControl.UI.Services.Base
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient client,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider) : base(client, localStorage)
        {
            this._client = client;
            this._localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            try
            {
                AuthRequest authRequest = new AuthRequest()
                {
                    Email = email,
                    Password = password
                };

                var authResponse = await _client.LoginAsync(authRequest);

                if (authResponse.Token != string.Empty)
                {
                    await _localStorage.SetItemAsync("token", authResponse.Token);
                    await ((ApiAuthenticationStateProvider) _authenticationStateProvider).LoggedIn();
                       
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ChangePassword(string email, string currentPassword, string newPassword)
        {
            var changePasswordRequest = new ChangePasswordRequest
            {
                Email = email,
                CurrentPassword = currentPassword,
                NewPassword = newPassword
            };

            try
            {
                await _client.ChangepasswordAsync(changePasswordRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> DeleteAsync(string userId)
        {
            try
            {
                await _client.RemoveAsync(userId);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<List<UserVM>> GetUsers()
        {
            var users = await _client.UsersAsync();
            var response = new List<UserVM>();
            foreach (var user in users)
            {
                response.Add(new UserVM()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name
                });
            }
            return response;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }

        public async Task<bool> RegisterAsync(string Name, string Email, string cpf, string userName, string Password)
        {
            RegistrationRequest registrationRequest = new RegistrationRequest()
            {
                Name = Name,
                Email = Email,
                Cpf = cpf,
                UserName = userName,
                Password = Password
            };
            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
                return true;
            return false;
        }
    }
}
