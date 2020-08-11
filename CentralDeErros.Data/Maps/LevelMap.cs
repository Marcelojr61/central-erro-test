using CentralDeErros.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CentralDeErros.Model.Maps
{
    public class LevelMap : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder
                .ToTable("level");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(32)")
                .HasMaxLength(32)
                .IsRequired();

            builder
                .HasMany(x => x.Errors)
                .WithOne(x => x.Level);
        }
    }
}
