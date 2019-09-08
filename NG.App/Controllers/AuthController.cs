using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NG.App.Models.Identity;
using NG.Entities;

namespace NG.App.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<NGUser> userManager;

        public AuthController(UserManager<NGUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Username);

            if (user == null || await this.userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized();                
            }
            
            return Ok(new { status = "pass is ok" });
        }
    }
}