using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class EditDataDoctor
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name can not be empty")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 12 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Specialization can not be empty")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 15 characters")]
        public string Specialization { get; set; }

        [StringLength(12, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 12 characters")]
        [Required(ErrorMessage = "Login can not be empty")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password can not be empty")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 12 characters")]
        public string Password { get; set; }
    }
}