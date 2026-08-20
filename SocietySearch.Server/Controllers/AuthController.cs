using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SocietySearch.Server.Model.DTO;
using SocietySearch.Server.Repositories;

namespace SocietySearch.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Manager> _userManager;
        private readonly TokenRepository _tokenRepository;

        public AuthController(UserManager<Manager> userManager, TokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterManagerRequestDto registerManagerRequestDto)
        {
            //Check existing email address
            var existingUser = await _userManager.FindByEmailAsync(
                registerManagerRequestDto.Email
                );
            if (existingUser != null)
            {
                return BadRequest(new
                {
                    message = "Email already exists."
                });
            }
            var manager = new Manager
            {
                Email = registerManagerRequestDto.Email,
                UserName = registerManagerRequestDto.Email,
                Name = registerManagerRequestDto.Name,
                PhoneNumber = registerManagerRequestDto.PhoneNumber,

            };
            var result = await _userManager.CreateAsync(manager, registerManagerRequestDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Manager registration failed.",
                    errors = result.Errors.Select(e => new
                    {
                        code = e.Code,
                        description = e.Description
                    })
                });
            }
            var roleResult =
               await _userManager.AddToRoleAsync(
                   manager,
                   "Manager");

            if (!roleResult.Succeeded)
            {
                // Optional cleanup if role assignment fails
                await _userManager.DeleteAsync(manager);

                return BadRequest(new
                {
                    message = "Manager role could not be assigned.",
                    errors = roleResult.Errors.Select(e => new
                    {
                        code = e.Code,
                        description = e.Description
                    })
                });
            }

            return Ok(new
            {
                message = "Manager registered successfully.",
                userId = manager.Id
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginManagerRequestDto request)
        {
            // Find manager by username
            var manager =
                await _userManager.FindByNameAsync(
                    request.Email);

            if (manager == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            // Verify password
            var passwordValid =
                await _userManager.CheckPasswordAsync(
                    manager,
                    request.Password);

            if (!passwordValid)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            // Verify that user is a Manager
            var isManager =
                await _userManager.IsInRoleAsync(
                    manager,
                    "Manager");

            if (!isManager)
            {
                return Forbid();
            }

            // Get roles
            var roles =
                await _userManager.GetRolesAsync(manager);

            // Generate JWT
            var token =
                _tokenRepository.GetToken(
                    manager,
                    roles);

            return Ok(new LoginResponseDto
            {
                Token = token,
                Id = manager.Id,
                Email = manager.Email!,
                Name = manager.Name!,
                PhoneNumber = manager.PhoneNumber!
            });
        }
    }

}
