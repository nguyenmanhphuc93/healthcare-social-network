using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;
using HeathcareSystem.ViewModels;
using Healthcare.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HeathcareSystem.Controllers
{
    [Authorize]
    public class HospitalController : BaseController
    {
        public HospitalController(IHealthcareContext context) : base(context) { }
        // GET: api/values
        [HttpGet]
        public IActionResult GetHospitals()
        {
            var hospitals = context.Hospitals.Include(n => n.Departments).ToList();
            var dic = new Dictionary<string, List<HospitalViewmodel>>();
            var doctorsInDeparmtents = new Dictionary<int, List<ProfileViewmodel>>();
            var doctors = context.DoctorInDepartments.Include(n => n.Doctor).ToList();
            foreach (var d in doctors)
            {
                var id = d.DepartmentId;
                List<ProfileViewmodel> viewmodels;
                var r = doctorsInDeparmtents.TryGetValue(id, out viewmodels);
                if (!r)
                {
                    viewmodels = new List<ProfileViewmodel>();
                }
                viewmodels.Add(new ProfileViewmodel(d.Doctor));
                doctorsInDeparmtents[id] = viewmodels;
            }

            foreach (var hospital in hospitals)
            {
                var address = hospital.Address;
                List<HospitalViewmodel> viewmodels;
                var r = dic.TryGetValue(address, out viewmodels);
                if (!r)
                {
                    viewmodels = new List<HospitalViewmodel>();
                }
                var hospitalViewModel = new HospitalViewmodel() { Name = hospital.Name, Id = hospital.Id, Departments = new List<DepartmentViewmodel>() };

                var departments = hospital.Departments.ToList();
                int count = departments.Count;
                for (var i = 0; i < count; ++i)
                {
                    var doctorViewModels = new List<ProfileViewmodel>();
                    doctorsInDeparmtents.TryGetValue(departments[i].Id, out doctorViewModels);
                    var department = new DepartmentViewmodel() { Name = departments[i].Name, Id = departments[i].Id };
                    department.Doctors = doctorViewModels;
                    hospitalViewModel.Departments.Add(department);
                }
                viewmodels.Add(hospitalViewModel);
                dic[address] = viewmodels;

            }
            var result = new List<HospitalInProvinceViewModel>();
            foreach (var p in dic.Keys)
            {
                result.Add(new HospitalInProvinceViewModel { Province = p, Hospitals = dic[p] });
            }

            return Ok(result);


        }

    }
}
