using HW_17_09_23.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_17_09_23.Controllers
{
    public class SkillController : Controller
    {
        private readonly SiteDbContext _context;

        public SkillController(SiteDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Skills list";
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewData["Title"] = "Create Skill";
            return View(new Skill());
        }

        [HttpPost]
        public ActionResult Create([FromForm] Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return View(skill);
            }

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var skill = _context.Skills.First(x => x.Id == id);
            return View(skill);
        }
    }
}
