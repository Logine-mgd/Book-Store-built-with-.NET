using Project1.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DTOs
{
    public class AddOrderDTO
    {
        public int Id { get; set; }
        public int Cust_id { get; set; }
       // public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public List<Order_ItemsDTO> Orderitems { get; set; } = new List<Order_ItemsDTO>();

    }
}
