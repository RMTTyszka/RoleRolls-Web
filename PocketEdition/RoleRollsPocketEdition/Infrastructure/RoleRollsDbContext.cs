using MassTransit;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Archetypes;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Core.Authentication.Users;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.CreatureTypes.Entities;
using RoleRollsPocketEdition.Damages.Entities;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Powers.Entities;
using RoleRollsPocketEdition.Roles;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Scenes.Entities;
using RoleRollsPocketEdition.Templates.Entities;
using Attribute = RoleRollsPocketEdition.Creatures.Entities.Attribute;

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
        public DbSet<Role> Roles { get; set; }
        public DbSet<DamageType> DamageTypes { get; set; }
        public DbSet<Archetype> Archetypes { get; set; }
        public DbSet<CreatureType> CreatureTypes { get; set; }
        public DbSet<Bonus> Bonus { get; set; }

        private readonly IConfiguration _configuration;

        public RoleRollsDbContext(DbContextOptions<RoleRollsDbContext> options, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("RoleRolls") ?? string.Empty);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CampaignTemplate>(template =>
            {
                template.Navigation(c => c.Lifes).AutoInclude();
                template.Navigation(c => c.Attributes).AutoInclude();
                template.Navigation(c => c.ItemConfiguration).AutoInclude();
                template.Navigation(c => c.Defenses).AutoInclude();
                template.Navigation(c => c.DamageTypes).AutoInclude();
                template.Navigation(c => c.AttributelessSkills).AutoInclude();
                template.Navigation(c => c.CreatureTypes).AutoInclude();
            });
            modelBuilder.Entity<AttributeTemplate>(attribute =>
            {
                attribute.Navigation(c => c.SkillTemplates).AutoInclude();
            });      
            modelBuilder.Entity<CreatureType>(attribute =>
            {
                attribute.Navigation(c => c.Bonuses).AutoInclude();
            });        
            modelBuilder.Entity<SkillTemplate>(skill =>
            {
                skill.Navigation(c => c.MinorSkills).AutoInclude();
            });      
            modelBuilder.Entity<CreatureType>(creatureType =>
            {
                creatureType.HasMany<Bonus>(e => e.Bonuses)
                    .WithOne(b => b.CreatureType)
                    .HasForeignKey(b => b.EntityId)
                    .IsRequired(false);
            });         
            modelBuilder.Entity<Archetype>(creatureType =>
            {
                creatureType.HasMany<Bonus>(e => e.Bonuses)
                    .WithOne(b => b.Archetype)
                    .HasForeignKey(b => b.EntityId)
                    .IsRequired(false);
            });
            
            modelBuilder.Owned<Property>();
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
                var stringProperties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                foreach (var property in stringProperties)
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(property.Name)
                        .HasMaxLength(450);
                }
            }

            var scenes = modelBuilder.Entity<Scene>();
            
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
            modelBuilder.AddTransactionalOutboxEntities();
        }
    }
}
