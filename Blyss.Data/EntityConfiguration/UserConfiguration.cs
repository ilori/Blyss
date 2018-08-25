namespace Blyss.Data.EntityConfiguration
{

    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasIndex(x => x.UserName)
                .IsUnique();

            builder.Property(x => x.FirstName)
                .HasMaxLength(15);

            builder.Property(x => x.LastName)
                .HasMaxLength(15);

            builder.Property(x => x.Description)
                .HasMaxLength(45);

            builder.HasMany(x => x.Courses)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}