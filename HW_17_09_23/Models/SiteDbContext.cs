﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HW_17_09_23.Models
{
    public class SiteDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public SiteDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<AboutMe> AboutMes { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SkillName> SkillNames { get; set; } // Додана таблиця SkillNames
        public virtual DbSet<ImageFile> Images { get; set; }
        public virtual DbSet<User> Users { get; set; } = null!;

    }
}
