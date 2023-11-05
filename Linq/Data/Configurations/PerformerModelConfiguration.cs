using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicHub.Data.Models;

namespace MusicHub.Data.Configurations
{
    internal class PerformerModelConfiguration :IEntityTypeConfiguration<Performer>
    {
        public void Configure(EntityTypeBuilder<Performer> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(20)
                .IsRequired(true);

            builder
                .Property(x => x.LastName)
                .HasMaxLength(20)
                .IsRequired(true);

            builder
                .Property(x => x.Age)
                .IsRequired(true);

            builder
                .Property(x => x.NetWorth)
                .IsRequired(true);

            builder
                .HasMany(x => x.PerformerSongs)
                .WithOne(x => x.Performer)
                .HasForeignKey(x => x.PerformerId);
        }
    }
}
