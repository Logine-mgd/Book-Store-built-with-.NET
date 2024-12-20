using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project1.DTOs;
using Project1.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IMapper map;
        RoleManager<IdentityRole> rolemanager;
        UserManager<IdentityUser> userManager;

        public AdminController(IMapper map,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolemanager)
        {
            this.userManager = userManager;
            this.rolemanager = rolemanager;
            this.map = map;
        }
        [HttpPost]
        [SwaggerOperation(Summary = "Create new admin")]
        [SwaggerResponse(200, "New Admin is created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized, must be an authorized admin")]
        [Authorize(Roles = "admin")]
        public IActionResult Create(AdminDTO ad)
        {
            Admin a = map.Map<Admin>(ad);
            IdentityResult ir = userManager.CreateAsync(a,ad.password).Result;
            if (ir.Succeeded)
            {
                IdentityResult r = userManager.AddToRoleAsync(a,"admin").Result;
                if (r.Succeeded) return Ok();
                else return BadRequest(r.Errors);
            }else
                return BadRequest(ir.Errors);
        }

        
    }
}
