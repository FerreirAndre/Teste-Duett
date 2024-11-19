using Microsoft.AspNetCore.Mvc;
using UserControl.Application.Contracts.Identity;
using UserControl.Application.Models.Identity;
using UserControl.Identity.Models;

namespace UserControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService { get; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok( await _userService.GetUsers());
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<User>> Get(string userId)
        {
            return Ok(await _userService.GetUser(userId));
        }

    }
}
