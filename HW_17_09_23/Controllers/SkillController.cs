using HW_17_09_23.Models;
using HW_17_09_23.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

			AboutMeViewModel aboutMeViewModel = new AboutMeViewModel
			{
				AboutMe = _context.AboutMes.First(x => x.Id == id),
				Skills = _context.Skills
                .Include(s => s.SkillName)
                .ThenInclude(sn => sn.Image)
                .Where(x => x.AboutMe.Id == id)
                .ToList()
			};
            
			
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
			var skillViewModel = new SkillForm();

			if (id != null)
			{
				skillViewModel.AboutMeId = id;
			}

			return View(skillViewModel);
		}

        [HttpPost]
        public ActionResult Create([FromForm] SkillForm model)
        {
            if (!ModelState.IsValid)
            {
    //            // Перевірка, чи обраний SkillName існує
    //            if (model.SelectedSkillNameId != null)
    //            {
				//	// Вибір існуючого SkillName
				//	var skillName = _context.SkillNames.Find(model.SelectedSkillNameId);
    //                if (skillName != null)
    //                {
				//		var skill = new Skill
				//		{
    //                        AboutMe = _context.AboutMes.First(x => x.Id == model.AboutMeId),
				//			SkillName = skillName,
				//			Percentage = model.Percentage
				//		};
				//		_context.Skills.Add(skill);
				//		_context.SaveChanges();
				//	}
				//}
				return RedirectToAction("Index", new { id = model.AboutMeId });
			}
			//// Створення нового SkillName
			//var newSkillName = new SkillName
			//{
			//	Name = model.NewSkillName
			//};
			//_context.SkillNames.Add(newSkillName);
			//         _context.SaveChanges();

			//var newSkill = new Skill
			//{
			//	AboutMe = _context.AboutMes.First(x => x.Id == model.AboutMeId),
			//	SkillName = newSkillName,
			//	Percentage = model.Percentage
			//};

			//_context.Skills.Add(newSkill);
			//_context.SaveChanges();

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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewData["Title"] = "Edit Skill";

			// Отримайте існуючий Skill за його Id
			//var skill = _context.Skills.Include(s => s.SkillName).FirstOrDefault(s => s.Id == id);
			var skill = _context.Skills
	                    .Include(s => s.SkillName)
	                    .Include(s => s.AboutMe)
	                    .FirstOrDefault(s => s.Id == id);
			if (skill == null)
            {
                return NotFound();
            }

            var selectedSkillNameId = skill.SkillName.Id;

            // Отримайте всі SkillName для випадаючого списку
            var skillNames = _context.SkillNames.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            // Якщо SkillName вже існує у списку, додайте його до випадаючого списку
            if (skill.SkillName != null)
            {
                skillNames.Insert(0, new SelectListItem
                {
                    Value = skill.SkillName.Id.ToString(),
                    Text = skill.SkillName.Name,
                    Selected = true // Встановіть вибране значення для наявного SkillName
                });
            }

            var skillViewModel = new SkillForm
            {
                AboutMeId =skill.AboutMe.Id,
                //Id = skill.Id.Value,
                SelectedSkillNameId = skill.SkillName.Id,
                Percentage = skill.Percentage,
                SkillNameList = skillNames
            };

            return View(skillViewModel);

            //var skill = _context.Skills.First(x => x.Id == id);
            //return View(skill);
        }

        [HttpPost]
        public ActionResult Edit(int id, SkillForm model)
        {
            // Отримайте існуючий Skill за його Id
            var skill = _context.Skills.Include(s => s.SkillName).FirstOrDefault(s => s.Id == id);
            //var skill = _context.Skills.First(x => x.Id == id);
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
            //// Створення нового SkillName
            //var newSkillName = new SkillName
            //{
            //    Name = model.NewSkillName
            //};
            //_context.SkillNames.Add(newSkillName);
            //_context.SaveChanges();

            //skill.SkillName = newSkillName;
            //skill.Percentage = model.Percentage;
            
            //_context.SaveChanges();
            return RedirectToAction("Index", new { id = model.AboutMeId });
        }

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var skill = _context.Skills.Include(s => s.AboutMe).First(x => x.Id == id);
            var AboutMeId = skill.AboutMe.Id;
			_context.Skills.Remove(skill);
			_context.SaveChanges();
			return RedirectToAction("Index", new { id = AboutMeId });
		}
	}
}
