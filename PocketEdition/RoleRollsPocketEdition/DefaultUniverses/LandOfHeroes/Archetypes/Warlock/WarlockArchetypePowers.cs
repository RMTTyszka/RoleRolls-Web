using System.Collections.Concurrent;
using RoleRollsPocketEdition.Archetypes.Models;
using RoleRollsPocketEdition.Powers.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static class WarlockArchetypePowers
{
    private static readonly ConcurrentDictionary<string, PowerDescriptionModel> _byName = new(StringComparer.OrdinalIgnoreCase);

    static WarlockArchetypePowers()
    {
        // Migração inicial: poderes já existentes nos arquivos .md

        // HEX (nível 0)
        Add(new PowerDescriptionModel
        {
            Name = "Hex",
            Level = 0,
            Description = "Aplica uma maldição ao alvo uma vez por rodada.",
            // Campos estruturados poderão ser refinados com sua validação
            GameDescription = @"Uma vez por rodada, uma criatura afetada por uma magia, ataque ou habilidade recebe uma maldição.
Uma criatura não pode receber mais de uma maldição.

**Efeitos da Maldição**
- -2 Acerto
- Reduz deslocamento em 1/3
- -2 Esquiva
- -2 Defesa"
        });

        // LINHAGEM (nível 1)
        Add(new PowerDescriptionModel
        {
            Name = "Linhagem",
            Level = 1,
            Description = "Escolha a linhagem e receba a passiva correspondente.",
            GameDescription = @"### Bruxo
Qualquer magia que causaria dano arcano, causa dano profano.
Passiva: o primeiro poder usado em conjunto com sua maldição a cada combate, que gastaria mana, não gasta mana.

### Feticeiro
Qualquer magia que causaria dano profano, causa dano arcano.
Passiva: pode usar 2 de mana para aumentar o círculo de um feitiço."
        });

        // MALDIÇÃO MACABRA (nível 1)
        Add(new PowerDescriptionModel
        {
            Name = "Maldição Macabra",
            Level = 1,
            Description = "Evolução do Hex: efeitos adicionais conforme o estado da maldição no alvo.",
            UsageMode = PowerUsageMode.PerResource,
            UsageResourceId = LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Mana],
            UsageResourceCost = 1,
            TargetType = TargetType.Target,
            ActionType = PowerActionType.Reactive,
            Trigger = "Quando um alvo for afetado por sua magia, ataque ou habilidade.",
            GameDescription = @"Causa os seguintes efeitos, dependendo da maldição atual no alvo:

- Recebe **Azar I** no acerto
- Reduz deslocamento em 2/3
- Aliado que atacou o alvo recebe **Sorte I**"
        });
    }

    public static void Add(PowerDescriptionModel model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.Name)) return;
        _byName[model.Name] = model;
    }

    public static PowerDescriptionModel? Get(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        return _byName.TryGetValue(name, out var model) ? model : null;
    }
}
