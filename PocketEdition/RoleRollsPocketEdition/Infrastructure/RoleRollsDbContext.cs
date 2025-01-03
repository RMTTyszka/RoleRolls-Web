﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition._Domain.Itens.Configurations;
using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition._Domain.Powers.Entities;
using RoleRollsPocketEdition._Domain.Rolls.Entities;
using RoleRollsPocketEdition._Domain.Scenes.Entities;
using RoleRollsPocketEdition.Authentication.Users;
using RoleRollsPocketEdition.Core;
using Attribute = RoleRollsPocketEdition._Domain.Creatures.Entities.Attribute;

namespace RoleRollsPocketEdition.Infrastructure
{
    public class RoleRollsDbContext : DbContext
    { 
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<MinorSkill> MinorSkills { get; set; }
        public DbSet<Life> Lifes { get; set; }
        public DbSet<CreaturePower> CreaturePowers { get; set; }
        public DbSet<CampaignTemplate> CampaignTemplates { get; set; }
        public DbSet<AttributeTemplate> AttributeTemplates { get; set; }
        public DbSet<SkillTemplate> SkillTemplates { get; set; }
        public DbSet<MinorSkillTemplate> MinorSkillTemplates { get; set; }
        public DbSet<LifeTemplate> LifeTemplates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Roll> Rolls { get; set; }
        public DbSet<Scene> CampaignScenes { get; set; }
        public DbSet<SceneCreature> SceneCreatures { get; set; }
        public DbSet<CampaignPlayer> CampaignPlayers { get; set; }
        public DbSet<PowerTemplate> PowerTemplates { get; set; }
        public DbSet<Defense> Defenses { get; set; }
        public DbSet<DefenseTemplate> DefenseTemplates { get; set; }
        public DbSet<SceneAction> SceneActions { get; set; }
        public DbSet<ItemTemplate> ItemTemplates { get; set; }
        public DbSet<WeaponTemplate> WeaponTemplates { get; set; }
        public DbSet<ConsumableTemplate> ConsumableTemplates { get; set; }
        public DbSet<ArmorTemplate> ArmorTemplates { get; set; }
        public DbSet<ItemInstance?> ItemInstances { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<ItemConfiguration> ItemConfigurations { get; set; }

        private readonly IConfiguration _configuration;

        public RoleRollsDbContext(DbContextOptions<RoleRollsDbContext> options, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            options.UseNpgsql(_configuration.GetConnectionString("RoleRolls") ?? string.Empty);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemTemplate>()
                .HasDiscriminator<string>("ItemType")
                .HasValue<ArmorTemplate>("Armor")
                .HasValue<WeaponTemplate>("Weapon")
                .HasValue<ConsumableTemplate>("Consumable")
                .HasValue<ItemTemplate>("Item");
            
            modelBuilder.Entity<ItemInstance>()
                .HasDiscriminator<string>("ItemType")
                .HasValue<ItemInstance>("Item");

            modelBuilder.Entity<Inventory>()
                .HasMany<ItemInstance>(e => e.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Head)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.HeadId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Neck)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.NeckId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Chest)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.ChestId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Arms)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.ArmsId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Hands)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.HandsId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.MainHand)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.MainHandId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.OffHand)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.OffHandId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Waist)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.WaistId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Feet)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.FeetId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.RightRing)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.RightRingId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.LeftRing)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.LeftRingId)
                .OnDelete(DeleteBehavior.Cascade);
            
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(entityType.ClrType) && entityType.BaseType is null)
                {
                    modelBuilder.Entity(entityType.ClrType).HasKey("Id");
                }
                else
                {
                    Console.WriteLine($"Entity type {entityType.ClrType} is not supported");
                }
            }

            var scenes = modelBuilder.Entity<Scene>();
            
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
