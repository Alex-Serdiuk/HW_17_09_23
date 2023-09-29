namespace HW_17_09_23.Models
{
    public class AboutMe
    {
		public AboutMe()
		{
			PhotoPath = "";
			Skills = new List<Skill>();
		}

		public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoPath { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
    }
}
