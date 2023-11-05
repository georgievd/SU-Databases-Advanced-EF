using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicHub.Data.Models;

namespace MusicHub.Data.Configurations
{
    public class SongPerformerModelConfiguration :IEntityTypeConfiguration<SongPerformer>
    {
        public void Configure(EntityTypeBuilder<SongPerformer> builder)
        {
            builder
                .HasKey(x => 
                    new { x.SongId, x.PerformerId });

            builder
                .HasOne(x => x.Song)
                .WithMany(x => x.SongPerformers)
                .HasForeignKey(x => x.SongId)
                .IsRequired(true);

            builder
                .HasOne(x => x.Performer)
                .WithMany(x => x.PerformerSongs)
                .HasForeignKey(x => x.PerformerId)
                .IsRequired(true);
        }
    }
}
