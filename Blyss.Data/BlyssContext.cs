namespace Blyss.Data
{

    using Entities;
    using EntityConfiguration;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class BlyssContext : IdentityDbContext<User>
    {

        public BlyssContext()
        {
        }

        public BlyssContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Video> Videos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new CategoryConfigration());
            builder.ApplyConfiguration(new LanguageConfiguration());
            builder.ApplyConfiguration(new VideoConfiguration());

            base.OnModelCreating(builder);
        }

    }

}