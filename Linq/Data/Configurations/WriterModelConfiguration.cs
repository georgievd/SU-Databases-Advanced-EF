using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicHub.Data.Models;

namespace MusicHub.Data.Configurations
{
    public class WriterModelConfiguration :IEntityTypeConfiguration<Writer>
    {
        public void Configure(EntityTypeBuilder<Writer> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired(true);

            builder
                .Property(x => x.Pseudonym)
                .IsRequired(false);

            builder
                .HasMany(x => x.Songs)
                .WithOne(x => x.Writer)
                .HasForeignKey(x => x.WriterId);
        }
    }
}
