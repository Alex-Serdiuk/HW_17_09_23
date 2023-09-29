namespace HW_17_09_23.Models;

public class Skill
{
    public int Id { get; set; }
    public int Percentage { get; set; }
    public virtual AboutMe AboutMe { get; set; }
    public virtual SkillName SkillName { get; set; } // Навичка
}
