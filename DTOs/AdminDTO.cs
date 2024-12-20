using System.ComponentModel.DataAnnotations;

namespace Project1.DTOs
{
    public class AdminDTO
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
      //  [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string email { get; set; }
        [Required]
        public string phonenumber { get; set; }

    }
}
