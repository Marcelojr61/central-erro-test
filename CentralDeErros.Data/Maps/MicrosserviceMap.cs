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
                .HasKey(k => k.ClientId);

            builder
                .Property(k => k.ClientId)
                .HasColumnName("client_id")
                .HasColumnType("nvarchar(450)")
                .HasMaxLength(450)
                .IsRequired();

            builder
                .Property(k => k.ClientSecret)
                .HasColumnName("client_secret")
                .HasColumnType("varchar(32)")
                .HasMaxLength(32)
                .IsRequired();

            builder
                .Property(k => k.Name)
                .HasColumnName("name")
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
