using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.DTOs;
using Project1.Models;
using Project1.UnitofWork;
using Swashbuckle.AspNetCore.Annotations;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        IMapper map;
        unitofwork uow;

        public AuthorController(IMapper map, unitofwork uow)
        {
            this.map = map;
            this.uow = uow;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Show all Authors")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
           List<Author> _as = uow.Rep_authors.SelectAll();
            return Ok(_as);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add New Author")]
        [SwaggerResponse(200, "A new Author is added", typeof(AuthorDTO))]
        [SwaggerResponse(400, "Invalid data")]
        //[Authorize(Roles = "admin")]
        public IActionResult Add(AuthorDTO adto)
        {
            if (ModelState.IsValid)
            {
                Author a = map.Map<Author>(adto);
                uow.Rep_authors.Add(a);
                uow.Rep_authors.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Edit Existing Author")]
        [SwaggerResponse(200, "Author is updated")]
        [SwaggerResponse(400, "Invalid data")]
        //[Authorize(Roles = "admin")]
        public IActionResult Edit(AuthorDTO adto)
        {
            if (ModelState.IsValid)
            {
                Author a = map.Map<Author>(adto);
                uow.Rep_authors.Edit(a);
                uow.Rep_authors.Save();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }

        }

        
    }

}

