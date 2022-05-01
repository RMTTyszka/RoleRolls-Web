﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RoleRollsPocketEdition.Infrastructure;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    [DbContext(typeof(RoleRollsDbContext))]
    [Migration("20220501172118_RemovedValueFromTemplates")]
    partial class RemovedValueFromTemplates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.Attribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeTemplateId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatureId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatureId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.AttributeTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatureTemplateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatureTemplateId");

                    b.ToTable("AttributeTemplates");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.Creature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Creatures");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.CreatureTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalAttributePoints")
                        .HasColumnType("integer");

                    b.Property<int>("TotalSkillsPoints")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("CreatureTemplates");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.Life", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatureId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LifeTemplateId")
                        .HasColumnType("uuid");

                    b.Property<int>("MaxValue")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatureId");

                    b.ToTable("Lifes");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.LifeTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatureTemplateId")
                        .HasColumnType("uuid");

                    b.Property<int>("MaxValue")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatureTemplateId");

                    b.ToTable("LifeTemplates");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.MinorSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("MinorSkillTemplateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("SkillId")
                        .HasColumnType("uuid");

                    b.Property<int>("SkillProficience")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SkillId");

                    b.ToTable("MinorSkills");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.MinorSkillTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("SkillTemplateId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SkillTemplateId");

                    b.ToTable("MinorSkillTemplates");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatureId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SkillTemplateId")
                        .HasColumnType("uuid");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatureId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.SkillTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatureTemplateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatureTemplateId");

                    b.ToTable("SkillTemplates");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.Attribute", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Domain.Creature", null)
                        .WithMany("Attributes")
                        .HasForeignKey("CreatureId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.AttributeTemplate", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Domain.CreatureTemplate", null)
                        .WithMany("Attributes")
                        .HasForeignKey("CreatureTemplateId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.Life", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Domain.Creature", null)
                        .WithMany("Lifes")
                        .HasForeignKey("CreatureId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.LifeTemplate", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Domain.CreatureTemplate", null)
                        .WithMany("Lifes")
                        .HasForeignKey("CreatureTemplateId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.MinorSkill", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Domain.Skill", null)
                        .WithMany("MinorSkills")
                        .HasForeignKey("SkillId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.MinorSkillTemplate", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Domain.SkillTemplate", null)
                        .WithMany("MinorSkills")
                        .HasForeignKey("SkillTemplateId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.Skill", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Domain.Creature", null)
                        .WithMany("Skills")
                        .HasForeignKey("CreatureId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.SkillTemplate", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Domain.CreatureTemplate", null)
                        .WithMany("Skills")
                        .HasForeignKey("CreatureTemplateId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.Creature", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Lifes");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.CreatureTemplate", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Lifes");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.Skill", b =>
                {
                    b.Navigation("MinorSkills");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Domain.SkillTemplate", b =>
                {
                    b.Navigation("MinorSkills");
                });
#pragma warning restore 612, 618
        }
    }
}
