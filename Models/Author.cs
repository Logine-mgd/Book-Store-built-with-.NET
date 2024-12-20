using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id {  get; set; }
        public string FullName {  get; set; }
        public string Bio {  get; set; }
        public int Age {  get; set; }
        public int Nbooks {  get; set; }
        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
}
