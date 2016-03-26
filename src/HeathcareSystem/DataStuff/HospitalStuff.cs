using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.DataStuff
{
    public static class HospitalStuff
    {
        public static void SeedDepartment(this HealthCareContext context)
        {
            var department = new string[]
            {
                "Biochemistry",
                "Cardiovascular surgery",
                "Obstetrics & Gynecology",
                "Obstetrics & Gynecology clinic",
                "Labor &Delivery suite",
                "Intensive care & Antitoxin",
                "Laboratory department",
                "Infection control",
                "Internal medicine clinic",
                "Pediatric clinic"
            };
            foreach (var item in department)
            {
                context.Departments.Add(new Department()
                {
                    Name = item
                });
            }
        }   
        public static void SeedDisease()
        {

        } 
        public static void SeedHospital()
        {

        }
        public static void SeedDoctoc()
        {

        }
        public static void SeedPatient()
        {

        }
        public  static void SeedManager()
        {

        }
    }
}
