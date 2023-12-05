using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userMAnager;
        public readonly TokenService _tokenService ;

        public AccountController(UserManager<AppUser> userMAnager, TokenService tokenService)
        {
            _tokenService = tokenService;
            _userMAnager = userMAnager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
            var user = await _userMAnager.FindByEmailAsync(loginDto.Email);
            if (user== null) return Unauthorized();

            var roles = await _userMAnager.GetRolesAsync(user);

            var result = await _userMAnager.CheckPasswordAsync(user,loginDto.Password);
            if (result){
                return new UserDto{
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    UserName = user.UserName,
                    Roles = roles.ToList()
                };
            }
            return Unauthorized();
        }
    }
}