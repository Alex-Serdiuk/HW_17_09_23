using HW_17_09_23.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HW_17_09_23.Controllers
{
    public class AboutMeController : Controller
    {
		private readonly SiteDbContext _context;

		public AboutMeController(SiteDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
        {
			ViewData["Title"] = "AboutMe list";
			return View(_context.AboutMes.ToList());
        }

		[HttpGet]
		public ActionResult Create()
		{
			ViewData["Title"] = "Create About Me";
			return View(new AboutMe());
		}

		[HttpPost]
		public ActionResult Create([FromForm] AboutMe aboutMe)
		{
			if (!ModelState.IsValid)
			{
				return View(aboutMe);
			}

			_context.AboutMes.Add(aboutMe);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
            ViewData["Title"] = "Edit About Me";
            var aboutMe = _context.AboutMes.First(x => x.Id == id);
			return View(aboutMe);
		}

		[HttpPost]
		public ActionResult Edit(int id, [FromForm] AboutMe form)
		{
			if (!ModelState.IsValid)
			{
				return View(form);
			}

			var aboutMe = _context.AboutMes.First(x => x.Id == id);
			aboutMe.FirstName = form.FirstName;
			aboutMe.LastName = form.LastName;
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var group = _context.AboutMes.First(x => x.Id == id);
			_context.AboutMes.Remove(group);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		//[HttpGet]
		//public ActionResult Skill(int id)
		//{
		//	ViewData["Title"] = "Skills list";
		//	var aboutMe = _context.AboutMes.First(x => x.Id == id);
		//	return View(aboutMe);
		//}
	}
}
