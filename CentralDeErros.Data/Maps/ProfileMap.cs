using CentralDeErros.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralDeErros.Model.Maps
{
    public class ProfileMap : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder
                .ToTable("profile");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Type)
                .HasColumnName("type")
                .HasColumnType("varchar(45)")
                .HasMaxLength(45)
                .IsRequired();
        }
    }
}
