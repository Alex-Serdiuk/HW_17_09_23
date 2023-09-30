using System.ComponentModel.DataAnnotations;

namespace HW_17_09_23.Models
{
	public class SkillViewModel
	{
		[Display(Name = "AboutMe Name")]
		public int AboutMeId { get; set; }

		[Display(Name = "Skill Name")]
		public int? SelectedSkillNameId { get; set; }

		[Display(Name = "New Skill Name")]
		public string NewSkillName { get; set; }

		[Display(Name = "Percentage")]
		[Required(ErrorMessage = "Percentage is required.")]
		[Range(1, 100, ErrorMessage = "Percentage must be between 1 and 100.")]
		public int Percentage { get; set; }
	}
}
