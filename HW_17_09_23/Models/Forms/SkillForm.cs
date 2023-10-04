using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HW_17_09_23.Models.Forms
{
    public class SkillForm
    {
        public SkillForm()
        {
            SkillNameList = new List<SelectListItem>();
        }

        //public int Id { get; set; }
        [Display(Name = "AboutMe Name")]
        public int AboutMeId { get; set; }

        [Display(Name = "Skill Name")]
        public int? SelectedSkillNameId { get; set; }

        //[Display(Name = "New Skill Name")]
        //public string NewSkillName { get; set; }

        [Display(Name = "Percentage")]
        [Required(ErrorMessage = "Percentage is required.")]
        [Range(1, 100, ErrorMessage = "Percentage must be between 1 and 100.")]
        public int Percentage { get; set; }

        public List<SelectListItem> SkillNameList { get; set; }
    }
}
