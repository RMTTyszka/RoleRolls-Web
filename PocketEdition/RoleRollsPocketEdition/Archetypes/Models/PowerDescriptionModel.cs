using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Powers.Entities;
using RoleRollsPocketEdition.Powers.Models;

namespace RoleRollsPocketEdition.Archetypes.Models;

public class PowerDescriptionModel : IEntityDto
{
    public Guid Id { get; set; }

    public PowerDescriptionModel()
    {
    }

    public PowerDescriptionModel(ArchertypePowerDescription archertypePowerDescription)
    {
        Name = archertypePowerDescription.Name;
        Id = archertypePowerDescription.Id;
        Description = archertypePowerDescription.Description;
        GameDescription = archertypePowerDescription.GameDescription;
        Level = archertypePowerDescription.RequiredLevel;
    }

    public int Level { get; set; }

    public string GameDescription { get; set; }

    public string Description { get; set; }

    public string Name { get; set; }

    // Optional structured fields for richer, English-named metadata
    public PowerType Type { get; set; }
    public PowerActionType ActionType { get; set; }
    public PowerDurationType DurationType { get; set; }
    public int? Duration { get; set; }
    public PowerUsageType? UsageType { get; set; }
    public string UsagesFormula { get; set; } = string.Empty;
    public TargetType TargetType { get; set; }
    public string CastFormula { get; set; } = string.Empty;
    public string Trigger { get; set; } = string.Empty;
    public string CastDescription { get; set; } = string.Empty;
    public Guid? UseAttributeId { get; set; }

    public Guid? TargetDefenseId { get; set; }

    // Unified usage limiting: by count (per interval) or by resource
    public int? NumberOfUsages { get; set; } // when UsageMode is PerEncounter/PerDay/PerSession
    public Guid? UsageResourceId { get; set; } // when UsageMode is PerResource (e.g., Mana Vitality Id)
    public int? UsageResourceCost { get; set; } // fixed cost for the resource mode

    // Portuguese Markdown rendering for UI
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

            // Meta line
            var metaParts = new List<string>();
            if (Type != default)
                metaParts.Add($"Tipo: {Type}");
            if (ActionType != default)
                metaParts.Add($"Ação: {ActionType}");
            if (DurationType != default)
            {
                var dur = Duration.HasValue && DurationType == PowerDurationType.Turns
                    ? $" ({Duration} turnos)"
                    : string.Empty;
                metaParts.Add($"Duração: {DurationType}{dur}");
            }

            // Back-compat with legacy UsageType/UsagesFormula, if provided
            if (UsageType.HasValue)
            {
                var usage = string.IsNullOrWhiteSpace(UsagesFormula) ? string.Empty : $" — {UsagesFormula}";
                metaParts.Add($"Frequência: {UsageType}{usage}");
            }

            // New unified usage rendering
            switch (UsageType)
            {
                case PowerUsageType.Encounter:
                    if (NumberOfUsages.HasValue)
                        metaParts.Add($"Usos: {NumberOfUsages.Value} por combate");
                    break;
                case PowerUsageType.Day:
                    if (NumberOfUsages.HasValue)
                        metaParts.Add($"Usos: {NumberOfUsages.Value} por dia");
                    break;
                case PowerUsageType.Session:
                    if (NumberOfUsages.HasValue)
                        metaParts.Add($"Usos: {NumberOfUsages.Value} por sessão");
                    break;
                case PowerUsageType.Resource:
                    if (UsageResourceId.HasValue && UsageResourceCost.HasValue)
                        metaParts.Add($"Custo: {UsageResourceCost.Value} (vitalidade)");
                    break;
                case PowerUsageType.Continuous:
                    metaParts.Add("Uso: Contínuo");
                    break;
            }

            if (TargetType != default)
                metaParts.Add($"Alvo: {TargetType}");

            if (metaParts.Count > 0)
            {
                sb.AppendLine(string.Join(" | ", metaParts));
                sb.AppendLine();
            }

            // Activation details (Trigger/Reaction)
            if (ActionType == PowerActionType.Reactive && !string.IsNullOrWhiteSpace(Trigger))
            {
                sb.AppendLine($"**Reação (Gatilho):** {Trigger.Trim()}");
                if (!string.IsNullOrWhiteSpace(CastFormula))
                    sb.AppendLine($"**Fórmula:** {CastFormula.Trim()}");
                sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(Trigger))
            {
                sb.AppendLine($"**Gatilho:** {Trigger.Trim()}");
                if (!string.IsNullOrWhiteSpace(CastFormula))
                    sb.AppendLine($"**Fórmula:** {CastFormula.Trim()}");
                sb.AppendLine();
            }

            if (!string.IsNullOrWhiteSpace(CastDescription) || !string.IsNullOrWhiteSpace(CastFormula))
            {
                var conjParts = new List<string>();
                if (!string.IsNullOrWhiteSpace(CastDescription))
                    conjParts.Add(CastDescription.Trim());
                if (!string.IsNullOrWhiteSpace(CastFormula))
                    conjParts.Add($"Fórmula: {CastFormula.Trim()}");
                sb.AppendLine($"**Conjuração:** {string.Join(" — ", conjParts)}");
                sb.AppendLine();
            }

            if (Level > 0)
            {
                sb.AppendLine($"**Nível Requerido:** {Level}");
                sb.AppendLine();
            }

            if (!string.IsNullOrWhiteSpace(GameDescription))
            {
                sb.AppendLine(GameDescription.Trim());
                sb.AppendLine();
            }

            return sb.ToString().TrimEnd();
        }
    }
}


