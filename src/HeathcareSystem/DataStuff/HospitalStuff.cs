using Healthcare.Models;
using Microsoft.AspNet.Identity;
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
                "Pediatric clinic",
                "Rehabilitation"
            };
            foreach (var item in department)
            {
                context.Departments.Add(new Department()
                {
                    Name = item
                });
            }
            context.SaveChange();
        }   
        public static void SeedDisease()
        {

        } 
        public static void SeedHospital(this HealthCareContext context)
        {
            var provinces = new string[] { "Hồ Chí Minh", "Bình Dương", "Đồng Nai", "Bình Phước", "Bình Thuận", "Tây Ninh" };
            var departments = context.Departments.ToList();

            for(var i = 0; i < 10; i++)
            {
                List<Department> des = new List<Department>();
                for(var j = 0; j< 5; j++)
                {
                    des.Add(departments[i + j]);
                }

                context.Hospitals.Add(new Hospital()
                {
                    Name = $"Hospital{i}",
                    Address = provinces[i / 2],
                    Departments = des            
                });
            }
            context.SaveChange();
        }
        public static void SeedDoctoc(this UserManager<User> _userManager, List<Profile> profiles)
        {
           for(var i = 0; i<profiles.Count; i++)
            {
                _userManager.CreateUser(profiles[i], $"Doctor{i}", "123456", "Doctor");
            }
        }
        public static void SeedPatient(this UserManager<User> _userManager, List<Profile> profiles)
        {
            for (var i = 0; i < profiles.Count; i++)
            {
                _userManager.CreateUser(profiles[i], $"Patient{i}", "123456", "Patient");
            }
        }
        public static void SeedManager(this UserManager<User> _userManager, List<Profile> profiles)
        {
            for (var i = 0; i < profiles.Count; i++)
            {
                _userManager.CreateUser(profiles[i], $"Manager{i}", "123456", "Manager");
            }
        }
        
    }
}
