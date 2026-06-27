# RoleRolls Business Rules Documentation Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Criar a primeira documentacao completa da regra de negocio do sistema base `RoleRolls`, separando com clareza o que e runtime real, o que e premissa de testes/balance e onde existem divergencias.

**Architecture:** A documentacao sera dividida em arquivos pequenos por tema dentro de `docs/rolerolls-system/`. Cada secao importante seguira a mesma estrutura: `Implementado hoje`, `Assumido por testes/balance`, `Divergencias e observacoes` e `Fontes`. O trabalho sera guiado por evidencias concretas do codigo, dos testes e dos documentos ja existentes, sem inventar regra nova.

**Tech Stack:** Markdown, ripgrep (`rg`), codigo C# existente em `Creatures/`, `Rolls/`, `Attacks/`, `Itens/`, `DefaultUniverses/LandOfHeroes/`, testes em `UnitTests/`.

---

## File Map

- Create: `docs/rolerolls-system/README.md`
  Responsabilidade: indice da documentacao, escopo, como ler as duas camadas de verdade e ordem recomendada de leitura.
- Create: `docs/rolerolls-system/01-visao-geral.md`
  Responsabilidade: explicar o sistema base, seus limites e o fluxo macro criatura -> propriedade -> roll -> combate -> vitalidade.
- Create: `docs/rolerolls-system/02-modelo-da-criatura.md`
  Responsabilidade: descrever os blocos centrais de `Creature`: atributos, skills, minor skills, vitalidades, defesas, equipamentos, bonuses e conditions.
- Create: `docs/rolerolls-system/03-progressao.md`
  Responsabilidade: registrar estado inicial real, limites de pontos, `LevelUp()` e a progressao artificial usada pelos testes.
- Create: `docs/rolerolls-system/04-resolucao-de-propriedades-e-formulas.md`
  Responsabilidade: explicar como `Property` vira numero e quais formulas/tokens movem `Life`, `Moral`, `Mana` e `Evasion`.
- Create: `docs/rolerolls-system/05-rolls.md`
  Responsabilidade: documentar o pipeline real de `Roll` e suas diferencas para os modelos abstratos de teste.
- Create: `docs/rolerolls-system/06-combate.md`
  Responsabilidade: documentar `Attack`, o calculo de hits, block, dano, grip, tier e o papel dos excessos acima da defesa.
- Create: `docs/rolerolls-system/07-evade-e-defesas.md`
  Responsabilidade: documentar `Evasion`, o metodo `Evade`, e a diferenca entre o fluxo testado e o fluxo realmente exposto pelo servico.
- Create: `docs/rolerolls-system/08-vitalidades-e-condicoes.md`
  Responsabilidade: documentar formulas de vitalidade, ordem de consumo, cascata de dano e thresholds de condicao.
- Create: `docs/rolerolls-system/09-contratos-de-teste-e-balance.md`
  Responsabilidade: documentar o que os testes travam com numeros, o que travam por tendencia e o que e apenas observacao de balance.
- Create: `docs/rolerolls-system/10-divergencias-e-pendencias.md`
  Responsabilidade: consolidar as inconsistencias e pontos pendentes, com destaque para dano, `Evade`, progressao e parametros nao usados.

## Primary Source Files

- `UnitTests/README.md`
- `UnitTests/Core/BaseCreature.cs`
- `UnitTests/Rolls/SkillAndAttributeRollTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/SkillAndAttributeDcTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/RpgBalanceDesignTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/BalanceSummary.md`
- `UnitTests/Attacks/Services/AttackServiceTests/HitPointScalingFormulas.md`
- `Creatures/Entities/Creature.cs`
- `Creatures/Entities/CreaturePropertyResolver.cs`
- `Creatures/Entities/CreatureAttack.cs`
- `Creatures/Entities/CreatureDefend.cs`
- `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs`
- `Creatures/Entities/Vitality.cs`
- `Creatures/Entities/Attribute.cs`
- `Creatures/Entities/SpecificSkill.cs`
- `Creatures/Entities/Skill.cs`
- `Rolls/Entities/Roll.cs`
- `Attacks/Services/AttackService.cs`
- `Itens/Configurations/ArmorDefinition.cs`
- `Itens/Configurations/ItemConfiguration.cs`
- `Itens/GripType.cs`
- `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs`

### Task 1: Create Index And Overview Files

**Files:**
- Create: `docs/rolerolls-system/README.md`
- Create: `docs/rolerolls-system/01-visao-geral.md`
- Reference: `docs/superpowers/specs/2026-06-27-rolerolls-business-rules-documentation-design.md`
- Reference: `UnitTests/README.md:1-225`
- Reference: `Creatures/Entities/Creature.cs:27-112`
- Reference: `Attacks/Services/AttackService.cs:38-87`

- [ ] **Step 1: Create the documentation index file**

```md
# RoleRolls System

## Escopo

Esta pasta documenta a regra de negocio do sistema base `RoleRolls`.
Ela nao tenta documentar ainda o conteudo completo dos `DefaultUniverses`.

## Como Ler Esta Documentacao

- `Implementado hoje`: comportamento confirmado no runtime atual.
- `Assumido por testes/balance`: comportamento tratado como contrato ou modelo pelos testes e docs auxiliares.
- `Divergencias e observacoes`: conflitos, simplificacoes e pontos pendentes.

## Ordem Recomendada

1. `01-visao-geral.md`
2. `02-modelo-da-criatura.md`
3. `03-progressao.md`
4. `04-resolucao-de-propriedades-e-formulas.md`
5. `05-rolls.md`
6. `06-combate.md`
7. `07-evade-e-defesas.md`
8. `08-vitalidades-e-condicoes.md`
9. `09-contratos-de-teste-e-balance.md`
10. `10-divergencias-e-pendencias.md`
```

- [ ] **Step 2: Create the overview file with the standard section structure**

```md
# Visao Geral

## Objetivo da secao

Explicar o que e o sistema base `RoleRolls`, quais sao seus limites e qual e o fluxo macro que conecta criatura, propriedades, rolagens, combate e vitalidades.

## Implementado hoje

- O aggregate central e `Creature`.
- O runtime calcula propriedades, rolagens, ataque e dano a partir de entidades e formulas do codigo.
- O fluxo de ataque exposto em producao passa por `AttackService` e chama `Creature.Attack(...)`.

## Assumido por testes/balance

- Os testes concentram a leitura do sistema em rolagens, ataque, evade, vitalidades e balance de arma x armadura.
- O `UnitTests/README.md` ja resume esse recorte como visao de alto nivel.

## Divergencias e observacoes

- O que os testes explicam nao cobre toda a regra viva do runtime.
- Parte da semantica de balance e um modelo abstrato, nao o fluxo real de producao.

## Fontes

- `UnitTests/README.md`
- `Creatures/Entities/Creature.cs`
- `Attacks/Services/AttackService.cs`
```

- [ ] **Step 3: Verify the source anchors for the overview text before expanding it**

Run:

```powershell
rg --line-number "Attack\(|ExecuteRoll|FromTemplate|UnitTests/README|AttackService" "Creatures/Entities/Creature.cs" "Attacks/Services/AttackService.cs" "UnitTests/README.md"
```

Expected: matches for `FromTemplate`, `ExecuteRoll`, `Attack(...)` and the overview text source in `UnitTests/README.md`.

- [ ] **Step 4: Expand both files with final prose and explicit links to the remaining section files**

```md
Adicione no `README.md` um bloco final `## Proxima Etapa` com esta frase:

`Depois desta documentacao do sistema base, a proxima camada prevista e a documentacao das regras de negocio dos DefaultUniverses.`

Adicione em `01-visao-geral.md` um bloco final `## Mapa do fluxo` com esta lista:

1. `Creature` nasce de um `CampaignTemplate`.
2. `Property` e resolvida em numero.
3. `Roll` converte numero em dados, bonus e sucessos.
4. `Attack` converte sucessos em hits e dano.
5. `Vitality` recebe dano e pode expor condicoes.
```

- [ ] **Step 5: Verify the two files were created with the expected top-level headings**

Run:

```powershell
rg --line-number "^# " "docs/rolerolls-system/README.md" "docs/rolerolls-system/01-visao-geral.md"
```

Expected: `# RoleRolls System` and `# Visao Geral`.

### Task 2: Document Creature Model And Progression

**Files:**
- Create: `docs/rolerolls-system/02-modelo-da-criatura.md`
- Create: `docs/rolerolls-system/03-progressao.md`
- Reference: `Creatures/Entities/Creature.cs:27-112,116-159,688-723`
- Reference: `Creatures/Entities/Attribute.cs:7-35`
- Reference: `Creatures/Entities/SpecificSkill.cs:6-37`
- Reference: `Creatures/Entities/Skill.cs:7-55`
- Reference: `UnitTests/Core/BaseCreature.cs:15-113`

- [ ] **Step 1: Create the creature model file with concrete subsections**

```md
# Modelo Da Criatura

## Objetivo da secao

Descrever os elementos centrais que compoem `Creature` e como eles se relacionam.

## Implementado hoje

- `Creature` agrega `Attributes`, `Skills`, `Vitalities`, `Defenses`, `Equipment`, `Inventory` e `Bonuses`.
- `SpecificSkills` e uma visao derivada de `Skills.SelectMany(...)`.
- Atributos nascem com `Points = 1`.
- Specific skills nascem com `Points = 0`.

## Assumido por testes/balance

- O fixture `BaseCreature` sobe manualmente atributos para `3` e specific skills para `1`.
- O fixture equipa arma media e armadura media como baseline de combate.

## Divergencias e observacoes

- O baseline dos testes nao representa o estado inicial real de uma criatura do runtime.

## Fontes

- `Creatures/Entities/Creature.cs`
- `Creatures/Entities/Attribute.cs`
- `Creatures/Entities/SpecificSkill.cs`
- `Creatures/Entities/Skill.cs`
- `UnitTests/Core/BaseCreature.cs`
```

- [ ] **Step 2: Create the progression file with the exact runtime-vs-tests split**

```md
# Progressao

## Objetivo da secao

Registrar como a progressao existe hoje no codigo e como os testes modelam uma progressao separada para balance.

## Implementado hoje

- `Level` nasce em `1`.
- `LevelUp()` apenas incrementa o inteiro `Level`.
- `AddPointToAttribute()` respeita o teto `4 + Level / 6`.
- `AddPointToSkill()` e `AddPointToSpecificSkill()` respeitam `PointsLimit`.

## Assumido por testes/balance

- `BaseCreature.WithLevel(level)` distribui pontos por marcos.
- Atributos sobem em `6`, `11` e `16`.
- Skills e specific skills sobem em `4`, `8` e `12`.

## Divergencias e observacoes

- A progressao usada para balance vive no fixture de teste, nao no fluxo principal de runtime.

## Fontes

- `Creatures/Entities/Creature.cs`
- `UnitTests/Core/BaseCreature.cs`
```

- [ ] **Step 3: Verify the exact source lines for starting values and level mechanics**

Run:

```powershell
rg --line-number "Level = 1|Points = 1|LevelUp\(|AddPointToAttribute|AddPointToSkill|AddPointToSpecificSkill" "Creatures/Entities/Creature.cs" "Creatures/Entities/Attribute.cs" "UnitTests/Core/BaseCreature.cs"
```

Expected: matches for runtime defaults and the test-only milestone progression.

- [ ] **Step 4: Expand the two files with the concrete lists of components and limits**

```md
Adicione em `02-modelo-da-criatura.md` uma lista `## Componentes` com estas entradas:

- `Attributes`
- `Skills`
- `SpecificSkills`
- `Vitalities`
- `Defenses`
- `Equipment`
- `Inventory`
- `Bonuses`

Adicione em `03-progressao.md` uma lista `## Limites observados no codigo` com estas entradas:

- `MaxPointsPerSpecificSkill = 3 + Level - 1`
- `MinPointsPerSpecificSkill = 0`
- `MaxAttributePoints = 4 + Level / 6`
- `Skill.PointsLimit = 3 + SpecificSkills.Count - 1`
```

- [ ] **Step 5: Verify the new files contain the expected headings and limits**

Run:

```powershell
rg --line-number "^# |MaxPointsPerSpecificSkill|MaxAttributePoints|PointsLimit|BaseCreature.WithLevel" "docs/rolerolls-system/02-modelo-da-criatura.md" "docs/rolerolls-system/03-progressao.md"
```

Expected: the two titles plus the four limit strings in the progression file.

### Task 3: Document Property Resolution And Rolls

**Files:**
- Create: `docs/rolerolls-system/04-resolucao-de-propriedades-e-formulas.md`
- Create: `docs/rolerolls-system/05-rolls.md`
- Reference: `Creatures/Entities/CreaturePropertyResolver.cs:13-178`
- Reference: `Creatures/Entities/Creature.cs:240-314,316-577`
- Reference: `Rolls/Entities/Roll.cs:10-130`
- Reference: `UnitTests/Rolls/SkillAndAttributeRollTests.cs:18-310`
- Reference: `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs:47-257`

- [ ] **Step 1: Create the property-resolution file with the concrete property-type breakdown**

```md
# Resolucao De Propriedades E Formulas

## Objetivo da secao

Explicar como uma `Property` vira numero e como as formulas do sistema base sao avaliadas.

## Implementado hoje

- `PropertyType.Attribute` resolve para pontos do atributo.
- `PropertyType.Skill` hoje nao soma `Skill.Points` de forma util no resolver principal.
- `PropertyType.MinorSkill` combina atributo associado e pontos da specific skill.
- `PropertyType.Defense` e `PropertyType.Vitality` usam formulas.
- `PropertyType.CreatureCondition` resolve para `0`.

## Assumido por testes/balance

- Os testes costumam modelar atributo e skill como pools de dados separados para estudar chance e balance.

## Divergencias e observacoes

- O modelo abstrato de testes simplifica o resolver real.

## Fontes

- `Creatures/Entities/CreaturePropertyResolver.cs`
- `Creatures/Entities/Creature.cs`
- `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs`
```

- [ ] **Step 2: Create the rolls file with the real pipeline and test model split**

```md
# Rolls

## Objetivo da secao

Documentar o pipeline real de `Roll` e as diferencas para os modelos probabilisticos usados nos testes.

## Implementado hoje

- `NumberOfDices = PropertyValue + Advantage`, salvo quando ha `PredefinedRolls`.
- Cada dado tem sucesso quando `roll + Bonus >= Complexity`.
- O roll final tem sucesso quando `NumberOfSuccesses >= Difficulty`.
- `NumberOfRollSuccesses = NumberOfSuccesses / Difficulty`.
- `Luck` rerrola os menores ou maiores dados crus e fica com melhor ou pior resultado.

## Assumido por testes/balance

- Os testes procuram CDs perto de 50 porcento.
- O baseline de atributos e skills usado nos testes nao e o mesmo do runtime real.
- Os modelos abstratos de sorte usam regras mais contextuais do que o `Roll` real.

## Divergencias e observacoes

- A semantica de `Luck` nos docs de teste nao e identica a implementacao de `Roll.Process(...)`.

## Fontes

- `Rolls/Entities/Roll.cs`
- `UnitTests/Rolls/SkillAndAttributeRollTests.cs`
- `UnitTests/README.md`
```

- [ ] **Step 3: Verify the formulas and token anchors before writing the final prose**

Run:

```powershell
rg --line-number "PropertyType\.Attribute|PropertyType\.Skill|PropertyType\.MinorSkill|ResolveTier|ResolveGrowth|Formula = \"10 \+ Evasion|Formula = \"4 \* Vigor|Formula = \"4 \* Intuition|Formula = \"10 \+ 2 \* Intelligence" "Creatures/Entities/CreaturePropertyResolver.cs" "Creatures/Entities/Creature.cs" "DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs"
```

Expected: matches for the property resolver branches and the `Life`, `Moral`, `Mana` and `Evasion` formulas.

- [ ] **Step 4: Add a concrete formulas block and a concrete roll summary block**

```md
Adicione em `04-resolucao-de-propriedades-e-formulas.md` um bloco `## Formulas base do template` com estas linhas:

- `Life = 4 * Vigor + 2 * Level + Growth`
- `Moral = 4 * Intuition + 2 * Level + Growth + 2 * Tier`
- `Mana = 10 + 2 * Intelligence`
- `Evasion = 10 + Evasion + DefenseBonus1 + ArmorBonus`

Adicione em `05-rolls.md` um bloco `## Resumo do pipeline real` com estas linhas:

1. resolver valor da propriedade
2. definir numero de dados
3. rolar os dados
4. aplicar `Luck`
5. contar sucessos contra `Complexity`
6. comparar `NumberOfSuccesses` com `Difficulty`
```

- [ ] **Step 5: Verify the two files contain the formula strings and roll pipeline headings**

Run:

```powershell
rg --line-number "Life = 4 \* Vigor|Moral = 4 \* Intuition|Mana = 10 \+ 2 \* Intelligence|Evasion = 10 \+ Evasion|Resumo do pipeline real" "docs/rolerolls-system/04-resolucao-de-propriedades-e-formulas.md" "docs/rolerolls-system/05-rolls.md"
```

Expected: matches for all four formulas and the roll summary heading.

### Task 4: Document Combat

**Files:**
- Create: `docs/rolerolls-system/06-combate.md`
- Reference: `Creatures/Entities/CreatureAttack.cs:18-184`
- Reference: `Creatures/Entities/Creature.cs:648-670`
- Reference: `Itens/GripType.cs:20-225`
- Reference: `Itens/Configurations/ArmorDefinition.cs:6-53`
- Reference: `Attacks/Services/AttackService.cs:72-183`
- Reference: `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs:29-313`

- [ ] **Step 1: Create the combat file with the exact runtime summary of `Attack`**

```md
# Combate

## Objetivo da secao

Explicar como o metodo `Creature.Attack(...)` transforma valor de hit em sucessos, hits, block e dano aplicado ao alvo.

## Implementado hoje

- O servico de ataque monta `AttackCommand` e chama `Creature.Attack(...)`.
- O hit usa a `HitProperty` obtida de `ItemConfiguration.GetWeaponHitProperty(...)`.
- A defesa do alvo entra como `complexity`.
- Cada sucesso guarda o excesso `rollTotal - complexity` quando esse valor e maior ou igual a zero.
- Os sucessos sao agrupados por dificuldade da arma: `Light = 1`, `Medium = 2`, `Heavy = 3`.
- O dano por hit hoje usa `max(chunkDamage + damageBonusPerHit - block, 1)`.

## Assumido por testes/balance

- Os testes verificam falha sem dano, cenarios de dano fixo, matrizes numericas por nivel e relacoes de dominancia entre arma e armadura.

## Divergencias e observacoes

- Alguns testes antigos ainda esperam `Success = true` com `TotalDamage = 0`.
- O calculo de dano ainda esta em revisao conceitual.

## Fontes

- `Creatures/Entities/CreatureAttack.cs`
- `Attacks/Services/AttackService.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
```

- [ ] **Step 2: Add the concrete subheadings and formulas that future readers will need**

```md
Adicione estes subtitulos em `docs/rolerolls-system/06-combate.md`:

## Fluxo do servico
## Escolha da arma e do grip
## Calculo de hit
## Calculo de sucessos acima da defesa
## Agrupamento por dificuldade da arma
## Block e reducao
## Aplicacao de dano nas vitalidades
## Divergencias atuais do modelo de dano

No bloco `## Agrupamento por dificuldade da arma`, inclua estas linhas:

- `Light`: 1 sucesso por hit
- `Medium`: 2 sucessos por hit
- `Heavy`: 3 sucessos por hit
```

- [ ] **Step 3: Verify the code anchors for combat math before finalizing the prose**

Run:

```powershell
rg --line-number "GetWeaponHitProperty|difficulty = gripStats\.AttackDifficult|successes\.Count / difficulty|Math\.Max\(chunkDamage \+ damageBonusPerHit - block, 1\)|ResolveWeaponVsArmorLuck|TotalBlock" "Creatures/Entities/CreatureAttack.cs" "Attacks/Services/AttackService.cs" "Itens/Configurations/ArmorDefinition.cs"
```

Expected: matches for hit property selection, difficulty, hit grouping, minimum damage floor, weapon-vs-armor luck and total block.

- [ ] **Step 4: Add a concrete worked example that matches current runtime semantics**

```md
Adicione um bloco `## Exemplo minimo` com este exemplo:

- defesa do alvo: `15`
- resultados finais dos dados: `15` e `15`
- excesso por dado: `0` e `0`
- com arma `Light`, os dois sucessos formam `2` hits
- com arma `Medium`, os dois sucessos formam `1` hit
- com arma `Heavy`, os dois sucessos nao formam hit suficiente

Feche o bloco com esta observacao:

`No runtime atual, empate com a defesa ainda conta como sucesso; o efeito final sobre dano depende do agrupamento por dificuldade da arma e do piso minimo de dano por hit.`
```

- [ ] **Step 5: Verify the combat file contains the worked example and grouping rules**

Run:

```powershell
rg --line-number "Exemplo minimo|Light: 1 sucesso por hit|Medium: 2 sucessos por hit|Heavy: 3 sucessos por hit|empate com a defesa" "docs/rolerolls-system/06-combate.md"
```

Expected: one match for the example heading, three grouping lines and the final observation.

### Task 5: Document Evade, Defenses, Vitalities And Conditions

**Files:**
- Create: `docs/rolerolls-system/07-evade-e-defesas.md`
- Create: `docs/rolerolls-system/08-vitalidades-e-condicoes.md`
- Reference: `Creatures/Entities/CreatureDefend.cs:16-125`
- Reference: `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs:8-104`
- Reference: `Creatures/Entities/Vitality.cs:8-116`
- Reference: `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs:47-257`
- Reference: `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs:29-387`
- Reference: `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs:109-174`

- [ ] **Step 1: Create the evade-and-defenses file with the production-vs-tests split**

```md
# Evade E Defesas

## Objetivo da secao

Documentar a defesa de `Evasion`, o metodo `Evade(...)` e o lugar que ele ocupa hoje no sistema.

## Implementado hoje

- O template base define `Evasion = 10 + Evasion + DefenseBonus1 + ArmorBonus`.
- O metodo `Evade(...)` existe no aggregate `Creature`.
- O fluxo principal do servico de ataque em producao chama `Attack(...)`, nao `Evade(...)`.

## Assumido por testes/balance

- `EvadeTests` trata `Evade(...)` como parte importante do contrato de combate.

## Divergencias e observacoes

- O metodo `Evade(...)` e exercitado diretamente nos testes, mas nao e o caminho principal exposto pelo servico atual.

## Fontes

- `Creatures/Entities/CreatureDefend.cs`
- `Attacks/Services/AttackService.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`
```

- [ ] **Step 2: Create the vitality-and-conditions file with the exact threshold semantics**

```md
# Vitalidades E Condicoes

## Objetivo da secao

Documentar formulas de vitalidade, ordem de consumo, cascata de dano e exposicao de condicoes por threshold.

## Implementado hoje

- `Life`, `Moral` e `Mana` usam formulas do template.
- Ataque basico percorre `BasicAttackOrder` em ordem crescente.
- Se uma vitalidade zera, o excesso segue para a proxima.
- `CurrentConditions` depende de `Value <= 0` e `Value <= 30 porcento do maximo`.

## Assumido por testes/balance

- Os testes mostram cenarios diretos de cascata de dano e thresholds de `Shaken` e `Bleeding`.

## Divergencias e observacoes

- O template base nao inclui `Mana` na ordem padrao, embora exista teste com uma configuracao que inclui.
- Condicao observavel e bonus de condicao efetivamente aplicado nao sao a mesma coisa hoje.

## Fontes

- `Creatures/Entities/Vitality.cs`
- `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs`
- `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
```

- [ ] **Step 3: Verify the exact source anchors for `Evasion`, `BasicAttackOrder` and thresholds**

Run:

```powershell
rg --line-number "Formula = \"10 \+ Evasion|BasicAttackOrder|ConditionAtThirtyPercent|ConditionAtZero|currentPercent <= 30m|Value <= 0|ApplyBasicAttackDamage|TakeDamage" "DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs" "Creatures/Entities/Vitality.cs" "Creatures/Entities/CreatureBasicAttackVitalityResolver.cs"
```

Expected: matches for the defense formula, vitality ordering, threshold properties and the damage cascade helpers.

- [ ] **Step 4: Expand both files with the exact default formulas and status mapping**

```md
Adicione em `docs/rolerolls-system/08-vitalidades-e-condicoes.md` um bloco `## Formulas base` com estas linhas:

- `Life = 4 * Vigor + 2 * Level + Growth`
- `Moral = 4 * Intuition + 2 * Level + Growth + 2 * Tier`
- `Mana = 10 + 2 * Intelligence`

Adicione um bloco `## Status observados no template base` com estas linhas:

- `Moral <= 30 porcento`: `Shaken`
- `Moral = 0`: `Bleeding`
- `Life <= 30 porcento`: `Debilitated`
```

- [ ] **Step 5: Verify the files contain the expected formula and threshold strings**

Run:

```powershell
rg --line-number "Moral <= 30 porcento|Moral = 0|Life <= 30 porcento|Evasion = 10 \+ Evasion|fluxo principal do servico de ataque em producao chama `Attack`" "docs/rolerolls-system/07-evade-e-defesas.md" "docs/rolerolls-system/08-vitalidades-e-condicoes.md"
```

Expected: matches for the three threshold lines, the defense formula and the note about production using `Attack`.

### Task 6: Document Test Contracts, Balance Models And Known Divergences

**Files:**
- Create: `docs/rolerolls-system/09-contratos-de-teste-e-balance.md`
- Create: `docs/rolerolls-system/10-divergencias-e-pendencias.md`
- Modify: `docs/rolerolls-system/README.md`
- Reference: `UnitTests/README.md:1-225`
- Reference: `UnitTests/Attacks/Services/AttackServiceTests/BalanceSummary.md:1-55`
- Reference: `UnitTests/Attacks/Services/AttackServiceTests/HitPointScalingFormulas.md:1-114`
- Reference: `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs:200-279,315-686`
- Reference: `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs:117-387`
- Reference: `UnitTests/Attacks/Services/AttackServiceTests/RpgBalanceDesignTests.cs`
- Reference: `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`

- [ ] **Step 1: Create the test-contracts file with the exact categories of evidence**

```md
# Contratos De Teste E Balance

## Objetivo da secao

Separar o que os testes travam com numeros exatos, o que travam por tendencia e o que e apenas observacao de balance.

## Implementado hoje

- O repositorio contem testes de roll, ataque, evade, balance abstrato e balance via `Creature.Attack(...)`.

## Assumido por testes/balance

- Existe um baseline abstrato de armas e armaduras.
- Existe uma leitura de HP esperado para aproximadamente quatro turnos.
- Parte das regras de sorte e vantagem nos docs de teste pertence ao modelo abstrato, nao ao runtime real.

## Divergencias e observacoes

- Nem todo numero de balance e uma regra viva do sistema de producao.

## Fontes

- `UnitTests/README.md`
- `UnitTests/Attacks/Services/AttackServiceTests/BalanceSummary.md`
- `UnitTests/Attacks/Services/AttackServiceTests/HitPointScalingFormulas.md`
```

- [ ] **Step 2: Create the divergences file with the already-confirmed mismatch list**

```md
# Divergencias E Pendencias

## Objetivo da secao

Centralizar conflitos entre runtime, testes e docs auxiliares para facilitar futuras revisoes de regra.

## Lista inicial de divergencias

- baseline de progressao dos testes diferente do estado inicial real
- `Luck` do runtime mais simples que `Luck` dos modelos abstratos
- `Attack` atual usa excessos acima da defesa agrupados por dificuldade da arma
- testes antigos ainda esperam ataques com dano zero em cenarios onde o runtime atual tem piso minimo de dano
- `Evade` aparece forte nos testes, mas o servico exposto usa `Attack`
- `Mana` nao faz parte da ordem padrao de dano basico do template base
- ha parametros expostos em `AttackCommand` que nao participam da resolucao principal

## Pendencias abertas

- revisar o modelo de dano
- decidir o papel real de `Evade` no fluxo de producao
- decidir se os testes antigos devem virar historia do sistema ou contrato vivo

## Fontes

- `UnitTests/README.md`
- `Creatures/Entities/CreatureAttack.cs`
- `Creatures/Entities/CreatureDefend.cs`
- `Attacks/Services/AttackService.cs`
```

- [ ] **Step 3: Verify the balance and divergence anchors before writing final prose**

Run:

```powershell
rg --line-number "Balance Summary|Hit Point Scaling Formulas|TotalDamage.Should\(\)\.Be\(0\)|Math\.Max\(chunkDamage \+ damageBonusPerHit - block, 1\)|Evade\(|HitPointsNeededForFourRounds|Luck \+1" "UnitTests/README.md" "UnitTests/Attacks/Services/AttackServiceTests/BalanceSummary.md" "UnitTests/Attacks/Services/AttackServiceTests/HitPointScalingFormulas.md" "UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs" "Creatures/Entities/CreatureAttack.cs" "Creatures/Entities/CreatureDefend.cs"
```

Expected: matches for the balance docs, the zero-damage test assertions, the minimum damage floor and the evade/runtime mismatch anchors.

- [ ] **Step 4: Update the main README to link the last two files and warn readers about known divergences**

```md
Adicione ao fim de `docs/rolerolls-system/README.md` este bloco:

## Leitura Critica

Se voce estiver investigando combate, leia `06-combate.md`, `07-evade-e-defesas.md` e `10-divergencias-e-pendencias.md` em conjunto. O sistema atual contem diferencas relevantes entre runtime, testes e docs auxiliares.
```

- [ ] **Step 5: Verify the new files and the README warning block**

Run:

```powershell
rg --line-number "^# |Lista inicial de divergencias|Pendencias abertas|Leitura Critica" "docs/rolerolls-system/09-contratos-de-teste-e-balance.md" "docs/rolerolls-system/10-divergencias-e-pendencias.md" "docs/rolerolls-system/README.md"
```

Expected: the two top-level titles, the divergence headings and the new `Leitura Critica` block in the README.

### Task 7: Run Final Consistency Pass Across The Whole Documentation Set

**Files:**
- Modify: `docs/rolerolls-system/README.md`
- Modify: `docs/rolerolls-system/01-visao-geral.md`
- Modify: `docs/rolerolls-system/02-modelo-da-criatura.md`
- Modify: `docs/rolerolls-system/03-progressao.md`
- Modify: `docs/rolerolls-system/04-resolucao-de-propriedades-e-formulas.md`
- Modify: `docs/rolerolls-system/05-rolls.md`
- Modify: `docs/rolerolls-system/06-combate.md`
- Modify: `docs/rolerolls-system/07-evade-e-defesas.md`
- Modify: `docs/rolerolls-system/08-vitalidades-e-condicoes.md`
- Modify: `docs/rolerolls-system/09-contratos-de-teste-e-balance.md`
- Modify: `docs/rolerolls-system/10-divergencias-e-pendencias.md`

- [ ] **Step 1: Verify that every section file exists before doing the consistency pass**

Run:

```powershell
rg --files "docs/rolerolls-system"
```

Expected: `README.md` plus `01` through `10` section files.

- [ ] **Step 2: Verify that section files `01` through `10` all contain the standard evidence structure**

Run:

```powershell
rg --line-number "^## Implementado hoje|^## Assumido por testes/balance|^## Divergencias e observacoes|^## Fontes" "docs/rolerolls-system"
```

Expected: four matches in each numbered file from `01` to `10`.

- [ ] **Step 3: Verify that no file claims unsupported behavior without a source block**

Run:

```powershell
rg --line-number "[T][O][D][O]|[T][B][D]|preencher|depois decidimos|talvez|provavelmente" "docs/rolerolls-system"
```

Expected: no matches.

- [ ] **Step 4: Read the final documentation in recommended order and tighten any duplicated or contradictory statements**

```md
Na passada final, aplique estas correcoes se aparecerem:

- sempre que uma frase falar de runtime, manter o verbo no presente: `o codigo faz`, `o metodo usa`, `o servico chama`
- sempre que uma frase falar de testes, usar `os testes assumem`, `os testes travam`, `o modelo abstrato usa`
- quando houver conflito, citar o arquivo em `## Fontes` e repetir a divergencia em `10-divergencias-e-pendencias.md`
```

- [ ] **Step 5: Verify the final document set with a heading scan**

Run:

```powershell
rg --line-number "^# |^## " "docs/rolerolls-system"
```

Expected: one top-level title per file, consistent section headings and no missing `## Fontes` block in files `01` to `10`.
