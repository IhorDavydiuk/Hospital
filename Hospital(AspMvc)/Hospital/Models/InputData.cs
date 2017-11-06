using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class InputData : IValidatableObject
    {
        HospitalContext context = new HospitalContext();
        [Required(ErrorMessage = "Login can not be empty")]
        public string Login { get; set; }
        [UIHint("Password")]
        [Required(ErrorMessage = "Password can not be empty")]
        public string Password { get; set; }
        [Display(Name = "I'm doctor")]
        public bool IsDoctor { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsDoctor)
            {
                if (!context.Doctors.Any(doc => doc.Login == Login))
                {
                    yield return new ValidationResult("Login is incorrect",
                        new string[] { "Login" });
                }
                if (!context.Doctors.Any(doc => doc.Password == Password))
                {
                    yield return new ValidationResult("Password is incorrect",
                        new string[] { "Password" });
                }
            }
            else
            {
                if (!context.Patients.Any(pat => pat.Login == Login))
                {
                    yield return new ValidationResult("Login is incorrect",
                        new string[] { "Login" });
                }
                if (!context.Patients.Any(pat => pat.Password == Password))
                {
                    yield return new ValidationResult("Password is incorrect",
                        new string[] { "Password" });
                }
            }
        }
        public int GetId()
        {
            if (IsDoctor)
            {
                return context.Doctors.First(doc => doc.Login == Login).Id;
            }
            else
            {
                return context.Patients.First(doc => doc.Login == Login).Id;
            }
        }
    }
}