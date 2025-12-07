using System.ComponentModel.DataAnnotations;

namespace IndianBank.Models.AuthenticateViewModel
{
    public class SignupViewModel
    {
        [Required]
        public String FullName { get; set; }

        [Required,EmailAddress]
        public String  Email{ get; set; }
        [Required,MinLength(10),MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required,MinLength(6)]
        public String Password { get; set; }
        [Required,Compare("Password")]
        public String ComparePassword { get; set; }

    }
}
