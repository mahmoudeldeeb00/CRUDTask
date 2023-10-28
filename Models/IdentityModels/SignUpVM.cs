using System.ComponentModel.DataAnnotations;
using Task.DAL.IdentityEntities;

namespace Task.Models.IdentityModels
{
    public class SignUpVM
    {
        [Required(ErrorMessage="First Name Required ")]
        [MinLength(5,ErrorMessage ="Min Length 5 ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage="Last Name Required ")]
        [MinLength(5,ErrorMessage ="Min Length 5 ")]
        public string LastName { get; set; }
        [Required(ErrorMessage="User Name Required ")]
        [MinLength(5,ErrorMessage ="Min Length 5 ")]
        public string UserName { get; set; }
        [Required(ErrorMessage="Email Required ")]
        [MinLength(5,ErrorMessage ="Min Length 5 ")]
        [EmailAddress(ErrorMessage ="invalid Email Format")]
        public string Email { get; set; }
        [Required(ErrorMessage="Password Required ")]
        [MinLength(5,ErrorMessage ="Min Length 8 ")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Passwords Is Not The Same ")]
        public string ConfirmPassword { get; set; }
        
    }
}
