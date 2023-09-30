namespace HW_17_09_23.Models
{
	public class AboutMeViewModel
	{
		public AboutMeViewModel()
		{
			AboutMe = new AboutMe();
			Skills = new List<Skill>();
		}

		public AboutMe AboutMe { get; set; }
		public List<Skill> Skills { get; set; }
	}
}
