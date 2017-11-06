using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Patient : Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string OutpatientCard { get; set; }
        public virtual ICollection<DiseaseRecord> Sickness { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}