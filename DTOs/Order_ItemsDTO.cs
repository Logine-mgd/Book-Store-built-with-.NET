using Project1.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DTOs
{
    public class Order_ItemsDTO
    {
        public int Order_id { get; set; }
        public int Book_id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        
    }
}
