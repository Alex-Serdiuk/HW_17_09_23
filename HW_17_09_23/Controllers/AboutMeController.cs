using HW_17_09_23.Models;
using HW_17_09_23.Models.Forms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HW_17_09_23.Controllers
{
	
	public class AboutMeController : Controller
	{
		private readonly SiteDbContext _context;
		private readonly IWebHostEnvironment _environment;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;

        public AboutMeController(SiteDbContext context, IWebHostEnvironment environment, ILogger<HomeController> logger, UserManager<User> userManager)
		{
			_context = context;
			_environment = environment;
            _logger = logger;
            _userManager = userManager;
        }

		public async Task<IActionResult> Index()
		{
          

            _logger.LogCritical("Some critical error !!!!!");
            _logger.LogError("Some error !!!!!");

            ViewData["Title"] = "AboutMe list";

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
               
                ViewData["Roles"] = (await _userManager.GetRolesAsync(user));

            }

            return View(_context.AboutMes.Include(x => x.Image).ToList());
		}

        [Authorize]
        [HttpGet]
		public ActionResult Create()
		{
			ViewData["Title"] = "Create About Me";
			return View(new AboutMe());
		}

        [Authorize]
        [HttpPost]
		public async Task<ActionResult> Create([FromForm] AboutMe aboutMe, IFormFile? image)
		{
			if (!ModelState.IsValid)
			{
				return View(aboutMe);
			}

			if (image != null)
			{
				aboutMe.Image = await Upload(image);
			}

			_context.AboutMes.Add(aboutMe);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

        [Authorize]
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

        [Authorize]
        [HttpGet]
		public ActionResult Edit(int id)
		{
			ViewData["Title"] = "Edit About Me";
			var aboutMe = _context.AboutMes.Include(x => x.Image).First(x => x.Id == id);
			return View(aboutMe);
		}

        [Authorize]
        [HttpPost]
		public async Task<ActionResult> Edit(int id, [FromForm] AboutMeForm form)
		{
			if (!ModelState.IsValid)
			{
				return View(form);
			}

			var aboutMe = await _context.AboutMes.Include(x => x.Image).FirstAsync(x => x.Id == id);
			aboutMe.FirstName = form.FirstName;
			aboutMe.LastName = form.LastName;
			aboutMe.Email = form.Email;
			aboutMe.Phone = form.Phone;
			aboutMe.Info = form.Info;

			if (form.Image != null)
			{
				if (aboutMe.Image != null)
				{
					//delete old
					var localFilename = Path.Combine(_environment.WebRootPath, "uploads", aboutMe.Image.FileName);
					System.IO.File.Delete(localFilename);
					_context.Images.Remove(aboutMe.Image);
				}

				aboutMe.Image = await Upload(form.Image);
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

        [Authorize]
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

		[HttpGet]
		public IActionResult Profile(int id)
		{
			ViewData["Title"] = "Skills list";

			AboutMeViewModel aboutMeViewModel = new AboutMeViewModel
			{
				AboutMe = _context.AboutMes.Include(x => x.Image).First(x => x.Id == id),
				Skills = _context.Skills
				.Include(s => s.SkillName)
				.ThenInclude(sn => sn.Image)
				.Where(x => x.AboutMe.Id == id)
				.ToList()
			};


			return View(aboutMeViewModel);
		}
	}
}
