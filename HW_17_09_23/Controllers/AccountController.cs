using HW_17_09_23.Models;
using HW_17_09_23.Models.Forms;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HW_17_09_23.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole<int>> _roleManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View(new LoginForm());
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromForm] LoginForm form)
		{
			if (!ModelState.IsValid)
			{
				return View(form);
			}

			var user = await _userManager.FindByEmailAsync(form.Login);

			if (user == null)
			{
				ModelState.AddModelError(nameof(form.Login), "User not found");
				return View(form);
			}

			var result = await _signInManager.PasswordSignInAsync(user.UserName, form.Password, true, false);

			if (!result.Succeeded)
			{
				ModelState.AddModelError(nameof(form.Login), "Invalid password");
				return View(form);
			}

			//Role

			// find
			var adminRole = await _roleManager.FindByNameAsync("Admin");
			if (adminRole == null)
			{
				//create
				await _roleManager.CreateAsync(new IdentityRole<int>() { Name = "Admin" });
			}

			//delete
			//await _roleManager.DeleteAsync(adminRole);

			// User roles



			if (!await _userManager.IsInRoleAsync(user, "Admin"))
			{
				await _userManager.AddToRoleAsync(user, "Admin");
			}

			var userRoles = await _userManager.GetRolesAsync(user);

			var claims = new List<Claim>()
		{
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.Email, user.Email),
		};

			foreach (var role in userRoles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
			};

			var claimsIdentity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);

			claims.ForEach(c => claimsIdentity.AddClaim(c));


			await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claimsIdentity));

			return Redirect("/");
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View(new RegisterForm());
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromForm] RegisterForm form)
		{
			if (!ModelState.IsValid)
			{
				return View(form);
			}

			var user = await _userManager.FindByEmailAsync(form.Login);

			if (user != null)
			{
				ModelState.AddModelError(nameof(form.Login), "User already exists");
				return View(form);
			}

			user = new User
			{
				Email = form.Login,
				PhoneNumber = form.Phone,
				UserName = form.Username
			};

			var result = await _userManager.CreateAsync(user, form.Password);

			if (!result.Succeeded)
			{
				ModelState.AddModelError(nameof(form.Login),
					string.Join(";", result.Errors.ToList().Select(e => e.Description))
					);
				return View(form);
			}

			await _signInManager.PasswordSignInAsync(form.Login, form.Password, true, false);
			return Redirect("/");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> LogoutAsync()
		{
			//await HttpContext.SignOutAsync();
			await _signInManager.SignOutAsync();
			return Redirect("/");
		}
	}
}
