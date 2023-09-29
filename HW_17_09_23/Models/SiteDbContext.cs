using Microsoft.EntityFrameworkCore;

namespace HW_17_09_23.Models
{
    public class SiteDbContext : DbContext
    {
        public SiteDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<AboutMe> AboutMes { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
    }
}
