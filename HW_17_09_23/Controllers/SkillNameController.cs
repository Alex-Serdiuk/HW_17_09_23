using HW_17_09_23.Models;
using HW_17_09_23.Models.Forms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HW_17_09_23.Controllers
{
    [Authorize]
    public class SkillNameController : Controller
    {
		private readonly SiteDbContext _context;
		private readonly IWebHostEnvironment _environment;

		public SkillNameController(SiteDbContext context, IWebHostEnvironment environment)
		{
			_context = context;
			_environment = environment;
		}

		public IActionResult Index()
        {
			ViewData["Title"] = "SkillNames list";
			var returnUrl = HttpContext.Request.Headers["Referer"].ToString();
			HttpContext.Response.Cookies.Append("editSkill-return-url", returnUrl, new CookieOptions
			{
				Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0))
			});
			@ViewData["Referer"] = HttpContext.Request.Cookies["editSkill-return-url"]?.ToString();
            return View(_context.SkillNames.Include(x => x.Image).ToList());
        }

		[HttpGet]
		public ActionResult Create()
		{
			ViewData["Title"] = "Create SkillName";
			return View(new SkillName());
		}

		[HttpPost]
		public async Task<ActionResult> Create([FromForm] SkillName skillName, IFormFile? image)
		{
			if (!ModelState.IsValid)
			{
				return View(skillName);
			}

			if (image != null)
			{
				skillName.Image = await Upload(image);
			}

			_context.SkillNames.Add(skillName);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");

		}

		[HttpPost]
		public async Task<ImageFile> Upload(IFormFile file)
		{
			// 6B29FC40-CA47-1067-b31d-00dd010662da         ".png"
			// 6B29FC40-CA47-1067-b31d-00dd010662da.png"
			var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

			// <img src="/uploads/6B29FC40-CA47-1067-b31d-00dd010662da.png">

			var dbFile = new ImageFile()
			{
				FileName = filename,
			};

			// C:\Users\kvvkv\source\repos\VPD121AspNet\VPD121AspNet\wwwroot\uploads\6B29FC40-CA47-1067-b31d-00dd010662da.png
			var localFilename =
				Path.Combine(_environment.WebRootPath, "uploads", dbFile.FileName);

			using (var localFile = System.IO.File.Open(localFilename, FileMode.OpenOrCreate))
			{
				await file.CopyToAsync(localFile);
			}

			_context.Images.Add(dbFile);
			await _context.SaveChangesAsync();
			return dbFile;
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			ViewData["Title"] = "Edit SkillName";
			var skillName = _context.SkillNames.Include(x => x.Image).First(x => x.Id == id);
			return View(skillName);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(int id, [FromForm] SkillNameForm form)
		{
			if (!ModelState.IsValid)
			{
				return View(form);
			}

			var skillName = await _context.SkillNames.Include(x => x.Image).FirstAsync(x => x.Id == id);
			skillName.Name = form.Name;

			if (form.Image != null)
			{
				if (skillName.Image != null)
				{
					//delete old
					var localFilename = Path.Combine(_environment.WebRootPath, "uploads", skillName.Image.FileName);
					System.IO.File.Delete(localFilename);
					_context.Images.Remove(skillName.Image);
				}

				skillName.Image = await Upload(form.Image);
			}


			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var group = _context.SkillNames.First(x => x.Id == id);
			_context.SkillNames.Remove(group);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
