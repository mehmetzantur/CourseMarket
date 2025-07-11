using System.ComponentModel.DataAnnotations;

namespace CourseMarket.Web.Models
{
    public class SigninInput
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool IsRemember { get; set; }
    }
}
