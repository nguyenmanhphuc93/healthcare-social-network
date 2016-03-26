using System;
using HeathcareSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Healthcare.Models
{
    public class HealthCareContext : IdentityDbContext<User, IdentityRole<long>, long>, IHealthcareContext
    {

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DoctorInDepartment> DoctorInDepartments { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }


        public int SaveChange()
        {
            return base.SaveChanges();
        }

        public void SetState(object entity, EntityState state)
        {
            base.Entry(entity).State = state;
        }
    }
}