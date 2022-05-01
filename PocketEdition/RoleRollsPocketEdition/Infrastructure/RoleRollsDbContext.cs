using Microsoft.EntityFrameworkCore;
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
        public DbSet<AttributeTemplate> AttributeTemplates { get; set; }
        public DbSet<SkillTemplate> SkillTemplates { get; set; }
        public DbSet<MinorSkillTemplate> MinorSkillTemplates { get; set; }
        public DbSet<LifeTemplate> LifeTemplates { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            var entity = modelBuilder.Entity<Entity>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).HasDefaultValue(Guid.NewGuid());
            }
            );

            var creature = modelBuilder.Entity<Creature>();
            creature.ToTable("Creatures");
        }
    }
}
