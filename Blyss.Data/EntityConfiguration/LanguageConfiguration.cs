namespace Blyss.Data.EntityConfiguration
{

    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {

        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasMany(x => x.Courses)
                .WithOne(x => x.Language)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}