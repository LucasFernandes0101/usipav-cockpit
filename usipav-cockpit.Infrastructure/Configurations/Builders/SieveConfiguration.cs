using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using usipav_cockpit.Domain.Entities;

namespace usipav_cockpit.Infrastructure.Configurations.Builders
{
    public class SieveConfiguration : IEntityTypeConfiguration<Sieve>
    {
        public void Configure(EntityTypeBuilder<Sieve> builder)
        {
            builder.ToTable("Sieve");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)");
            builder.Property(x => x.Description)
                .HasColumnType("varchar(200)");
            builder.Property(x => x.MaxTemperature)
                .HasColumnType("DECIMAL(5, 2)");
            builder.Property(x => x.MinTemperature)
                .HasColumnType("DECIMAL(5, 2)");

            builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd();
            builder.Property(x => x.UpdatedAt).ValueGeneratedOnUpdate();
        }
    }
}
