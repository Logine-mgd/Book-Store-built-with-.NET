using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Models
{
    public class Order_Items
    {
        [ForeignKey("Order")]
        public int Order_id {  get; set; }
        [ForeignKey("Book")]
        public int Book_id {  get; set; }
        public int Quantity {  get; set; }
        public double UnitPrice { get; set; }
        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }

    }
}
