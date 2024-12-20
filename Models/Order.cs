using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int Cust_id {  get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice {  get; set; }
        public string Status {  get; set; }
        public virtual Customer customer { get; set; }
        public virtual List<Order_Items> Orderitems { get; set; } = new List<Order_Items>();
        
    }
}
