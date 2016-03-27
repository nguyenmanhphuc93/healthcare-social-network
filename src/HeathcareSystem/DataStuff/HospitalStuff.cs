using Healthcare.Models;
using HeathcareSystem.Models;
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
            context.SeedHospital();
            var department = new string[]
            {
                "Biochemistry",
                "Cardiovascular surgery",
                "Obstetrics & Gynecology",
                "Labor &Delivery suite",
            };
            var hospitals = context.Hospitals.ToList();
            foreach (var hospital in hospitals)
            {
                foreach (var item in department)
                {
                    context.Departments.Add(new Department()
                    {
                        Name = item,
                        HospitalId = hospital.Id
                    });
                }
            }

            context.SaveChange();
        }
        public static void SeedDisease(this HealthCareContext context)
        {
            var diseases = new string[] {"Fever",
                                         "Allergy",
                                         "Nausea",
                                         "Vomit",
                                         "Asthma",
                                         "Chest pain",
                                         "Dyspnea",
                                         "Influenza",
                                         "Anemia",
                                         "Hypertension",
                                         "Hypotension",
                                         "Myocardial ìnarction",
                                         "Appendicitis",
                                         "Constipation",
                                         "Diarrhoea",
                                         "Coma",
                                         "Diabetes",
                                         "Faint",
                                         "Angina",
                                         "Chicke - pox",
                                         "Fracture",
                                         "Scald",
                                         "Pestilence",
                                         "Road accident"};
            foreach (var item in diseases)
            {
                context.Diseases.Add(new Disease()
                {
                    Name = item
                });
            }
            context.SaveChange();
        }
        private static void SeedHospital(this HealthCareContext context)
        {
            var provinces = new string[] { "Hồ Chí Minh", "Bình Dương", "Đồng Nai", "Bình Phước" };
            var departments = context.Departments.ToList();

            for (var i = 0; i < 6; i++)
            {
                context.Hospitals.Add(new Hospital()
                {
                    Name = $"Hospital {i}",
                    Address = provinces[i / 2],
                });
            }
            context.SaveChange();
        }

        public static List<Profile> SeedDoctor(this UserManager<User> _userManager, HealthCareContext context, int count = 20)
        {
            var result = new List<Profile>();
            for (var i = 0; i < count; ++i)
            {
                var profile = new Profile { DisplayName = $"Doctor{i}" };
                context.Profiles.Add(profile);
                context.SaveChange();
                result.Add(profile);
                _userManager.CreateUser(profile, profile.DisplayName, "123456", "Doctor");
            }
            return result;
        }

        public static void SeedDoctoc(this UserManager<User> _userManager, List<Profile> profiles)
        {
            for (var i = 0; i < profiles.Count; i++)
            {
                _userManager.CreateUser(profiles[i], $"Doctor{i}", "123456", "Doctor");
            }
        }
        public static void SeedPharmacist(this UserManager<User> _userManager, List<Profile> profiles)
        {
            for (var i = 0; i < profiles.Count; i++)
            {
                _userManager.CreateUser(profiles[i], $"Pharmacist{i}", "123456", "Pharmacist");
            }
        }

        //public static void AddDoctor
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
