using CentralDeErros.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralDeErros.Model.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.UserName)
                .HasColumnName("username")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(45)")
                .HasMaxLength(45)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder
                .HasOne(x => x.Profile)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ProfileId)
                .IsRequired();

        }
    }
}
