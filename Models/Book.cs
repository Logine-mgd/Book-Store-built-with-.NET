using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id {  get; set; }
        public string Title {  get; set; }
        [ForeignKey("Author")]
        public int Auth_id {  get; set; }
        public int Stock {  get; set; }
        public double Price {  get; set; }
        public DateTime PublishDate { get; set; }
        [ForeignKey("Catalog")]
        public int Cat_id { get; set; }
        
        public virtual Author Author { get; set; }
        public virtual Catalog Catalog { get; set; }
    }
}
