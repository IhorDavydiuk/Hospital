using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class DiseaseRecord
    {
        HospitalContext context = new HospitalContext();

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Illness { get; set; }
        [Required]
        [UIHint("MultilineText")]
        public string Deskription { get; set; }
        public int DoctorId { get; set; }
        [Required]
        [Display(Name= "Outpatient Card")]
        public string OutpatientCard { get; set; }
        public StateOfHealth Status { get; set; }

        public bool IsOutpatientCatd()
        {
            return context.Patients.Any(pat => pat.OutpatientCard == OutpatientCard);
        }
    }
}