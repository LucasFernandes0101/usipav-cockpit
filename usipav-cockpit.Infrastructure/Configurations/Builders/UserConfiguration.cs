using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using usipav_cockpit.Domain.Entities;

namespace usipav_cockpit.Infrastructure.Configurations.Builders
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)");
            builder.Property(x => x.Email)
                .HasColumnType("varchar(256)");
            builder.Property(x => x.Password)
                .HasColumnType("varchar(60)");

            builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd();
            builder.Property(x => x.UpdatedAt).ValueGeneratedOnUpdate();
        }
    }
}
