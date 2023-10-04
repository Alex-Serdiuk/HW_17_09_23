namespace HW_17_09_23.Models
{
    public class SkillName
    {
        public SkillName()
        {
            Skills = new HashSet<Skill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
		public virtual ImageFile? Image { get; set; }
	}
}
