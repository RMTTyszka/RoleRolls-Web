﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RoleRollsPocketEdition.Infrastructure;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    [DbContext(typeof(RoleRollsDbContext))]
    partial class RoleRollsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MassTransit.EntityFrameworkCoreIntegration.InboxState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("Consumed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ConsumerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Delivered")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("LastSequenceNumber")
                        .HasColumnType("bigint");

                    b.Property<Guid>("LockId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<int>("ReceiveCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Received")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasAlternateKey("MessageId", "ConsumerId");

                    b.HasIndex("Delivered");

                    b.ToTable("InboxState");
                });

            modelBuilder.Entity("MassTransit.EntityFrameworkCoreIntegration.OutboxMessage", b =>
                {
                    b.Property<long>("SequenceNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("SequenceNumber"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid?>("ConversationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CorrelationId")
                        .HasColumnType("uuid");

                    b.Property<string>("DestinationAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime?>("EnqueueTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FaultAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Headers")
                        .HasColumnType("text");

                    b.Property<Guid?>("InboxConsumerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InboxMessageId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InitiatorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OutboxId")
                        .HasColumnType("uuid");

                    b.Property<string>("Properties")
                        .HasColumnType("text");

                    b.Property<Guid?>("RequestId")
                        .HasColumnType("uuid");

                    b.Property<string>("ResponseAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SourceAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("SequenceNumber");

                    b.HasIndex("EnqueueTime");

                    b.HasIndex("ExpirationTime");

                    b.HasIndex("OutboxId", "SequenceNumber")
                        .IsUnique();

                    b.HasIndex("InboxMessageId", "InboxConsumerId", "SequenceNumber")
                        .IsUnique();

                    b.ToTable("OutboxMessage");
                });

            modelBuilder.Entity("MassTransit.EntityFrameworkCoreIntegration.OutboxState", b =>
                {
                    b.Property<Guid>("OutboxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("Delivered")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("LastSequenceNumber")
                        .HasColumnType("bigint");

                    b.Property<Guid>("LockId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("OutboxId");

                    b.HasIndex("Created");

                    b.ToTable("OutboxState");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Authentication.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Attribute", b =>
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

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Creature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CampaignId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatureTemplateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Creatures");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.CreaturePower", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ConsumedUsages")
                        .HasColumnType("integer");

                    b.Property<Guid>("CreatureId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PowerTemplateId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatureId");

                    b.HasIndex("PowerTemplateId");

                    b.ToTable("CreaturePowers");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Defense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatureId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DefenseTemplateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Formula")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatureId");

                    b.ToTable("Defenses");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Life", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatureId")
                        .HasColumnType("uuid");

                    b.Property<string>("Formula")
                        .IsRequired()
                        .HasColumnType("text");

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

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.MinorSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("MinorSkillTemplateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SkillId");

                    b.ToTable("MinorSkills");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Skill", b =>
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

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Domain.Entities.DefenseTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatureTemplateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Formula")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatureTemplateId");

                    b.ToTable("DefenseTemplates");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.AttributeTemplate", b =>
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

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.CreatureTemplate", b =>
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

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.LifeTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatureTemplateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Formula")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatureTemplateId");

                    b.ToTable("LifeTemplates");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.MinorSkillTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SkillTemplateId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SkillTemplateId");

                    b.ToTable("MinorSkillTemplates");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.SkillTemplate", b =>
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

            modelBuilder.Entity("RoleRollsPocketEdition.Domain.Campaigns.Entities.Campaign", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatureTemplateId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InvitationSecret")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MasterId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Domain.Campaigns.Entities.CampaignPlayer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CampaignId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InvidationCode")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("CampaignPlayers");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Domain.Campaigns.Entities.Scene", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CampaignId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CampaignScenes");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Domain.Campaigns.Entities.SceneAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActorId")
                        .HasColumnType("uuid");

                    b.Property<int>("ActorType")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SceneId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("SceneActions");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Powers.Entities.PowerTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ActionType")
                        .HasColumnType("integer");

                    b.Property<Guid>("CampaignId")
                        .HasColumnType("uuid");

                    b.Property<string>("CastDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CastFormula")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PowerDurationType")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TargetDefenseId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int?>("UsageType")
                        .HasColumnType("integer");

                    b.Property<string>("UsagesFormula")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UseAttributeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.ToTable("PowerTemplates");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Rolls.Entities.Roll", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActorId")
                        .HasColumnType("uuid");

                    b.Property<int>("ActorType")
                        .HasColumnType("integer");

                    b.Property<Guid>("CampaignId")
                        .HasColumnType("uuid");

                    b.Property<int>("Complexity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<bool>("Hidden")
                        .HasColumnType("boolean");

                    b.Property<int>("NumberOfCriticalFailures")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfCriticalSuccesses")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfDices")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfSuccesses")
                        .HasColumnType("integer");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uuid");

                    b.Property<int>("PropertyType")
                        .HasColumnType("integer");

                    b.Property<int>("RollBonus")
                        .HasColumnType("integer");

                    b.Property<string>("RolledDices")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SceneId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Success")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Rolls");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Scenes.Domain.Entities.SceneCreature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatureId")
                        .HasColumnType("uuid");

                    b.Property<int>("CreatureType")
                        .HasColumnType("integer");

                    b.Property<bool>("Hidden")
                        .HasColumnType("boolean");

                    b.Property<Guid>("SceneId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("SceneCreatures");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Attribute", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Entities.Creature", null)
                        .WithMany("Attributes")
                        .HasForeignKey("CreatureId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.CreaturePower", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Entities.Creature", "Creature")
                        .WithMany("Powers")
                        .HasForeignKey("CreatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoleRollsPocketEdition.Powers.Entities.PowerTemplate", "PowerTemplate")
                        .WithMany()
                        .HasForeignKey("PowerTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creature");

                    b.Navigation("PowerTemplate");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Defense", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Entities.Creature", null)
                        .WithMany("Defenses")
                        .HasForeignKey("CreatureId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Life", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Entities.Creature", null)
                        .WithMany("Lifes")
                        .HasForeignKey("CreatureId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.MinorSkill", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Entities.Skill", null)
                        .WithMany("MinorSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Skill", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Creatures.Entities.Creature", null)
                        .WithMany("Skills")
                        .HasForeignKey("CreatureId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Domain.Entities.DefenseTemplate", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.CreaturesTemplates.Entities.CreatureTemplate", null)
                        .WithMany("Defenses")
                        .HasForeignKey("CreatureTemplateId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.AttributeTemplate", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.CreaturesTemplates.Entities.CreatureTemplate", null)
                        .WithMany("Attributes")
                        .HasForeignKey("CreatureTemplateId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.LifeTemplate", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.CreaturesTemplates.Entities.CreatureTemplate", null)
                        .WithMany("Lifes")
                        .HasForeignKey("CreatureTemplateId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.MinorSkillTemplate", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.CreaturesTemplates.Entities.SkillTemplate", null)
                        .WithMany("MinorSkills")
                        .HasForeignKey("SkillTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.SkillTemplate", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.CreaturesTemplates.Entities.CreatureTemplate", null)
                        .WithMany("Skills")
                        .HasForeignKey("CreatureTemplateId");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Powers.Entities.PowerTemplate", b =>
                {
                    b.HasOne("RoleRollsPocketEdition.Domain.Campaigns.Entities.Campaign", "Campaign")
                        .WithMany("PowerTemplates")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campaign");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Creature", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Defenses");

                    b.Navigation("Lifes");

                    b.Navigation("Powers");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Creatures.Entities.Skill", b =>
                {
                    b.Navigation("MinorSkills");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.CreatureTemplate", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Defenses");

                    b.Navigation("Lifes");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.CreaturesTemplates.Entities.SkillTemplate", b =>
                {
                    b.Navigation("MinorSkills");
                });

            modelBuilder.Entity("RoleRollsPocketEdition.Domain.Campaigns.Entities.Campaign", b =>
                {
                    b.Navigation("PowerTemplates");
                });
#pragma warning restore 612, 618
        }
    }
}
