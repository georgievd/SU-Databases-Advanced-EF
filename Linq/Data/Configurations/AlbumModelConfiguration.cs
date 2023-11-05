using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicHub.Data.Models;

namespace MusicHub.Data.Configurations
{
    internal class AlbumModelConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(40).
                IsRequired(true);

            builder
                .Property(x => x.ReleaseDate)
                .IsRequired(true);

            builder
                .HasOne(x => x.Producer)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.ProducerId);

            builder
                .HasMany(x => x.Songs)
                .WithOne(x => x.Album);
        }
    }
}
