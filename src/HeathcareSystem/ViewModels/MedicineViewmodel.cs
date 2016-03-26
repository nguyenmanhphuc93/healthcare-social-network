using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class MedicineViewmodel
    {
        public MedicineViewmodel(Medicine medicine)
        {
            this.Id = medicine.Id;
            this.Name = medicine.Name;
            this.Quantity = medicine.Quantity;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
    }
}
