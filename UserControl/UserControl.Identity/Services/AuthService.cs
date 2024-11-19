using CpfLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserControl.Application.Contracts.Identity;
using UserControl.Application.Models.Identity;
using UserControl.Domain.Exceptions;
using UserControl.Identity.Models;

namespace UserControl.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._jwtSettings = jwtSettings.Value;
            this._signInManager = signInManager;
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ChangePasswordResponse { Succeeded = false, Errors = new[] { "User not found" } };
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
                return new ChangePasswordResponse { Succeeded = true, Errors = null };
            else
                return new ChangePasswordResponse { Succeeded = false, Errors = result.Errors.Select(e => e.Description).ToArray() };
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException("user not found.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded == false)
            {
                throw new BadRequestException($"Credentials for '{request.Email}' are not valid.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName,
                Email = request.Email
            };
            return response;
        }


        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var validCpf = Cpf.Check(request.Cpf);
            if (!validCpf)
            {
                throw new InvalidCpfException();
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                UserName = request.UserName,
                Cpf = request.Cpf,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "USER");
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    sb.AppendFormat("- {0}\n", err.Description);
                }
                throw new BadRequestException($"{sb}");
            }
        }

        public async Task DeleteUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            await _userManager.DeleteAsync(user);

        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationMinutes),
                signingCredentials: signingCredentials
                );
            
            return jwtSecurityToken;
        }
    }
}
