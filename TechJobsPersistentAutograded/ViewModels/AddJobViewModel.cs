using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechJobsPersistentAutograded.Models;

namespace TechJobsPersistentAutograded.ViewModels
{
    public class AddJobViewModel
    {

       public string Name { get; set; }
       public int EmployerId { get; set; }

       public string SkillId { get; set; }
       public List<Skill> Skills { get; set; }
       public List <SelectListItem> Employers { get; set; }
       
        public AddJobViewModel()
        {

        }
        public AddJobViewModel(IEnumerable<Employer> employers,List<Skill> skills)
        {
            List<SelectListItem> tempEmployers = new List<SelectListItem>();
            
            foreach(var employer in employers)
            {
                tempEmployers.Add(new SelectListItem(employer.Name, employer.Id.ToString()));
            }
            Employers = tempEmployers;
            Skills = skills;

        }

    }
}
