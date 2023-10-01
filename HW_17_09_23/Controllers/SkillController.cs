using HW_17_09_23.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW_17_09_23.Controllers
{
    public class SkillController : Controller
    {
        private readonly SiteDbContext _context;

        public SkillController(SiteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            ViewData["Title"] = "Skills list";
			// Используйте Distinct(), чтобы избежать дублирования SkillName
			var skillNameIdsInSkills = _context.Skills
				.Where(x => x.AboutMe.Id == id)
				.Select(x => x.SkillName.Id)
				.Distinct()
				.ToList();

			AboutMeViewModel aboutMeViewModel = new AboutMeViewModel
			{
				AboutMe = _context.AboutMes.First(x => x.Id == id),
				Skills = _context.Skills.Where(x => x.AboutMe.Id == id).ToList(),
				SkillNames = _context.SkillNames
					.Where(x => skillNameIdsInSkills.Contains(x.Id))
					.ToList()
			};
            
			//aboutMe.Skills = _context.Skills.Where(x => x.AboutMe.Id == aboutMe.Id);
            return View(aboutMeViewModel);
        }
        //public IActionResult Index()
        //{
           
        //    return View();
        //}

        [HttpGet]
        public ActionResult Create(int id)
        {
			ViewData["Title"] = "Create Skill";

			// Отримайте дані для випадаючого списку SkillName з бази даних
			var skillNames = _context.SkillNames.Select(s => new SelectListItem
			{
				Value = s.Id.ToString(),
				Text = s.Name
			});

			// Додайте дані до ViewData
			ViewData["SelectedSkillNameId"] = skillNames;
			var skillViewModel = new SkillViewModel();

			if (id != null)
			{
				skillViewModel.AboutMeId = id;
			}

			return View(skillViewModel);
		}

        [HttpPost]
        public ActionResult Create(SkillViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Перевірка, чи обраний SkillName існує
                if (model.SelectedSkillNameId != null)
                {
					// Вибір існуючого SkillName
					var skillName = _context.SkillNames.Find(model.SelectedSkillNameId);
                    if (skillName != null)
                    {
						var skill = new Skill
						{
                            AboutMe = _context.AboutMes.First(x => x.Id == model.AboutMeId),
							SkillName = skillName,
							Percentage = model.Percentage
						};
						_context.Skills.Add(skill);
						_context.SaveChanges();
					}
				}
				return RedirectToAction("Index", new { id = model.AboutMeId });
			}
			// Створення нового SkillName
			var newSkillName = new SkillName
			{
				Name = model.NewSkillName
			};
			_context.SkillNames.Add(newSkillName);
            _context.SaveChanges();

			var newSkill = new Skill
			{
				AboutMe = _context.AboutMes.First(x => x.Id == model.AboutMeId),
				SkillName = newSkillName,
				Percentage = model.Percentage
			};

			_context.Skills.Add(newSkill);
			_context.SaveChanges();
			return RedirectToAction("Index", new { id = model.AboutMeId });
		}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var skill = _context.Skills.First(x => x.Id == id);
            return View(skill);
        }

        [HttpPost]
        public ActionResult Edit(int id, SkillViewModel model)
        {
            var skill = _context.Skills.First(x => x.Id == id); ;
            if (!ModelState.IsValid)
            {
                // Перевірка, чи обраний SkillName існує
                if (model.SelectedSkillNameId != null)
                {
                    // Вибір існуючого SkillName
                    var skillName = _context.SkillNames.Find(model.SelectedSkillNameId);
                    if (skillName != null)
                    {
                        
                        skill.SkillName = skillName;
                        skill.Percentage = model.Percentage;
                        _context.SaveChanges();
                    }
                }
                return RedirectToAction("Index", new { id = model.AboutMeId });
            }
            // Створення нового SkillName
            var newSkillName = new SkillName
            {
                Name = model.NewSkillName
            };
            _context.SkillNames.Add(newSkillName);
            _context.SaveChanges();

            skill.SkillName = newSkillName;
            skill.Percentage = model.Percentage;
            
            _context.SaveChanges();
            return RedirectToAction("Index", new { id = model.AboutMeId });
        }
    }
}
