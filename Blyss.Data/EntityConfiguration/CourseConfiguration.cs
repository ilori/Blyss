namespace Blyss.Data.EntityConfiguration
{

    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {

        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.Name)
                .HasMaxLength(13)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(45)
                .IsRequired();


            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Language)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Video)
                .WithOne(x => x.Course)
                .HasForeignKey<Video>(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}