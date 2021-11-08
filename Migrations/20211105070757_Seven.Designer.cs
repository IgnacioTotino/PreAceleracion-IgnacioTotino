﻿// <auto-generated />
using System;
using ChallengeDisney.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChallengeDisney.Migrations
{
    [DbContext(typeof(DisneyContext))]
    [Migration("20211105070757_Seven")]
    partial class Seven
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("MoviesData")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChallengeDisney.Entidades.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MoviesId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Story")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MoviesId");

                    b.ToTable("Charaters");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Age = 21,
                            Image = "Holis",
                            Name = "Zoro",
                            Story = "Wano",
                            Weight = 70
                        },
                        new
                        {
                            Id = 4,
                            Age = 17,
                            Image = "ghost",
                            Name = "Perona",
                            Story = "Thriler Bark",
                            Weight = 45
                        });
                });

            modelBuilder.Entity("ChallengeDisney.Entidades.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            Image = "holaaaa",
                            Name = "Anime"
                        });
                });

            modelBuilder.Entity("ChallengeDisney.Entidades.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GenresId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Qualification")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GenresId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Image = "holaa",
                            Qualification = 5,
                            Title = "Back to Future"
                        },
                        new
                        {
                            Id = 4,
                            CreationDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Image = "holaaa",
                            Qualification = 5,
                            Title = "One Piece Stampede"
                        });
                });

            modelBuilder.Entity("ChallengeDisney.Entidades.Character", b =>
                {
                    b.HasOne("ChallengeDisney.Entidades.Movie", "Movies")
                        .WithMany("Characters")
                        .HasForeignKey("MoviesId");

                    b.Navigation("Movies");
                });

            modelBuilder.Entity("ChallengeDisney.Entidades.Movie", b =>
                {
                    b.HasOne("ChallengeDisney.Entidades.Genre", "Genres")
                        .WithMany("Movies")
                        .HasForeignKey("GenresId");

                    b.Navigation("Genres");
                });

            modelBuilder.Entity("ChallengeDisney.Entidades.Genre", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("ChallengeDisney.Entidades.Movie", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
