using System;
using System.Collections.Generic;
using RoleRollsPocketEdition.Spells.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Spells;

public static class LandOfHeroesWarlockSpells
{
    // Stable IDs for Warlock spells
    public static readonly Guid MantoDasSombrasId = Guid.Parse("D6A0E2A3-2E2F-4D5D-A7A4-2B3D6E8C9F10");
    public static readonly Guid ClarividenciaId   = Guid.Parse("5E3B2F1C-A4D6-41B8-9C3D-7A2B1F6E5D40");
    public static readonly Guid MascaraId         = Guid.Parse("8C9F0E1D-2A3B-4C5D-9E7F-6A5B4C3D2E10");
    public static readonly Guid SomIlusorioId     = Guid.Parse("1A2B3C4D-5E6F-4781-92A3-B4C5D6E7F890");
    public static readonly Guid ControlarMortosId = Guid.Parse("AA0BCDEF-1234-4A56-89BC-DEF012345678");
    public static readonly Guid PoderEscuridaoId  = Guid.Parse("0F1E2D3C-4B5A-6978-89AB-CDEF01234567");
    public static readonly Guid ComunhaoEspiritosId = Guid.Parse("ABCDEF01-2345-4678-89AB-CDEF0123A456");

    private static readonly Dictionary<(Guid spellId, int circle), Guid> CircleIds = new()
    {
        { (MantoDasSombrasId, 1), Guid.Parse("c73b2d79-65c6-fd58-ea84-89d25b926acb") },
        { (MantoDasSombrasId, 2), Guid.Parse("197472df-1e3a-a8f4-f2f8-6df99948bdae") },
        { (MantoDasSombrasId, 3), Guid.Parse("03b4db76-ca59-29d0-4fc5-0c29d3bbdc3d") },
        { (MantoDasSombrasId, 4), Guid.Parse("ced557ab-e09b-1ecf-c585-26cd56719e5a") },
        { (MascaraId, 1), Guid.Parse("7f7996c2-2ac6-8d38-6b85-39882885f396") },
        { (MascaraId, 2), Guid.Parse("03de9b53-70b3-3945-336e-bf3074f19727") },
        { (MascaraId, 3), Guid.Parse("07da71ba-2cd8-0c86-4a24-4e9208ca72dc") },
        { (MascaraId, 4), Guid.Parse("ddfb07ff-7f13-b150-7f35-21fb91aeafac") },
        { (PoderEscuridaoId, 1), Guid.Parse("0238f2d8-738e-9886-fc0b-4c65645029ec") },
        { (PoderEscuridaoId, 2), Guid.Parse("dd41d32a-fda7-1e11-f250-1e98c4c70eec") },
        { (PoderEscuridaoId, 3), Guid.Parse("02c23760-6afd-725f-2ef1-779765f1072d") },
        { (PoderEscuridaoId, 4), Guid.Parse("b8831dd7-767f-c689-c515-5872af6f9bdc") },
        { (ControlarMortosId, 1), Guid.Parse("ebc45bd3-0a89-059a-06eb-84dd7748c5df") },
        { (ControlarMortosId, 2), Guid.Parse("54569236-b61c-cd98-777f-c8d0c764f43d") },
        { (ControlarMortosId, 3), Guid.Parse("aee61db1-9ef0-a8b3-fb45-e1986d681d3e") },
        { (ControlarMortosId, 4), Guid.Parse("82dc0eea-0619-310c-bc60-892f1865dc15") },
        { (ClarividenciaId, 1), Guid.Parse("2a111069-9ffc-a30d-2ec1-da01b1aba31c") },
        { (ClarividenciaId, 2), Guid.Parse("b4c449b9-c7a9-836c-0605-11fc47c02b72") },
        { (ClarividenciaId, 3), Guid.Parse("ecbf5680-e95e-5801-58b8-51b029e5de8a") },
        { (ClarividenciaId, 4), Guid.Parse("a8fa74f7-ba3d-0050-23d5-bc4a51f23b77") },
        { (SomIlusorioId, 1), Guid.Parse("6fedfa34-2c06-d081-2eb7-8bb4da3015e1") },
        { (SomIlusorioId, 2), Guid.Parse("51c5d8b1-3976-1457-d2d9-9cb897c9594b") },
        { (SomIlusorioId, 3), Guid.Parse("c1e9b640-5078-c171-a8bb-fec59e70818b") },
        { (SomIlusorioId, 4), Guid.Parse("7dca1f03-e2fb-d37f-6a58-5d73c4c5fed4") },
        { (ComunhaoEspiritosId, 1), Guid.Parse("f8078952-afde-a679-c0ad-45e6af791e35") },
        { (ComunhaoEspiritosId, 2), Guid.Parse("f93e9b1a-5a7b-6aa1-6c92-fe0b130c6265") },
        { (ComunhaoEspiritosId, 3), Guid.Parse("d2c16edf-0f42-bacd-7aa2-b3fc716e1b6c") },
        { (ComunhaoEspiritosId, 4), Guid.Parse("330ba5ec-37d6-4a9a-eddb-e8527d16fbbd") }
    };

    private static Guid GetCircleId(Guid spellId, int circle)
    {
        if (!CircleIds.TryGetValue((spellId, circle), out var id))
        {
            throw new InvalidOperationException($"Circle id not configured for spell {spellId} circle {circle}");
        }

        return id;
    }

    public static List<Spell> Build()
    {
        return new List<Spell>
        {
            BuildMantoDasSombras(),
            BuildClarividencia(),
            BuildMascara(),
            BuildSomIlusorio(),
            BuildControlarOsMortos(),
            BuildPoderDaEscuridao(),
            BuildComunhaoComEspiritos()
        };
    }

    private static Spell BuildMantoDasSombras() => new Spell
    {
        Id = MantoDasSombrasId,
        Name = "Manto das Sombras",
        Description = "Se esconde da visão do inimigo.",
        Circles =
        [
            new SpellCircle
            {
                Id = GetCircleId(MantoDasSombrasId, 1),
                Circle = 1,
                Title = "Manto das Sombras",
                InGameDescription = "Manto inicial que envolve o conjurador e aliados em penumbra.",
                EffectDescription = "Para cada 2 de CD: +1 de bônus em Nimbleness; afeta 1 criatura por dado. O efeito termina ao interagir com outra criatura.",
                CastingTime = "1 turno",
                Duration = "até 1 minuto ou ao interagir",
                Requirements = "Warlock, foco de sombras",
                LevelRequirement = 1
            },
            new SpellCircle
            {
                Id = GetCircleId(MantoDasSombrasId, 2),
                Circle = 2,
                Title = "Cloak of the Hidden",
                InGameDescription = "Sombras se ajustam aos movimentos, permitindo agir furtivamente mesmo em ação.",
                EffectDescription = "Pode se locomover na velocidade normal enquanto anda furtivo.",
                CastingTime = "1 turno",
                Duration = "até 10 minutos, com concentração",
                Requirements = "Warlock, foco de sombras",
                LevelRequirement = 4
            },
            new SpellCircle
            {
                Id = GetCircleId(MantoDasSombrasId, 3),
                Circle = 3,
                Title = "Shroud of Silence",
                InGameDescription = "A escuridão se torna absoluta; parado, o alvo se funde ao ambiente.",
                EffectDescription = "+2 de bônus para cada 2 de CD. Ficar parado por 2 turnos torna completamente invisível.",
                CastingTime = "1 turno",
                Duration = "até 10 minutos",
                Requirements = "Warlock, foco de sombras",
                LevelRequirement = 8
            },
            new SpellCircle
            {
                Id = GetCircleId(MantoDasSombrasId, 4),
                Circle = 4,
                Title = "Eclipse Mantle",
                InGameDescription = "Domínio total das sombras; pode interagir e se mover entre seres vivos sem quebrar o efeito.",
                EffectDescription = "Fica invisível em 1 turno; contato com outras criaturas não encerra o efeito.",
                CastingTime = "1 turno",
                Duration = "até 10 minutos",
                Requirements = "Warlock, foco de sombras",
                LevelRequirement = 12
            }
        ]
    };

    private static Spell BuildClarividencia() => new Spell
    {
        Id = ClarividenciaId,
        Name = "Clarividência",
        Description = "Ver e ouvir à distância por aliados, vínculos ou fetiches.",
        Circles =
        [
            new SpellCircle
            {
                Id = GetCircleId(ClarividenciaId, 1),
                Circle = 1,
                Title = "Olho do Fetiche",
                InGameDescription = "A visão se estende por laços de confiança ou por fetiches preparados.",
                EffectDescription = "Observar aliado à distância ou através de estatuetas vodu. 1 estatueta ativa a cada 5 níveis de Warlock.",
                CastingTime = "10 minutos por fonte",
                Duration = "(soma dos dados do Teste de Poder) x 10 min x Nível",
                Requirements = "Warlock, foco de adivinhação",
                LevelRequirement = 1
            },
            new SpellCircle
            {
                Id = GetCircleId(ClarividenciaId, 2),
                Circle = 2,
                Title = "Olho do Vínculo",
                InGameDescription = "O olhar encontra o inimigo por meio do que lhe pertence.",
                EffectDescription = "Observar um inimigo à distância com objeto pessoal do alvo. Estatuetas podem ser posicionadas previamente.",
                CastingTime = "10 minutos por fonte",
                Duration = "(soma dos dados do Teste de Poder) x 10 min x Nível",
                Requirements = "Warlock, foco de adivinhação",
                LevelRequirement = 4
            },
            new SpellCircle
            {
                Id = GetCircleId(ClarividenciaId, 3),
                Circle = 3,
                Title = "Passos do Fetiche",
                InGameDescription = "Os fetiches ganham passos, e o toque recente abre caminho à visão.",
                EffectDescription = "Mover estatueta até 3 m/turno enquanto observa. Se tocou o inimigo nos últimos 10 min, dispensa objeto do alvo.",
                CastingTime = "10 minutos por fonte",
                Duration = "(soma dos dados do Teste de Poder) x 10 min x Nível (base)",
                Requirements = "Warlock, foco de adivinhação",
                LevelRequirement = 8
            },
            new SpellCircle
            {
                Id = GetCircleId(ClarividenciaId, 4),
                Circle = 4,
                Title = "Olhar Marcante do Vodu",
                InGameDescription = "Nem distâncias, nem muros de mundo detêm o olhar.",
                EffectDescription = "Distância sem limite. Para estatuetas, tempo só consome quando ativas.",
                CastingTime = "10 minutos por fonte",
                Duration = "(soma dos dados do Teste de Poder) x 10 min x Nível (base)",
                Requirements = "Warlock, foco de adivinhação",
                LevelRequirement = 12
            }
        ]
    };

    private static Spell BuildMascara() => new Spell
    {
        Id = MascaraId,
        Name = "Máscara",
        Description = "Molda aparências para enganar olhos e expectativas.",
        Circles =
        [
            new SpellCircle
            {
                Id = GetCircleId(MascaraId, 1),
                Circle = 1,
                Title = "Máscara do Traje",
                InGameDescription = "O tecido mente por você; cores, cortes e símbolos se dobram ao capricho.",
                EffectDescription = "Alterar aparência de roupas/armas visíveis; +1 em Blefar quando relevante.",
                CastingTime = "1 turno",
                Duration = "Inteligência x Nível x 2 min",
                Requirements = "Warlock, foco de ilusão",
                LevelRequirement = 1
            },
            new SpellCircle
            {
                Id = GetCircleId(MascaraId, 2),
                Circle = 2,
                Title = "Rosto Moldável",
                InGameDescription = "Traços, altura e porte se rearranjam como cera morna.",
                EffectDescription = "Alterar aparência física superficial; mantém +1 em Blefar quando apropriado.",
                CastingTime = "1 turno",
                Duration = "Inteligência x Nível x 3 min",
                Requirements = "Warlock, foco de ilusão",
                LevelRequirement = 4
            },
            new SpellCircle
            {
                Id = GetCircleId(MascaraId, 3),
                Circle = 3,
                Title = "Cenário Ilusório",
                InGameDescription = "Quatro paredes vestem outro papel.",
                EffectDescription = "Altera aparência de um cômodo médio (~10x10x3m). Não bloqueia passagem/sons.",
                CastingTime = "1 turno",
                Duration = "Inteligência x Nível x 4 min",
                AreaOfEffect = "Cômodo médio (~10x10x3m)",
                Requirements = "Warlock, foco de ilusão",
                LevelRequirement = 8
            },
            new SpellCircle
            {
                Id = GetCircleId(MascaraId, 4),
                Circle = 4,
                Title = "Persona Irrefutável",
                InGameDescription = "A máscara torna-se identidade.",
                EffectDescription = "+4 em Blefar quando relevante; interações comuns não desfazem o efeito.",
                CastingTime = "1 turno",
                Duration = "Inteligência x Nível x 5 min",
                Requirements = "Warlock, foco de ilusão",
                LevelRequirement = 12
            }
        ]
    };

    private static Spell BuildSomIlusorio() => new Spell
    {
        Id = SomIlusorioId,
        Name = "Som Ilusório",
        Description = "Cria, molda ou suprime sons para enganar sentidos.",
        Circles =
        [
            new SpellCircle
            {
                Id = GetCircleId(SomIlusorioId, 1),
                Circle = 1,
                Title = "Ecos Fabricados",
                InGameDescription = "Onde não há fonte, o som nasce.",
                EffectDescription = "Cria som ilusório com potência equivalente ao PE, enganando criaturas de nível até o PE.",
                CastingTime = "12 turnos",
                Requirements = "Warlock, foco de ilusão",
                LevelRequirement = 1
            },
            new SpellCircle
            {
                Id = GetCircleId(SomIlusorioId, 2),
                Circle = 2,
                Title = "Silêncio Conveniente",
                InGameDescription = "O mundo prende a respiração.",
                EffectDescription = "Remove o som de uma área. Penalidade em ouvir igual ao Teste de Poder, +1 por cada 2 de CD.",
                CastingTime = "12 turnos",
                AreaOfEffect = "Área a definir",
                Requirements = "Warlock, foco de ilusão",
                LevelRequirement = 4
            },
            new SpellCircle
            {
                Id = GetCircleId(SomIlusorioId, 3),
                Circle = 3,
                Title = "Gatilho Sonoro",
                InGameDescription = "A mentira aguarda o momento certo.",
                EffectDescription = "Define gatilho simples para ativar/desativar o som ilusório.",
                CastingTime = "12 turnos",
                Requirements = "Warlock, foco de ilusão",
                LevelRequirement = 8
            },
            new SpellCircle
            {
                Id = GetCircleId(SomIlusorioId, 4),
                Circle = 4,
                Title = "Clamor Irrefutável",
                InGameDescription = "O som cresce, domina e confunde sem rastro de dúvida.",
                EffectDescription = "Aumenta potência, alcance e fidelidade; múltiplas fontes e detalhes finos.",
                CastingTime = "12 turnos",
                Requirements = "Warlock, foco de ilusão",
                LevelRequirement = 12
            }
        ]
    };

    private static Spell BuildControlarOsMortos() => new Spell
    {
        Id = ControlarMortosId,
        Name = "Controlar os Mortos",
        Description = "Interroga, move e devolve fôlego aos que partiram.",
        Circles =
        [
            new SpellCircle
            {
                Id = GetCircleId(ControlarMortosId, 1),
                Circle = 1,
                Title = "Voz do Sepulcro",
                InGameDescription = "As cordas do além sussurram por bocas imóveis.",
                EffectDescription = "Para cada sucesso no PE: 1 pergunta a morto de até Nível dias. Dano encerra.",
                CastingTime = "10 minutos",
                Duration = "até concluir perguntas ou sofrer dano",
                Requirements = "Warlock, foco de necromancia",
                LevelRequirement = 1
            },
            new SpellCircle
            {
                Id = GetCircleId(ControlarMortosId, 2),
                Circle = 2,
                Title = "Marionete Cadavérica",
                InGameDescription = "O corpo obedece ordens simples, sem alma nem memória.",
                EffectDescription = "Controla corpo de um morto; segue instruções simples e comunica-se com baixa inteligência. Dano encerra.",
                CastingTime = "10 minutos",
                Duration = "até 10 minutos, com concentração",
                Requirements = "Warlock, foco de necromancia",
                LevelRequirement = 4
            },
            new SpellCircle
            {
                Id = GetCircleId(ControlarMortosId, 3),
                Circle = 3,
                Title = "Possessão Cadavérica",
                InGameDescription = "Sua vontade habita a casca; a carne lembra o vivo.",
                EffectDescription = "Possui cadáver; aparência parcialmente viva; usa inteligência do conjurador. Dano encerra.",
                CastingTime = "10 minutos",
                Duration = "até 10 minutos, com concentração",
                Requirements = "Warlock, foco de necromancia",
                LevelRequirement = 8
            },
            new SpellCircle
            {
                Id = GetCircleId(ControlarMortosId, 4),
                Circle = 4,
                Title = "Chamado à Vida",
                InGameDescription = "O sopro retorna por tempo contado.",
                EffectDescription = "Ressuscita por tempo limitado; danos fazem falecer novamente.",
                CastingTime = "10 minutos",
                Duration = "PE x horas",
                Requirements = "Warlock, foco de necromancia",
                LevelRequirement = 12
            }
        ]
    };

    private static Spell BuildPoderDaEscuridao() => new Spell
    {
        Id = PoderEscuridaoId,
        Name = "Poder da Escuridão",
        Description = "Convoca trevas densas para cegar, ocultar e transitar entre sombras.",
        Circles =
        [
            new SpellCircle
            {
                Id = GetCircleId(PoderEscuridaoId, 1),
                Circle = 1,
                Title = "Véu de Trevas",
                InGameDescription = "A luz vacila; olhos falham.",
                EffectDescription = "Cria área de escuridão; penalidade em observar igual ao Teste de Poder.",
                CastingTime = "1 turno",
                Duration = "até 10 minutos, com concentração",
                AreaOfEffect = "Área a definir",
                Requirements = "Warlock, foco de sombras",
                LevelRequirement = 1
            },
            new SpellCircle
            {
                Id = GetCircleId(PoderEscuridaoId, 2),
                Circle = 2,
                Title = "Manto da Noite",
                InGameDescription = "Sombras se prendem à pele, abafando passos e contornos.",
                EffectDescription = "+1 em Furtividade/Esconder-se por cada 2 de CD; invisível parado em sombra.",
                CastingTime = "1 turno",
                Duration = "até 10 minutos, com concentração",
                Requirements = "Warlock, foco de sombras",
                LevelRequirement = 4
            },
            new SpellCircle
            {
                Id = GetCircleId(PoderEscuridaoId, 3),
                Circle = 3,
                Title = "Sombra Vinculada ou Caminhante",
                InGameDescription = "As trevas seguem o alvo — ou abrem passagens curtas entre sombras.",
                EffectDescription = "(a) Âncora: área fixa em objeto/criatura; (b) Caminhante: ganha 2º círculo e 1 teleporte/turno até 9 m para sombra em visão.",
                CastingTime = "1 turno",
                Duration = "até 10 minutos, com concentração",
                Requirements = "Warlock, foco de sombras",
                LevelRequirement = 8
            },
            new SpellCircle
            {
                Id = GetCircleId(PoderEscuridaoId, 4),
                Circle = 4,
                Title = "Eclipse Profundo",
                InGameDescription = "As sombras dominam o espaço; passos sombrios atravessam o véu.",
                EffectDescription = "Aumenta muito a área; alvo pode entrar no ‘mundo das sombras’ e teleportar sem linha de visão dentro do alcance.",
                CastingTime = "1 turno",
                Duration = "até 10 minutos, com concentração",
                Requirements = "Warlock, foco de sombras",
                LevelRequirement = 12
            }
        ]
    };

    private static Spell BuildComunhaoComEspiritos() => new Spell
    {
        Id = ComunhaoEspiritosId,
        Name = "Comunhão com Espíritos",
        Description = "Convoca espíritos vigilantes e mensageiros.",
        Circles =
        [
            new SpellCircle
            {
                Id = GetCircleId(ComunhaoEspiritosId, 1),
                Circle = 1,
                Title = "Vigias Etéreos",
                InGameDescription = "Olhos do além montam guarda em silêncio.",
                EffectDescription = "Vigiam área; notificam eventos; acordam durante descanso longo ao detectar gatilhos.",
                CastingTime = "10 minutos",
                Duration = "PE x horas ou até fim do descanso longo",
                Requirements = "Warlock, foco de espíritos",
                LevelRequirement = 1
            },
            new SpellCircle
            {
                Id = GetCircleId(ComunhaoEspiritosId, 2),
                Circle = 2,
                Title = "Batedores do Vento",
                InGameDescription = "Sussurros atravessam paredes e retornam com relatos.",
                EffectDescription = "Atravessam paredes e obstáculos não mágicos; relata o visto/ouvido.",
                CastingTime = "10 minutos",
                Duration = "até 10 minutos, com concentração",
                Requirements = "Warlock, foco de espíritos",
                LevelRequirement = 4
            },
            new SpellCircle
            {
                Id = GetCircleId(ComunhaoEspiritosId, 3),
                Circle = 3,
                Title = "Mãos Sussurrantes",
                InGameDescription = "Pequenos toques que giram chaves e empurram ferrolhos.",
                EffectDescription = "Interagem levemente (puxar/empurrar/maçanetas); abrir portas simples. Testes de abrir/furtar podem usar Conhecimento Arcano.",
                CastingTime = "10 minutos",
                Duration = "até 10 minutos, com concentração",
                Requirements = "Warlock, foco de espíritos",
                LevelRequirement = 8
            },
            new SpellCircle
            {
                Id = GetCircleId(ComunhaoEspiritosId, 4),
                Circle = 4,
                Title = "Elo Anímico",
                InGameDescription = "Intenções reveladas no toque do espírito.",
                EffectDescription = "Tocam espíritos de vivos e identificam intenções (pressentimento com Conhecimento Arcano, +1 por cada 2 de CD).",
                CastingTime = "10 minutos",
                Duration = "até 10 minutos, com concentração",
                Requirements = "Warlock, foco de espíritos",
                LevelRequirement = 12
            }
        ]
    };
}


