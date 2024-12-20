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
    public class BookController : ControllerBase
    {
        unitofwork uow;
        IMapper map;
        public BookController(unitofwork _uow, IMapper map)
        {
            uow = _uow;
            this.map = map;
        }
        
        [HttpGet]
        [SwaggerOperation(Summary = "Show all books")]
        public IActionResult GetAll()
        {
            List<Book> bs = uow.Rep_books.SelectAll();
            return Ok(bs);
        }
        
        [HttpPost]
        [SwaggerOperation(Summary = "Add New Book")]
        [SwaggerResponse(200, "A new book is added", typeof(BookDTO))]
        [SwaggerResponse(400, "Invalid data")]
        [SwaggerResponse(401, "Unauthorized, must be an authorized admin")]
        [Authorize(Roles = "admin")]
        public IActionResult Add(BookDTO bdto)
        {
            if (ModelState.IsValid)
            {
                Book b = map.Map<Book>(bdto);
                uow.Rep_books.Add(b);
                uow.Rep_books.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Edit Existing Book")]
        [SwaggerResponse(200, "Book is updated")]
        [SwaggerResponse(400, "Invalid data")]
        [SwaggerResponse(401, "Unauthorized, must be an authorized admin")]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(BookDTO bdto)
        {
            if (ModelState.IsValid)
            {
                Book b = map.Map<Book>(bdto);
                uow.Rep_books.Edit(b);
                uow.Rep_books.Save();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Author")]
        [SwaggerResponse(200, "Book is deleted")]
        [SwaggerResponse(404, "Book is not found")]
        [SwaggerResponse(401, "Unauthorized, must be an authorized admin")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id) 
        {
            Book b = uow.Rep_books.SelectbyId(id);
            if(b!=null){
                uow.Rep_books.Delete(id);
                uow.Rep_books.Save();
                return Ok();
            }else 
                return NotFound();
        }
    }
    }
