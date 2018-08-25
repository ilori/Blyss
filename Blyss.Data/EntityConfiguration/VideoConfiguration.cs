namespace Blyss.Data.EntityConfiguration
{

    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {

        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.YouTubeId)
                .IsUnique();

            builder.Property(x => x.YouTubeId)
                .IsRequired();

            builder.HasIndex(x => x.PlainYoutubeId)
                .IsUnique();

            builder.Property(x => x.PlainYoutubeId)
                .IsRequired()
                .HasMaxLength(11);
        }

    }

}