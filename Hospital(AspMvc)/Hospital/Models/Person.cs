using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class Person
    {
        public HospitalContext context = new HospitalContext();

        [StringLength(12, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 12 characters")]
        [Required(ErrorMessage = "Login can not be empty")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password can not be empty")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 12 characters")]
        public string Password { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsDoctor { get; set; }
    }
}