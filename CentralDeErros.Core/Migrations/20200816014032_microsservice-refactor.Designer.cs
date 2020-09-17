﻿// <auto-generated />
using System;
using CentralDeErros.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CentralDeErros.API.Migrations
{
    [DbContext(typeof(CentralDeErrosDbContext))]
    [Migration("20200816014032_microsservice-refactor")]
    partial class microsservicerefactor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CentralDeErros.Model.Models.Environment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Phase")
                        .IsRequired()
                        .HasColumnName("phase")
                        .HasColumnType("varchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("environment");
                });

            modelBuilder.Entity("CentralDeErros.Model.Models.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnName("details")
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250);

                    b.Property<int>("EnviromentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ErrorDate")
                        .HasColumnName("error_date")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsArchived")
                        .HasColumnName("is_archived")
                        .HasColumnType("bit");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("MicrosserviceClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnName("origin")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.HasKey("Id");

                    b.HasIndex("EnviromentId");

                    b.HasIndex("LevelId");

                    b.HasIndex("MicrosserviceClientId");

                    b.ToTable("error");
                });

            modelBuilder.Entity("CentralDeErros.Model.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("level");
                });

            modelBuilder.Entity("CentralDeErros.Model.Models.Microsservice", b =>
                {
                    b.Property<string>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("client_id")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasColumnName("client_secret")
                        .HasColumnType("varchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ClientId");

                    b.ToTable("microservice");
                });

            modelBuilder.Entity("CentralDeErros.Model.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("type")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.HasKey("Id");

                    b.ToTable("profile");
                });

            modelBuilder.Entity("CentralDeErros.Model.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("CentralDeErros.Model.Models.Error", b =>
                {
                    b.HasOne("CentralDeErros.Model.Models.Environment", "Environment")
                        .WithMany("Errors")
                        .HasForeignKey("EnviromentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CentralDeErros.Model.Models.Level", "Level")
                        .WithMany("Errors")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CentralDeErros.Model.Models.Microsservice", "Microsservice")
                        .WithMany("Errors")
                        .HasForeignKey("MicrosserviceClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CentralDeErros.Model.Models.User", b =>
                {
                    b.HasOne("CentralDeErros.Model.Models.Profile", "Profile")
                        .WithMany("Users")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
