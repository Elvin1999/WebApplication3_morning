using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication3.Dtos;
using WebApplication3.Entities;
using WebApplication3.Services.Abstract;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                Fullname = model.Fullname,
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new
            {
                message = "User registered successfully"
            });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                return Unauthorized("Invalid email or password");
            }

            var accessToken = await _tokenService.GenerateToken(user);

            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDto model)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);

            if (principal == null)
            {
                return Unauthorized("Invalid access token");
            }

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            if (user.RefreshToken != model.RefreshToken)
            {
                return Unauthorized("Invalid refresh token");
            }

            if (user.RefreshTokenExpireTime <= DateTime.Now)
            {
                return Unauthorized("Refresh token expired");
            }

            var newAccessToken = _tokenService.GenerateToken(user);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpireTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }


    }
}
