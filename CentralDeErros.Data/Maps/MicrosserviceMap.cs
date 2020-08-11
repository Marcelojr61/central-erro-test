using CentralDeErros.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralDeErros.Model.Maps
{
    public class MicrosserviceMap : IEntityTypeConfiguration<Microsservice>
    {
        public void Configure(EntityTypeBuilder<Microsservice> builder)
        {

            builder
                .ToTable("microservice");

            builder
                .HasKey(k => k.Id);

            builder
                .Property(k => k.Id)
                .HasColumnName("id")
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(k => k.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(k => k.Token)
                .HasColumnName("token")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasMany(k => k.Errors)
                .WithOne(a => a.Microsservice)
                .IsRequired();
        }
    }
}
