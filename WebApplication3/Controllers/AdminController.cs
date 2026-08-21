using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication3.Dtos;
using WebApplication3.Entities;
using WebApplication3.Services.Abstract;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AdminController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            ITokenService tokenService,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }


        [HttpPost("register-manager")]
        [Authorize(Roles ="Admin")]
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
            var manager = await _userManager.FindByEmailAsync(model.Email);
            if (manager == null)
            {
                return BadRequest("Manager can not created");
            }
            await _userManager.AddToRoleAsync(manager, "Manager");


            return Ok(new
            {
                message = "User registered successfully"
            });

        }


    }
}
