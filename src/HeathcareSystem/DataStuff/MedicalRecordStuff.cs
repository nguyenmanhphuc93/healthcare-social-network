using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Healthcare.Models;
using HeathcareSystem.Models;

namespace HeathcareSystem.DataStuff
{
    public static class MedicalRecordStuff
    {
        public static void SeedMedicalRecord(this HealthCareContext context)
        {
            var medicines = SeedMedicines();
        }
        public static List<Medicine> SeedMedicines()
        {
            var drugs = new string[]
            { "Sertraline", "Fluoxetine","Citalopram", "Escitalopram", "Paroxetine", "Fluvoxamine",  "Trazodone", "Desvenlafaxine",   "Duloxetine", "Venlafaxine",
                "amitriptyline", "amoxapine", "clomipramine","desipramine","doxepin", "imipramine", "nortriptyline","protriptyline", "trimipramine"  };

            var random = new Random();
            var medicines = new List<Medicine>();
            var a = random.Next(drugs.Length - 3);
            var b = random.Next(a+1, drugs.Length - 1);
            for (var i = a; i <= b; i++)
            {
                medicines.Add(new Medicine()
                {
                    Name = drugs[i],
                    Quantity = $"{random.Next(1, 5)}/Day"
                });
            }
            return medicines;
        }
        public static void SeedPrescription(this HealthCareContext context)
        {
            var medicines = SeedMedicines();
            context.Prescriptions.Add(new Prescription()
            {
                Medicines = medicines
            });
            context.SaveChange();
        }
        public static List<MedicalResult> SeedMedicalResult(HealthCareContext context)
        {
            var diseases = context.Diseases.ToList();
            var random = new Random();
            var status = new string[] { "Danger", "Normal", "Serious", "Not serious" };
            var a = random.Next(diseases.Count - 2);
            var b = random.Next(a + 1, diseases.Count - 1);

            var medicalResults = new List<MedicalResult>();
            for (var i = a; i <= b; i++)
            {
                medicalResults.Add(new MedicalResult()
                {
                    Disease = diseases[i],
                    DiseaseId = diseases[i].Id,
                    Status = status[random.Next(4)]
                });
            }
            return medicalResults;
        }


    }
}
