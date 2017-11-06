using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class HospitalContext : DbContext
    {
        public HospitalContext() : base("Hospital")
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DiseaseRecord> BurnDiseases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasMany(c => c.Patients)
                .WithMany(s => s.Doctors)
                .Map(t => t.MapLeftKey("DoctorId")
                .MapRightKey("PatientId")
                .ToTable("PatientDoctor"));
        }
    }
}