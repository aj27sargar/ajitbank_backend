using System.Security.Claims;
using Ajit_Bank.AppServices;
using Ajit_Bank.Helpers;
using Ajit_Bank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ajit_Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ErrorLogger _logger;

        public AuthController(AuthService authService, ErrorLogger logger)
        {
            _authService = authService;
            _logger = logger;
        }

        // Login Endpoint
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = _authService.Login(request);
                if (result.Success)
                {
                    _logger.LogInfo($"User '{request.Username}' logged in successfully.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInfo($"Failed login attempt for username: {request.Username}");
                    return Unauthorized(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception during login for username: {request.Username}", ex);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Register Endpoint
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                var result = _authService.Register(request);
                if (result.Success)
                {
                    _logger.LogInfo($"User '{request.Username}' registered successfully.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInfo($"Failed registration attempt for username: {request.Username}");
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception during registration for username: {request.Username}", ex);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Profile Endpoint
        [HttpGet("profile")]
        [Authorize]
        public IActionResult Profile()
        {
            try
            {
                var username = User.Identity?.Name;
                var email = User.FindFirst("Email")?.Value;
                var phone = User.FindFirst("phone")?.Value;

                _logger.LogInfo($"Profile accessed by '{username}'");

                return Ok(new
                {
                    username,
                    email,
                    phone
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while fetching profile", ex);
                return StatusCode(500, new { message = "Failed to load profile" });
            }
        }

        // Update Profile Endpoint
        [HttpPut("update-profile")]
        [Authorize]
        public IActionResult UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                var result = _authService.UpdateUserProfile(userId, request.Username, request.Email, request.PhoneNumber);

                if (!result)
                {
                    _logger.LogInfo($"Failed profile update for user ID {userId}");
                    return BadRequest(new { message = "Update failed" });
                }

                _logger.LogInfo($"Profile updated for user ID {userId}");
                return Ok(new { message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in UpdateProfile", ex);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
