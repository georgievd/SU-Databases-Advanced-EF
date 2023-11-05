using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicHub.Data.Models;

namespace MusicHub.Data.Configurations
{
    public class ProducerModelConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> builder)
        {

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired(true);

            builder
                .Property(x => x.PhoneNumber)
                .IsRequired(false);

            builder
                .Property(x => x.Pseudonym)
                .IsRequired(false);

            builder.
                HasMany(x => x.Albums)
                .WithOne(x => x.Producer)
                .HasForeignKey(x => x.ProducerId);
        }
    }
}
