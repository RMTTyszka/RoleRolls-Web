using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Spells.Entities;

namespace RoleRollsPocketEdition.Spells.Models;

public class SpellModel : IEntityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<SpellCircleModel> Circles { get; set; } = new();

    public string MdDescription
    {
        get
        {
            var sb = new System.Text.StringBuilder();
            if (!string.IsNullOrWhiteSpace(Name))
            {
                sb.AppendLine($"## {Name}");
                sb.AppendLine();
            }
            if (!string.IsNullOrWhiteSpace(Description))
            {
                sb.AppendLine(Description.Trim());
                sb.AppendLine();
            }

            var first = Circles.OrderBy(c => c.Circle).FirstOrDefault();
            if (first != null)
            {
                if (!string.IsNullOrWhiteSpace(first.Requirements))
                    sb.AppendLine($"**Requisitos:** {first.Requirements}  ");
                if (!string.IsNullOrWhiteSpace(first.CastingTime))
                    sb.AppendLine($"**Tempo de Preparo:** {first.CastingTime}  ");
                sb.AppendLine();
            }

            foreach (var circle in Circles.OrderBy(c => c.Circle))
            {
                var ordinal = $"{circle.Circle}º Círculo";
                sb.AppendLine($"### {ordinal} - {circle.Title}");
                sb.AppendLine();
                if (!string.IsNullOrWhiteSpace(circle.InGameDescription))
                {
                    sb.AppendLine($"*{circle.InGameDescription.Trim()}*");
                    sb.AppendLine();
                }
                if (!string.IsNullOrWhiteSpace(circle.EffectDescription))
                    sb.AppendLine($"**{circle.EffectDescription.Trim()}**  ");
                if (circle.LevelRequirement > 0)
                    sb.AppendLine($"**Nível Requerido:** {circle.LevelRequirement}  ");
                if (!string.IsNullOrWhiteSpace(circle.Duration))
                    sb.AppendLine($"**Duração:** {circle.Duration.Trim()}  ");
                if (!string.IsNullOrWhiteSpace(circle.AreaOfEffect))
                    sb.AppendLine($"**Área:** {circle.AreaOfEffect.Trim()}  ");
                if (!string.IsNullOrWhiteSpace(circle.Requirements))
                    sb.AppendLine($"**Requisitos (círculo):** {circle.Requirements.Trim()}  ");
                if (!string.IsNullOrWhiteSpace(circle.CastingTime))
                    sb.AppendLine($"**Tempo de Preparo (círculo):** {circle.CastingTime.Trim()}  ");

                sb.AppendLine();
            }

            return sb.ToString().TrimEnd();
        }
    }

    public SpellModel()
    {
    }

    public SpellModel(Spell spell)
    {
        Id = spell.Id;
        Name = spell.Name;
        Description = spell.Description;
        Circles = (spell.Circles ?? new List<SpellCircle>())
            .Select(c => new SpellCircleModel(c))
            .ToList();
    }
}
