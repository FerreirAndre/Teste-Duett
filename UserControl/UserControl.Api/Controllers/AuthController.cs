using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserControl.Application.Contracts.Identity;
using UserControl.Application.Models.Identity;

namespace UserControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authService.Register(request));
        }

        [HttpDelete("remove/{userId}")]
        public async Task<ActionResult> Delete(string userId)
        {
            await _authService.DeleteUser(userId);
            
            return Ok("User deleted.");
        }

        [HttpPost("changepassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var response = await _authService.ChangePassword(request);
            if (response.Succeeded)
            {
                return Ok("Password Changed");
            }
            
            return BadRequest(response.Errors);
        }
    }
}
