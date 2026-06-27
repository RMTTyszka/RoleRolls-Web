# RoleRolls Business Rules Documentation Design

## Goal

Documentar toda a regra de negocio do sistema `RoleRolls` antes de entrar na documentacao dos `DefaultUniverses`.

O foco desta etapa e produzir uma documentacao confiavel do sistema base: atributos, skills, rolls, combate, evade, vitalidades, formulas, progressao, contratos de teste e divergencias conhecidas.

## Scope

Esta documentacao cobre:

- o comportamento implementado hoje no runtime
- o comportamento assumido hoje pelos testes e documentos de balance
- as divergencias entre runtime, testes e docs
- as formulas e mapeamentos centrais usados pelo sistema base
- o papel do `LandOfHeroes` apenas quando ele define estrutura core usada pelo `RoleRolls`

Esta documentacao nao cobre ainda:

- poderes, magias, classes, racas e conteudo de universo como produto principal
- reconciliacao ou rework das regras divergentes
- redesign do sistema de dano

## Main Decision

Quando houver divergencia entre codigo real e testes/docs, a documentacao sera organizada em duas camadas:

1. `Implementado hoje`
2. `Assumido por testes/balance`

Toda secao relevante tambem tera um bloco `Divergencias e observacoes` para registrar conflitos, simplificacoes e pontos pendentes de revisao.

## Why This Approach

O repositorio hoje mistura tres tipos de verdade:

- a regra viva do runtime
- a regra usada por testes de contrato ou balance
- a regra descrita em documentos auxiliares

Essas tres camadas nao sao identicas. Em alguns pontos elas ate se contradizem. A documentacao precisa expor isso claramente, sem tentar esconder o estado atual do sistema.

## Documentation Architecture

O conjunto final da documentacao do `RoleRolls` sera modular, com um indice e secoes separadas por tema.

Arquivos planejados:

- `docs/rolerolls-system/README.md`
- `docs/rolerolls-system/01-visao-geral.md`
- `docs/rolerolls-system/02-modelo-da-criatura.md`
- `docs/rolerolls-system/03-progressao.md`
- `docs/rolerolls-system/04-resolucao-de-propriedades-e-formulas.md`
- `docs/rolerolls-system/05-rolls.md`
- `docs/rolerolls-system/06-combate.md`
- `docs/rolerolls-system/07-evade-e-defesas.md`
- `docs/rolerolls-system/08-vitalidades-e-condicoes.md`
- `docs/rolerolls-system/09-contratos-de-teste-e-balance.md`
- `docs/rolerolls-system/10-divergencias-e-pendencias.md`

## Section Template

Cada secao da documentacao final seguira o mesmo formato:

### 1. Objetivo da secao

Resumo curto do que aquela parte do sistema explica.

### 2. Implementado hoje

Descricao objetiva do comportamento do runtime atual, sempre derivada do codigo e com referencias de arquivo.

### 3. Assumido por testes/balance

Descricao objetiva do que os testes unitarios e docs de balance tratam como contrato ou modelo esperado.

### 4. Divergencias e observacoes

Registro dos conflitos entre runtime e testes/docs, regras stale, modelos simplificados e pontos que ainda exigem discussao futura.

### 5. Fontes

Lista dos arquivos principais usados como evidencia naquela secao.

## Planned Content Per Section

### `README.md`

- mapa da documentacao
- como ler as camadas de verdade
- limites de escopo desta fase
- ponte para a futura documentacao dos `DefaultUniverses`

### `01-visao-geral.md`

- o que e `RoleRolls`
- o que pertence ao sistema base e o que pertence aos universos
- fluxo macro: criatura -> propriedade -> roll -> combate -> vitalidade -> condicao
- glossario minimo: atributo, skill, minor skill, complexity, difficulty, hit, evade, block, vitality

### `02-modelo-da-criatura.md`

- atributos
- skills gerais
- specific skills
- vitalidades
- defesas
- equipamentos
- bonuses e conditions
- como esses elementos se conectam no aggregate `Creature`

### `03-progressao.md`

- estado inicial real de uma criatura no runtime
- limites de pontos hoje no codigo
- o que `LevelUp()` realmente faz
- progressao artificial usada pelos testes de balance
- diferenca entre progressao modelada e progressao exposta de fato pelo sistema

### `04-resolucao-de-propriedades-e-formulas.md`

- como `Property` vira numero no runtime
- diferenca entre atributo, skill, minor skill, defesa, vitalidade e condicao
- formulas com tokens
- `Tier`, `Growth`, `ArmorBonus`, `DefenseBonus1`, `DefenseBonus2`
- formulas base de `Life`, `Moral`, `Mana` e `Evasion`

### `05-rolls.md`

- pipeline do `Roll`
- quantidade de dados
- bonus
- complexity
- difficulty
- sucesso por dado
- `NumberOfSuccesses`
- `NumberOfRollSuccesses`
- criticos
- sorte e azar
- vantagem e desvantagem
- diferenca entre o roll real e os modelos probabilisticos dos testes

### `06-combate.md`

- fluxo de `Attack`
- escolha de arma e grip
- hit property usada de fato
- defesa do alvo
- calculo de sucessos acima da defesa
- agrupamento por dificuldade da arma
- block
- bonus de dano por tier
- aplicacao de dano nas vitalidades
- observacoes sobre o modelo de dano atual e o fato de ele ainda estar em revisao

### `07-evade-e-defesas.md`

- defesa de evasao no template base
- metodo `Evade` existente no codigo
- diferenca entre `Attack` e `Evade`
- fato de `Evade` ser testado diretamente, mas nao ser o fluxo principal exposto no servico de ataque atual
- limites do que esse metodo representa hoje como regra viva

### `08-vitalidades-e-condicoes.md`

- formulas e papeis de `Life`, `Moral` e `Mana`
- ordem de consumo em ataque basico
- cascata de dano entre vitalidades
- thresholds de condicao
- `CurrentConditions` e `CurrentStatus`
- diferenca entre condicao observavel e bonus realmente aplicado no runtime

### `09-contratos-de-teste-e-balance.md`

- o que os testes travam com numeros exatos
- o que os testes travam por tendencia
- o que e apenas teste de observacao/log
- baseline de armas e armaduras nos modelos abstratos
- formulas auxiliares de HP esperado usadas nos docs de teste

### `10-divergencias-e-pendencias.md`

- divergencias entre runtime e testes
- divergencias entre runtime e docs de suporte
- parametros expostos mas nao usados no fluxo real
- pontos que exigem revisao futura, comecando por dano e papel real do `Evade`

## Evidence Policy

A documentacao final deve seguir estas regras:

- nenhuma regra sera descrita sem evidencias concretas em codigo, teste ou doc existente
- sempre que possivel, a fonte sera apontada com caminho de arquivo e linhas
- regras inferidas sem confirmacao explicita nao entram como fato; entram no maximo como observacao
- quando houver comportamento exposto por teste mas nao por fluxo de producao, isso sera marcado
- quando houver comportamento implementado mas nao validado por teste, isso tambem sera marcado

## Primary Evidence Sources Already Identified

Fontes principais para o sistema base:

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
- `Rolls/Entities/Roll.cs`
- `Attacks/Services/AttackService.cs`
- `Itens/Configurations/ArmorDefinition.cs`
- `Itens/Configurations/ItemConfiguration.cs`
- `Itens/GripType.cs`
- `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs`

## Known Critical Divergences To Be Captured

O documento final precisa deixar explicito pelo menos estes conflitos ja encontrados:

- testes-base usam uma progressao artificial diferente do estado inicial real da criatura
- o `Roll` real implementa `luck` de forma mais simples do que os modelos abstratos dos testes/docs
- o metodo `Attack` hoje calcula dano a partir dos excessos acima da defesa, agrupados pela dificuldade da arma
- alguns testes antigos ainda esperam cenarios de `Success = true` com `TotalDamage = 0`, o que conflita com o piso minimo de dano por hit no `Attack` atual
- `Evade` e exercitado nos testes, mas o servico de ataque em producao passa pelo fluxo de `Attack`
- a ordem padrao de dano basico no template nao inclui `Mana`, embora exista teste cobrindo uma configuracao que inclui
- ha parametros do comando de ataque expostos no modelo, mas nao realmente usados na resolucao principal

## Quality Bar For Completion

A primeira versao da documentacao do `RoleRolls` sera considerada pronta quando:

- todos os topicos centrais do sistema base estiverem documentados
- cada topico trouxer claramente o que e runtime real e o que e premissa de teste/balance
- as formulas e mapeamentos principais estiverem descritos com fonte
- as divergencias mais importantes estiverem centralizadas em uma secao propria
- a documentacao permitir responder, sem reler o codigo inteiro, como atributos, skills, rolls, ataque, evade e vitalidades funcionam hoje

## Deferred Topics

Ficam explicitamente para a etapa posterior:

- documentacao das regras de negocio dos `DefaultUniverses`
- conteudo especifico de archetypes, powers, spells, magics e glossarios de universo
- decisoes de redesign das mecanicas divergentes

## Next Step After Spec Approval

Depois da aprovacao desta spec, o proximo passo e escrever um plano de execucao da documentacao em tarefas pequenas, cobrindo a criacao dos arquivos finais, a ordem de preenchimento e a verificacao cruzada com codigo e testes.
