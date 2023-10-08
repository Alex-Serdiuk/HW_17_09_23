using System.ComponentModel.DataAnnotations;

namespace HW_17_09_23.Models.Forms
{
	public class SkillNameForm
	{
		[Display(Name = "Name")]
		public string Name { get; set; }

		//[Required(ErrorMessage = "Зображення обов'язкове")]
		public IFormFile? Image { get; set; }
	}
}
