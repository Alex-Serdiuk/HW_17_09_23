using System.ComponentModel.DataAnnotations;

namespace HW_17_09_23.Models.Forms
{
	public class LoginForm
	{
		[EmailAddress]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
