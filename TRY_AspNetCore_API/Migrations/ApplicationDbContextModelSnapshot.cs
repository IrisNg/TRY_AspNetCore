﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TRY_AspNetCore_API.Data;

#nullable disable

namespace TRY_AspNetCore_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("CoursesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Accuracy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PP")
                        .HasColumnType("int");

                    b.Property<int?>("Power")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.PokemonMove", b =>
                {
                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<int>("MoveId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PokemonId", "MoveId");

                    b.HasIndex("MoveId");

                    b.ToTable("PokemonMove");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.PokemonType", b =>
                {
                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Slot")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PokemonId", "TypeId");

                    b.HasIndex("TypeId");

                    b.ToTable("PokemonType");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("TRY_AspNetCore_API.Models.Domain.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TRY_AspNetCore_API.Models.Domain.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.PokemonMove", b =>
                {
                    b.HasOne("TRY_AspNetCore_API.Models.Domain.Move", "Move")
                        .WithMany("Pokemons")
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TRY_AspNetCore_API.Models.Domain.Pokemon", "Pokemon")
                        .WithMany("Moves")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Move");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.PokemonType", b =>
                {
                    b.HasOne("TRY_AspNetCore_API.Models.Domain.Pokemon", "Pokemon")
                        .WithMany("Types")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TRY_AspNetCore_API.Models.Domain.Type", "Type")
                        .WithMany("Pokemons")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pokemon");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.Move", b =>
                {
                    b.Navigation("Pokemons");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.Pokemon", b =>
                {
                    b.Navigation("Moves");

                    b.Navigation("Types");
                });

            modelBuilder.Entity("TRY_AspNetCore_API.Models.Domain.Type", b =>
                {
                    b.Navigation("Pokemons");
                });
#pragma warning restore 612, 618
        }
    }
}
