using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NG.App.Models.Identity;
using NG.Entities;
using System.Threading.Tasks;

namespace NG.App.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<NGUser> userManager;
        private SignInManager<NGUser> signInManager;

        public AuthController(UserManager<NGUser> userManager, SignInManager<NGUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Username);
            
            if (user == null)
            {
                return Unauthorized();                
            }

            var signInResult = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!signInResult.Succeeded)
            {
                return Unauthorized();
            }
            
            return Ok();
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {                        
            await this.signInManager.SignOutAsync();

            return Ok();
        }
    }
}