using MassTransit;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition._Domain.Itens;
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
        public DbSet<CreatureTemplate> CreatureTemplates { get; set; }
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
        public DbSet<ArmorTemplate> ArmorTemplates { get; set; }
        public DbSet<ItemInstance> ItemInstances { get; set; }

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
                .HasValue<ArmorInstance>("Armor")
                .HasValue<WeaponInstance>("Weapon")
                .HasValue<ItemInstance>("Item");
            
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
