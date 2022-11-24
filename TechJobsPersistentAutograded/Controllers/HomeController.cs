using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistentAutograded.Models;
using TechJobsPersistentAutograded.ViewModels;
using TechJobsPersistentAutograded.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistentAutograded.Controllers
{

    public class HomeController : Controller

    {
        private JobRepository _repo;

        public HomeController(JobRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("/")]
        [Route("/Index")]
        public IActionResult Index()

        {
            IEnumerable<Job> jobs = _repo.GetAllJobs();

            return View(jobs);
        }


        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            AddJobViewModel addJobViewModel = new AddJobViewModel(_repo.GetAllEmployers(),new List<Skill>(_repo.GetAllSkills()));
            return View(addJobViewModel);
        }

        [HttpPost]
        
        [Route("/ProcessAddJobForm")]
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Job job = new Job
                {
                    Name = addJobViewModel.Name
                };
                foreach(var skill in selectedSkills)
                {
                    JobSkill jobSkill = new JobSkill();
                    jobSkill.SkillId = Int32.Parse(skill);
                    jobSkill.Job = job;
                    _repo.AddNewJobSkill (jobSkill);

                }
                job.EmployerId = addJobViewModel.EmployerId;
                _repo.AddNewJob(job);
                _repo.SaveChanges();
                return Redirect("Index");
            }

            return View(addJobViewModel);
        }


        public IActionResult Detail(int id)
        {
            Job theJob = _repo.FindJobById(id);

            List<JobSkill> jobSkills = _repo.FindSkillsForJob(id).ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }

    }

}


