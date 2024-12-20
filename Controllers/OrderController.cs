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
    public class OrderController : ControllerBase
    {
        unitofwork uow;
        public OrderController(unitofwork uow)
        {
             this.uow = uow;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add New Order")]
        [SwaggerResponse(200, "A new Order is added", typeof(BookDTO))]
        [SwaggerResponse(400, "Invalid Quantity")]
        [Authorize(Roles = "admin,customer")]
        public IActionResult Add(AddOrderDTO odto)
        {
            Order o = new Order()
            {
                Id = odto.Id,
                Cust_id = odto.Cust_id,
                Date = DateTime.Now,
                Status = "created"
            };
            
            double totalprice = 0;
            foreach (var i in odto.Orderitems)
            {
                Book b = uow.Rep_books.SelectbyId(i.Book_id);
                totalprice += b.Price * i.Quantity;
                if (b.Stock >= i.Quantity)
                {
                    Order_Items oi = new Order_Items()
                    {
                        Order_id = o.Id,
                        Book_id = i.Book_id,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    };
                    o.Orderitems.Add(oi);
                    uow.Rep_orderitems.Add(oi);
                    uow.Rep_order.Add(o);
                    b.Stock -= i.Quantity;
                    uow.Rep_books.Edit(b);
                }
                else
                {
                    return BadRequest("Error in Quantity");
                }
            }
            o.TotalPrice = totalprice;
            uow.SaveChanges();
            return Ok();
        }
    }
}
