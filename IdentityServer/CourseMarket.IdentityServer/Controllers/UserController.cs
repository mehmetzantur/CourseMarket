using CourseMarket.IdentityServer.Dtos;
using CourseMarket.IdentityServer.Models;
using CourseMarket.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseMarket.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupDto signupDto)
        {
            var user = new ApplicationUser { UserName = signupDto.UserName, Email = signupDto.Email, City = signupDto.City };
            var result = await _userManager.CreateAsync(user, signupDto.Password);

            if (!result.Succeeded)
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));

            return NoContent();
        }
    }
}
