﻿// <auto-generated />
using HW_17_09_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HW_17_09_23.Migrations
{
    [DbContext(typeof(SiteDbContext))]
    [Migration("20230929192130_Unit3")]
    partial class Unit3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("HW_17_09_23.Models.AboutMe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AboutMes");
                });

            modelBuilder.Entity("HW_17_09_23.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AboutMeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Percentage")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SkillNameId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AboutMeId");

                    b.HasIndex("SkillNameId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("HW_17_09_23.Models.SkillName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SkillNames");
                });

            modelBuilder.Entity("HW_17_09_23.Models.Skill", b =>
                {
                    b.HasOne("HW_17_09_23.Models.AboutMe", "AboutMe")
                        .WithMany("Skills")
                        .HasForeignKey("AboutMeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HW_17_09_23.Models.SkillName", "SkillName")
                        .WithMany("Skills")
                        .HasForeignKey("SkillNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AboutMe");

                    b.Navigation("SkillName");
                });

            modelBuilder.Entity("HW_17_09_23.Models.AboutMe", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("HW_17_09_23.Models.SkillName", b =>
                {
                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
