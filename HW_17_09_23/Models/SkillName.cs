﻿namespace HW_17_09_23.Models
{
    public class SkillName
    {
        public SkillName()
        {
            Skills = new List<Skill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
    }
}