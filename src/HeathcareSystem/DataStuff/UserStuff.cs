using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.DataStuff
{
    public static class UserStuff
    {
        public static void SeedUser(this HealthCareContext context, Profile profile, string email, string userName)
        {
            var user = new User()
            {
                ProfileId = profile.Id,
                UserName = userName,
                Profile = profile,
            };
            context.Users.Add(user);
            context.SaveChange();
        }
        public static void SeedProfile(this HealthCareContext context, int count = 50)
        {
            var mobiphones = SeedPhone(count);
            var homePhones = SeedPhone(count);
            for(var i = 0; i<count; i++)
            {
                context.Profiles.Add(new Profile()
                {
                    FirstName = $"Văn {(char)('A' + i)}",
                    LastName = $"Nguyễn",
                    DisplayName = $"Văn {(char)('A' + i)}",
                    MobiPhone = mobiphones[i],
                    HomePhone = homePhones[i]
                    
                });
            }
            context.SaveChange();
        }
        public static List<string> SeedPhone(int count = 50)
        {
            var phones = new List<string>();
            var random = new Random();
            
            for(var i = 0; i< count; i++)
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
