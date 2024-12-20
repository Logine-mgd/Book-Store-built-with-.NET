using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project1.DTOs;
using Project1.Models;
using Project1.UnitofWork;
using Swashbuckle.AspNetCore.Annotations;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        unitofwork uow;
        IMapper map;
        RoleManager<IdentityRole> rolemanager;
        UserManager<IdentityUser> userManager;

        public CustomerController(UserManager<IdentityUser> user, RoleManager<IdentityRole> role, IMapper map)
        {
            this.userManager = user;
            this.rolemanager = role;
            this.map = map;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add new customer")]
        [SwaggerResponse(200, "Customer is added")]
        [SwaggerResponse(400, "Invalid Data")]
        public IActionResult Create(CustomerDTO c)
        {
            Customer cust = map.Map<Customer>(c);
            var r = userManager.CreateAsync(cust, c.password).Result;
            if (r.Succeeded)
            {
                IdentityResult rr = userManager.AddToRoleAsync(cust, "customer").Result;
                if (rr.Succeeded) return Ok();
                else return BadRequest(rr.Errors);
            }
            else
                return BadRequest(r.Errors);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Edit Customer Data")]
        [SwaggerResponse(200, "Customer is updated")]
        [SwaggerResponse(400, "Invalid Data")]
        [SwaggerResponse(401, "Unauthorized, must be an authorized customer")]
        [Authorize(Roles = "customer")]
        public IActionResult Edit(CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                Customer cust = (Customer)userManager.FindByIdAsync(customer.id).Result;
                if (cust == null) return NotFound();
                cust.Name= customer.Name;
                cust.Address=customer.Address;
                cust.PhoneNumber= customer.phonenumber;
                cust.Email = customer.email;
                cust.UserName = customer.username;
                var r = userManager.UpdateAsync(cust).Result;
                if (r.Succeeded)
                    return NoContent();
                else
                    return BadRequest(r.Errors);
            }
            else return BadRequest(ModelState);
        }

        [HttpPost("changepassword")]
        [SwaggerOperation(Summary = "Change Password")]
        [SwaggerResponse(200, "Password is updated")]
        [SwaggerResponse(400, "Invalid Data")]
        [SwaggerResponse(401, "Unauthorized, must be an authorized admin or customer")]
        [Authorize(Roles = "admin,customer")]
        public IActionResult changepassword(ChangepassDTO pass)
        {
            if (ModelState.IsValid)
            {
                var cust = userManager.FindByIdAsync(pass.id).Result;
                var r = userManager.ChangePasswordAsync(cust, pass.oldpassword, pass.newpassword).Result;
                if (r.Succeeded)   return Ok();
                else               return BadRequest(r.Errors);
            }
            else    return BadRequest(ModelState);
            }

        [HttpGet]
        [SwaggerOperation(Summary = "Show All customers")]
        [SwaggerResponse(401, "Unauthorized, must be an authorized admin")]
        [Authorize(Roles ="admin")]
        public IActionResult Getall()
        {
            var custs = userManager.GetUsersInRoleAsync("customer").Result.OfType<Customer>().ToList();
            if (!custs.Any()) return NotFound();
            List<CustomerDTO> cdtos = new List<CustomerDTO>();
            foreach(var u in custs)
            {
                cdtos.Add(map.Map<CustomerDTO>(u));
            }
            return Ok(cdtos);
        }
        
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get customer by id")]
        [SwaggerResponse(401, "Unauthorized, must be an authorized admin")]
        [Authorize(Roles = "admin")]
        public IActionResult getbyid(string id)
        {
            var cust = (Customer)userManager.GetUsersInRoleAsync("customer").Result.Where(n => n.Id == id).FirstOrDefault();
            if (cust == null) return NotFound();
            CustomerDTO cdto = map.Map<CustomerDTO>(cust);
            return Ok(cdto);
        }

    }
}
