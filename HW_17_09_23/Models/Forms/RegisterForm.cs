using System.ComponentModel.DataAnnotations;

namespace HW_17_09_23.Models.Forms
{
	public class RegisterForm
	{
		[Required]
		public string Username { get; set; }

		public string? Phone { get; set; }
		[EmailAddress]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
