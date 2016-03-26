using HeathcareSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;


namespace Healthcare.Models
{
    public interface IHealthcareContext
    {
        DbSet<Appointment> Appointments { get; set; }
        DbSet<Hospital> Hospitals { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<DoctorInDepartment> DoctorInDepartments { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<IdentityRole<long>> Roles { get; set; }
        DbSet<Disease> Diseases { get; set; }
        DbSet<Profile> Profiles { get; set; }
        DbSet<AppointmentRequest> AppointmentRequests { get; set; }
        DbSet<Examination> Examinations { get; set; }
        int SaveChange();
        void SetState(object entity, EntityState state);
    }
}