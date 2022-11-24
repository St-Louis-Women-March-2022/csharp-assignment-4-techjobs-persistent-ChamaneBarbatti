using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistentAutograded.Data;
using TechJobsPersistentAutograded.Models;
using TechJobsPersistentAutograded.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistentAutograded.Controllers
{
   
    public class EmployerController : Controller
    {
        private JobRepository jobRepository;
        public EmployerController(JobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }
    

        // GET: /<controller>/
        public IActionResult Index()
        {
           IEnumerable<Employer> employers = jobRepository.GetAllEmployers();


           
            return View(employers);
        }

        public IActionResult Add()
        { 
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();
            return View(addEmployerViewModel);
        }

        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            if (ModelState.IsValid)
            {
                Employer employer = new Employer
                {
                    Name = addEmployerViewModel.Name,
                    Location = addEmployerViewModel.Location
                };
                jobRepository.AddNewEmployer(employer);
                jobRepository.SaveChanges();
                return Redirect("/Employer");

            }
            return View(addEmployerViewModel);
        }

        public IActionResult About(int id)
        {
            Employer employer = jobRepository.FindEmployerById(id);


            return View(employer);
        }
    }
}

