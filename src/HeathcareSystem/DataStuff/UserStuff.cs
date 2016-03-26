using Healthcare.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.DataStuff
{
    public static class UserStuff
    {
        public static void CreateUser(this UserManager<User> _usermanager, Profile profile, string userName, string password, String role)
        {
            var user = new User()
            {
                ProfileId = profile.Id,
                UserName = userName,
                Profile = profile,
            };
            _usermanager.CreateAsync(user, password).Wait();
            _usermanager.AddToRoleAsync(user, role).Wait();


        }
        public static List<Profile> SeedProfile(this HealthCareContext context, int count = 20)
        {
            //var mobiphones = SeedPhone(count);
            //var homePhones = SeedPhone(count);
            var profiles = new List<Profile>();
            for (var i = 0; i < count; i++)
            {
                profiles.Add(new Profile
                {
                    FirstName = $"Văn {(char)('A' + i)}",
                    LastName = $"Nguyễn",
                    DisplayName = $"Văn {(char)('A' + i)}",
                    //MobiPhone = mobiphones[i],
                    //HomePhone = homePhones[i]

                });
            }
            context.AddRange(profiles);


            context.SaveChange();
            return profiles;
        }

        public static List<string> SeedPhone(int count = 50)
        {
            var phones = new List<string>();
            var random = new Random();

            for (var i = 0; i < count; i++)
            {
                var phone = "0";
                for (var j = 0; j < 10; i++)
                {
                    var digit = random.Next(10);
                    phone += digit;
                }
                phones.Add(phone);
            }
            return phones;
        }
    }
}
