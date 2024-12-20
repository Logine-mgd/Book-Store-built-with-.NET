using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project1.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IMapper map;
        SignInManager<IdentityUser> sign;
        UserManager<IdentityUser> UserManager { get; set; }
        public AccountController(IMapper m, SignInManager<IdentityUser> sign, UserManager<IdentityUser> UserManager)
        {
            map = m;
            this.UserManager = UserManager;
            this.sign = sign;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "User Login")]
        [SwaggerResponse(200, "User logged in successfully,token is returned")]
        [SwaggerResponse(401, "Unauthorized user")]
        public IActionResult Login(Login_DTO l)
        {
            var res = sign.PasswordSignInAsync(l.username, l.password, false, false).Result;
            if(res.Succeeded)
            {
                var user = UserManager.FindByNameAsync(l.username).Result;
                List<Claim> userdata = new List<Claim>();
                userdata.Add(new Claim(ClaimTypes.Name, user.UserName));
                userdata.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                var roles = UserManager.GetRolesAsync(user).Result;
                foreach (var itemRole in roles)
                {
                    userdata.Add(new Claim(ClaimTypes.Role, itemRole));
                }

                string key = "Secret key Logine Magdy Secret Key";
                var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                var signcer = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                     claims: userdata,
                     expires: DateTime.Now.AddDays(2),
                     signingCredentials: signcer
                     );
                var tokenst = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(tokenst);

            }
            else return Unauthorized("invalid username or pasword");
        }

        [HttpPost("changepassword")]
        [SwaggerResponse(401, "Unauthorized user")]
        //[Authorize]
        public IActionResult changepassword(ChangepassDTO pass)
        {
            if (ModelState.IsValid)
            {
                var cust = UserManager.FindByIdAsync(pass.id).Result; 
                var r = UserManager.ChangePasswordAsync(cust, pass.oldpassword, pass.newpassword).Result;
                if (r.Succeeded)
                    return Ok();
                else
                    return BadRequest(r.Errors);
            }
            else return BadRequest(ModelState);
        }

       
        [HttpGet("logout")]
        [Authorize]
        public IActionResult logout()
        {
            sign.SignOutAsync();
            return Ok();
        }
    }
}
