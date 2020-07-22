﻿using CentralDeErros.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeErros.Core.Models.Maps
{
    public class OccurrenceMap : IEntityTypeConfiguration<Occurrence>
    {
        public void Configure(EntityTypeBuilder<Occurrence> builder)
        {
            builder
                .ToTable("occurrence");

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
                .HasColumnType("varchar")
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(k => k.Origin)
                .HasColumnName("origin")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(k => k.OccurrenceDate)
                .HasColumnName("occurrence_date")
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .HasOne(k => k.Microsservice)
                .WithMany(s => s.Occurrences)
                .HasForeignKey(x => x.MicrosserviceId)
                .IsRequired();

            builder
                .HasOne(x => x.Environment)
                .WithMany(x => x.Occurrences)
                .HasForeignKey(x => x.EnviromentId)
                .IsRequired();

            builder
                .HasOne(x => x.Error)
                .WithMany(x => x.Occurrences)
                .HasForeignKey(x => x.ErrorId)
                .IsRequired();


        }
    }
}
