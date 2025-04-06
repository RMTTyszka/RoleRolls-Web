using System.Text;
using System.Text.Unicode;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoleRollsPocketEdition.Archetypes;
using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Authentication.Users;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.CreatureTypes.Entities;
using RoleRollsPocketEdition.Damages.Entities;
using RoleRollsPocketEdition.Encounters.Entities;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Powers.Entities;
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
        public DbSet<SpecificSkill> MinorSkills { get; set; }
        public DbSet<Vitality> Vitalities { get; set; }
        public DbSet<CreaturePower> CreaturePowers { get; set; }
        public DbSet<CampaignTemplate> CampaignTemplates { get; set; }
        public DbSet<AttributeTemplate> AttributeTemplates { get; set; }
        public DbSet<SkillTemplate> SkillTemplates { get; set; }
        public DbSet<SpecificSkillTemplate> MinorSkillTemplates { get; set; }
        public DbSet<VitalityTemplate> VitalityTemplates { get; set; }
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
        public DbSet<DamageType> DamageTypes { get; set; }
        public DbSet<Archetype> Archetypes { get; set; }
        public DbSet<CreatureType> CreatureTypes { get; set; }
        public DbSet<Bonus> Bonus { get; set; }
        public DbSet<Encounter> Encounters { get; set; }

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

            base.OnModelCreating(modelBuilder);
            ModelCreature(modelBuilder);
            ModelArchetype(modelBuilder);
            modelBuilder.Owned<Property>();

            modelBuilder.Entity<CampaignTemplate>(template =>
            {
            });
            
            modelBuilder.Entity<Encounter>(e =>
            {
                e.HasMany<Creature>(p => p.Creatures)
                    .WithOne(c => c.Encounter)
                    .OnDelete(DeleteBehavior.Cascade);
            });         
            modelBuilder.Entity<AttributeTemplate>(e =>
            {
                e.HasMany<SkillTemplate>(p => p.SkillTemplates)
                    .WithOne(c => c.AttributeTemplate)
                    .OnDelete(DeleteBehavior.Cascade);        
                e.HasMany<SpecificSkillTemplate>(p => p.SpecificSkillTemplates)
                    .WithOne(c => c.AttributeTemplate)
                    .OnDelete(DeleteBehavior.Cascade);
                e.Navigation(c => c.SkillTemplates).AutoInclude();

            });
            modelBuilder.Entity<CreatureType>(e =>
            {
                e.Navigation(c => c.Bonuses).AutoInclude();
                e.HasMany(ct => ct.Bonuses)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasMany<Creature>(p => p.Creatures)
                    .WithOne(p => p.CreatureType)
                    .OnDelete(DeleteBehavior.SetNull);
            });        
            modelBuilder.Entity<SkillTemplate>(skill =>
            {
                skill.Navigation(c => c.SpecificSkills).AutoInclude();
            });      
       

            
            modelBuilder.Entity<ItemTemplate>(e =>
            {
                e.HasDiscriminator<string>("ItemType")
                    .HasValue<ArmorTemplate>("Armor")
                    .HasValue<WeaponTemplate>("Weapon")
                    .HasValue<ConsumableTemplate>("Consumable")
                    .HasValue<ItemTemplate>("Item");
                e.HasOne<PowerTemplate>(p => p.Power)
                    .WithMany(p => p.ItemTemplates)
                    .OnDelete(DeleteBehavior.SetNull);
            });


            modelBuilder.Entity<ItemInstance>(e =>
            {
                e.HasDiscriminator<string>("ItemType")
                    .HasValue<ItemInstance>("Item");
                    e.HasOne<Equipment>()
                    .WithOne(e => e.Neck)
                    .HasForeignKey<Equipment>(e => e.NeckId)
                    .OnDelete(DeleteBehavior.Cascade);
                    e.HasOne<PowerInstance>(p => p.PowerInstance)
                        .WithMany(p => p.ItemInstances)
                        .OnDelete(DeleteBehavior.SetNull);
            });
  

            modelBuilder.Entity<Inventory>()
                .HasMany<ItemInstance>(e => e.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Head)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.HeadId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Neck)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.NeckId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Chest)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.ChestId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Arms)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.ArmsId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Hands)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.HandsId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.MainHand)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.MainHandId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.OffHand)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.OffHandId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Waist)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.WaistId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.Feet)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.FeetId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.RightRing)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.RightRingId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Equipment>()
                .HasOne<ItemInstance>(e => e.LeftRing)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.LeftRingId)
                .OnDelete(DeleteBehavior.SetNull);
            
           

            

            
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
            modelBuilder.AddTransactionalOutboxEntities();
        }

        private void ModelArchetype(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Archetype>(e =>
            {
                e.Navigation(c => c.Bonuses).AutoInclude();
                e.HasMany(a => a.Bonuses)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasMany<Creature>(p => p.Creatures)
                    .WithOne(p => p.Archetype)
                    .OnDelete(DeleteBehavior.SetNull);
                e.Property(p => p.Details)
                    .HasConversion(
                        v => Encoding.UTF8.GetBytes(v),
                        v => Encoding.UTF8.GetString(v))
                    .Metadata.SetMaxLength(null);
            });   
        }

        private void ModelCreature(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attribute>(e =>
            {
                e.HasMany<SpecificSkill>(p => p.SpecificSkills)
                    .WithOne(p => p.Attribute)
                    .OnDelete(DeleteBehavior.Cascade);
            });         
            modelBuilder.Entity<Skill>(e =>
            {
                e.HasMany<SpecificSkill>(p => p.SpecificSkills)
                    .WithOne(p => p.Skill)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}