using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicHub.Data.Models;

namespace MusicHub.Data.Configurations
{
    public class SongModelConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired(true);

            builder
                .Property(x => x.Duration)
                .IsRequired(true);

            builder
                .Property(x => x.CreatedOn)
                .IsRequired(true);

            builder
                .Property(x => x.Genre)
                .IsRequired(true);

            builder
                .HasOne(x => x.Album)
                .WithMany(x => x.Songs)
                .HasForeignKey(x => x.AlbumId);

            builder
                .HasOne(x => x.Writer)
                .WithMany(x => x.Songs)
                .HasForeignKey(x => x.WriterId);

            builder
                .HasMany(x => x.SongPerformers)
                .WithOne(x => x.Song)
                .HasForeignKey(x => x.SongId);
            
            builder
                .Property(x => x.Price)
                .IsRequired(true);
        }
    }
}
