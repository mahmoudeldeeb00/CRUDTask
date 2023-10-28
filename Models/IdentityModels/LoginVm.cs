using System.ComponentModel.DataAnnotations;

namespace Task.Models.IdentityModels
{
    public class LoginVM
    {
        [Required(ErrorMessage="Email Required ")]
       
        [MinLength(8,ErrorMessage ="min length 8 ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required ")]
        [MinLength(8, ErrorMessage = "min length 8 ")]
        public string Password { get; set; }

        public bool Remmember { get; set; }
      
    }
}
