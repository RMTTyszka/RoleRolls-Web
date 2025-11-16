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


