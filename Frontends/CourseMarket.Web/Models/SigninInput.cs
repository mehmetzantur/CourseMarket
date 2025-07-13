using System.ComponentModel.DataAnnotations;

namespace CourseMarket.Web.Models
{
    public class SigninInput
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Display(Name = "Remember Me")]
        public bool IsRemember { get; set; }
    }
}
