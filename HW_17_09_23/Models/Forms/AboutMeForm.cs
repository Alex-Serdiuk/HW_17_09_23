using System.ComponentModel.DataAnnotations;

namespace HW_17_09_23.Models.Forms
{
	public class AboutMeForm
	{
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Phone number")]
        public string? Phone { get; set; }
        [Display(Name = "About me")]
        public string? Info { get; set; }

        [Required(ErrorMessage = "Зображення обов'язкове")]
		public IFormFile Image { get; set; }
	}
}
