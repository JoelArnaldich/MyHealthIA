﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyHealthAI.Models;

#nullable disable

namespace MyHealthAI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241114174441_MigracionIncial")]
    partial class MigracionIncial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyHealthAI.Models.AnswerIA", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Answers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("AnswerIA");
                });

            modelBuilder.Entity("MyHealthAI.Models.DialyExercise", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("ExerciseHighPerformance")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseLowPerformance")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseMediumPerformance")
                        .HasColumnType("int");

                    b.Property<int>("LiftWeights")
                        .HasColumnType("int");

                    b.Property<int>("Run")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("Walk")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("DialyExercises");
                });

            modelBuilder.Entity("MyHealthAI.Models.DialyWater", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("WaterLiter")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("DialyWater");
                });

            modelBuilder.Entity("MyHealthAI.Models.Meal", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Carbohydrate")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("Fat")
                        .HasColumnType("int");

                    b.Property<int>("Kcal")
                        .HasColumnType("int");

                    b.Property<int>("MealTypeID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Protein")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int?>("Weight")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MealTypeID");

                    b.HasIndex("UserID");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("MyHealthAI.Models.MealType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("MealType");
                });

            modelBuilder.Entity("MyHealthAI.Models.Objective", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("GoalWeight")
                        .HasColumnType("int");

                    b.Property<bool>("LoseWeight")
                        .HasColumnType("bit");

                    b.Property<bool>("LoseWeightWinMuscle")
                        .HasColumnType("bit");

                    b.Property<bool>("WinMuscleWinWeight")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("MyHealthAI.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<int?>("ObjectiveID")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Weight")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ObjectiveID")
                        .IsUnique()
                        .HasFilter("[ObjectiveID] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyHealthAI.Models.AnswerIA", b =>
                {
                    b.HasOne("MyHealthAI.Models.User", "User")
                        .WithMany("answersIA")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyHealthAI.Models.DialyExercise", b =>
                {
                    b.HasOne("MyHealthAI.Models.User", "User")
                        .WithMany("dialy_Exercises")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyHealthAI.Models.DialyWater", b =>
                {
                    b.HasOne("MyHealthAI.Models.User", "User")
                        .WithMany("DialyWater")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyHealthAI.Models.Meal", b =>
                {
                    b.HasOne("MyHealthAI.Models.MealType", "MealType")
                        .WithMany("Meals")
                        .HasForeignKey("MealTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyHealthAI.Models.User", "User")
                        .WithMany("Meals")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyHealthAI.Models.User", b =>
                {
                    b.HasOne("MyHealthAI.Models.Objective", "Objective")
                        .WithOne("User")
                        .HasForeignKey("MyHealthAI.Models.User", "ObjectiveID");

                    b.Navigation("Objective");
                });

            modelBuilder.Entity("MyHealthAI.Models.MealType", b =>
                {
                    b.Navigation("Meals");
                });

            modelBuilder.Entity("MyHealthAI.Models.Objective", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("MyHealthAI.Models.User", b =>
                {
                    b.Navigation("DialyWater");

                    b.Navigation("Meals");

                    b.Navigation("answersIA");

                    b.Navigation("dialy_Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
