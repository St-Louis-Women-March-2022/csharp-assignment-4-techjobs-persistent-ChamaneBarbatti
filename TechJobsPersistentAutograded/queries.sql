--Part 1
/*describe jobs;

describe skills;

describe employers;

describe jobskills;*/


--Part 2

select * from techjobs.employers
where Location="St. Louis";


--Part 3
select  distinct skills.name,  skills.Description from jobs,skills, jobskills
where jobs.Id = jobskills.JobId and skills.Id= jobskills.SkillId 
order by skills.name; 
