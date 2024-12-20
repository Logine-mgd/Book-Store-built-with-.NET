using System.ComponentModel.DataAnnotations;

namespace Project1.DTOs
{
    public class ChangepassDTO
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string oldpassword { get; set; }
        [Required]
        public string newpassword { get; set; }
        [Compare("newpassword",ErrorMessage =("Password doesn't match"))]
        public string confirm_password { get; set; }
    }

    public class CustomerDTO
    {
        [Required]
        public string id { get; set; }
        public string Name { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string email { get; set; }

        public string Address { get; set; }
        [Required]
        public string phonenumber { get; set; }

    }
}
