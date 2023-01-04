using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Authentication.Users;
using RoleRollsPocketEdition.Campaigns.Domain;
using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Infrastructure
{
    public class RoleRollsDbContext : DbContext
    { 
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<Creatures.Domain.Attribute> Attributes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<MinorSkill> MinorSkills { get; set; }
        public DbSet<Life> Lifes { get; set; }
        public DbSet<CreatureTemplate> CreatureTemplates { get; set; }
        public DbSet<AttributeTemplate> AttributeTemplates { get; set; }
        public DbSet<SkillTemplate> SkillTemplates { get; set; }
        public DbSet<MinorSkillTemplate> MinorSkillTemplates { get; set; }
        public DbSet<LifeTemplate> LifeTemplates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Roll> Rolls { get; set; }


        public RoleRollsDbContext(DbContextOptions<RoleRollsDbContext> options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            options.UseNpgsql("Host=localhost:5432;Database=RoleRollsPocket;Username=postgres;Password=123qwe");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasKey("Id");
                }
            }

        }
    }
}
