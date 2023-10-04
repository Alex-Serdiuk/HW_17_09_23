using System.ComponentModel.DataAnnotations;

namespace HW_17_09_23.Models.Forms
{
	public class AboutMeForm
	{
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Зображення обов'язкове")]
		public IFormFile Image { get; set; }
	}
}
