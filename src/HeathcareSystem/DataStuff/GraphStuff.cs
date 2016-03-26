using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.DataStuff
{
    public class GraphStuff
    {
        public List<object> SeedSicknessInProvince(HealthCareContext context)
        {
            var result = new List<object>();
            var provinces = new string[] { "Hồ Chí Minh", "Đồng Nai", "Bình Phước", "Bình Thuận", "Tây Ninh" };
            var diseases = context.Diseases.ToList();
            var random = new Random();
            foreach (var province in provinces) { }
            {
                var a = random.Next(19);
                var b = random.Next(a + 1, 20);
                var list = new List<object>();
                for (int i = a; i <= b; i++)
                {
                    list.Add(new
                    {
                        Name = diseases[i],
                        Ammount = random.Next(200)
                    });

                }
                result.Add(new
                {
                    Name = provinces,
                    Diseases = list
                });

            }
            var listDiseases = new List<object>();
            listDiseases.Add(new { Name = "Hypotension", Ammount = 100 });
            listDiseases.Add(new { Name = "Anemia", Ammount = 50 });
            listDiseases.Add(new { Name = "Asthma", Ammount = 24 });
            result.Add(new { Name = "Bình Dương", Diseases = listDiseases });

            return result;
        }
        public List<object> SeedLocationSickness(HealthCareContext context)
        {
            var result = new List<object>();
            var diseases = context.Diseases.ToList();
            var list = new List<object>();
            list.Add(new { Name = "Bến Cát", Ammount = 11 });
            list.Add(new { Name = "Phú Giáo", Ammount = 29 });
            list.Add(new { Name = "Tp.Thủ Dầu Một", Ammount = 23 });
            list.Add(new { Name = "Bàu Bàng", Ammount = 17 });
            list.Add(new { Name = "Dầu Tiếng", Ammount = 20 });
            result.Add(new { Name = "Hypotension", Provinces = list});

            list = new List<object>();
            list.Add(new { Name = "Bến Cát", Ammount = 15 });
            list.Add(new { Name = "Phú Giáo", Ammount = 14 });
            list.Add(new { Name = "Bàu Bàng", Ammount = 21 });
            result.Add(new { Name = "Anemia", Provinces = list});

            list = new List<object>();
            list.Add(new { Name = "Bến Cát", Ammount = 10 });
            list.Add(new { Name = "Phú Giáo", Ammount = 3 });
            list.Add(new { Name = "Bàu Bàng", Ammount = 7 });
            list.Add(new { Name = "Dầu Tiếng", Ammount = 4 });
            result.Add(new { Name = "Asthma", Provinces = list});

            return result;
        }
    }
}
