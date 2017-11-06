using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class Doctor : Person, IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name can not be empty")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 12 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specialization can not be empty")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 15 characters")]
        public string Specialization { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (context.Doctors.Any(doc => doc.Login == Login))
            {
                yield return new ValidationResult("This login already exists",
                    new string[] { "Login" });
            }
        }
    }
}
