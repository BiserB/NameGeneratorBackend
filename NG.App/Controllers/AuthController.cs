using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NG.App.Models.Identity;
using NG.App.Models.JWT;
using NG.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NG.App.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<NGUser> userManager;
        private SignInManager<NGUser> signInManager;
        private readonly AppSettings appSettings;

        public AuthController(UserManager<NGUser> userManager,
            SignInManager<NGUser> signInManager,
            IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appSettings = appSettings.Value;
        }

        [HttpGet("IsLoggedIn")]
        public async Task<ActionResult> IsLoggedIn()
        {
            bool isLoggedIn = await this.userManager.GetUserAsync(this.HttpContext.User) != null;
            
            return Ok(isLoggedIn);
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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Username);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.Email,
                Token = tokenString
            });
        }
    }
}