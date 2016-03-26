using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.DataStuff
{
    public class GraphStuff
    {
        public object SeedSicknessInProvince()
        {
            var result = new object();
            var provinces = new string[] { "Hồ Chí Minh", "Đồng Nai", "Bình Phước", "Bình Thuận", "Tây Ninh" };
           
            var listDiseases = new List<object>();
            listDiseases.Add(new { Name = "Hypotension", Ammount = 100 });
            listDiseases.Add(new { Name = "Anemia", Ammount = 50 });
            listDiseases.Add(new { Name = "Asthma", Ammount = 24 });
            listDiseases.Add(new { Name = "Road accident", Ammount = 74 });
            listDiseases.Add(new { Name = "Pestilence", Ammount = 42 });

            result = new { Name = "Bình Dương", Diseases = listDiseases };

            return result;
        }
        public object SeedLocationSickness()
        {
            var result = new object();
            var list = new List<object>();
            list.Add(new { Name = "Bến Cát", Ammount = 11 });
            list.Add(new { Name = "Phú Giáo", Ammount = 29 });
            list.Add(new { Name = "Tp.Thủ Dầu Một", Ammount = 23 });
            list.Add(new { Name = "Bàu Bàng", Ammount = 17 });
            list.Add(new { Name = "Dầu Tiếng", Ammount = 20 });
            result = new { Name = "Hypotension", Provinces = list};
            return result;
        }
    }
}
