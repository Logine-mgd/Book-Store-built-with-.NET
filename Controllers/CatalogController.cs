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
    public class CatalogController : ControllerBase
    {
        unitofwork uow;
        IMapper map;

        public CatalogController(unitofwork _uow,IMapper map)
        {
            uow = _uow;
            this.map = map;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Show Catalog")]
        public IActionResult GetAll()
        {
            List<Catalog> cs = uow.Rep_catalog.SelectAll();
            return Ok(cs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Catalog By ID")]
        [SwaggerResponse(200, "Catalog is found")]
        [SwaggerResponse(404, "Catalog is not found")]
        public IActionResult GetById(int id)
        {
            Catalog c = uow.Rep_catalog.SelectbyId(id);
            if(c == null) return NotFound();
            return Ok(c);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add new Catalog")]
        [SwaggerResponse(200, "Catalog is added")]
        [Authorize(Roles = "admin")]
        public IActionResult Put(CatalogDTO cdto)
        {
            Catalog c = map.Map<Catalog>(cdto);
            uow.Rep_catalog.Add(c);
            uow.Rep_catalog.Save();
            return Ok();
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Edit Existing Catalog")]
        [SwaggerResponse(200, "Catalog is updated")]
        [Authorize(Roles = "admin")]
        public IActionResult EditC(CatalogDTO cdto)
        {
            Catalog c = map.Map<Catalog>(cdto);
            uow.Rep_catalog.Edit(c);
            uow.Rep_catalog.Save();
            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Catalog")]
        [SwaggerResponse(200, "Catalog is deleted")]
        [SwaggerResponse(404, "Catalog is not found")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            Catalog c = uow.Rep_catalog.SelectbyId(id);
            if (c == null) return BadRequest();
            uow.Rep_catalog.Delete(id);
            uow.Rep_catalog.Save();
            return Ok();
        }

    }
}
