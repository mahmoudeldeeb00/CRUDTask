using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Task.DAl.Entities;

namespace TaskProject.Models
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage=" First Name Reqired ")]
        [MinLength(5,ErrorMessage ="Min Lenght 5 ")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = " Last Name Reqired ")]
        [MinLength(5, ErrorMessage = "Min Lenght 5 ")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = " National ID Reqired ")]
        [StringLength(14, ErrorMessage = "Nathional Id Must be 14 Digits ") , MinLength(14,ErrorMessage = "Nathional Id Must be 14 Digits")]
        [RegularExpression("^[2-3][0-9]*", ErrorMessage ="Enter A valid National Id")]
        public string NationalId { get; set; } = null!;
        [Required(ErrorMessage = " Phone Reqired ")]
        [StringLength(11, ErrorMessage = "Phone"), MinLength(11,ErrorMessage = "Phone must be 11 digits ")]
        [RegularExpression("^[0-0][0-1][0-5][0-9]*", ErrorMessage = "Enter A valid Phone Number")]
        public string? Telephone { get; set; }
        [Required(ErrorMessage ="Salary required")]
        [Range(500,1000000 , ErrorMessage ="Salary must be more than 500 ")]
        public decimal? Salary { get; set; }
        [Required(ErrorMessage ="Village Required")]
        public int VillageId { get; set; }
        [ValidateNever]
        public string VillageName { get; set; } 
        [ValidateNever]

        public string CityName { get; set; } [ValidateNever]
        public string DovernateName { get; set; }

    }
}
