using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ajit_Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // This endpoint requires a valid JWT token
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            // You can extract user identity info here if needed
            var username = User.Identity?.Name ?? "Unknown User";

            return Ok(new
            {
                message = "JWT authentication successful!",
                user = username
            });
        }
    }
}
