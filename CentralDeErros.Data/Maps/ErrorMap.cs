using CentralDeErros.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralDeErros.Model.Maps
{
    public class ErrorMap : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> builder)
        {
            builder
                .ToTable("error");

            builder
                .HasKey(k => k.Id);

            builder
                .Property(k => k.Id)
                .HasColumnName("id")
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(k => k.Details)
                .HasColumnName("details")
                .HasColumnType("varchar(250)")
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(k => k.Origin)
                .HasColumnName("origin")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(45)")
                .HasMaxLength(45)
                .IsRequired();


            builder
                .Property(k => k.ErrorDate)
                .HasColumnName("error_date")
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .HasOne(k => k.Microsservice)
                .WithMany(s => s.Errors)
                .HasForeignKey(x => x.MicrosserviceId)
                .IsRequired();

            builder
                .HasOne(x => x.Environment)
                .WithMany(x => x.Errors)
                .HasForeignKey(x => x.EnviromentId)
                .IsRequired();

            builder
                .HasOne(x => x.Level)
                .WithMany(s => s.Errors)
                .HasForeignKey(b => b.LevelId)
                .IsRequired();


        }
    }
}
