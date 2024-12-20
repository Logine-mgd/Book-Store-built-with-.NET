using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Auth_id { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }
        public int Cat_id { get; set; }

    }
}
