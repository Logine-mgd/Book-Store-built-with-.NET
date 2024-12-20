using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Customer:IdentityUser
    {
        public string Name { get; set; }
        public string Address {  get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
