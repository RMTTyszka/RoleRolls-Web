# RoleRolls - Guia Operacional de Refatoração

## Propósito

Esta doc existe para ser usada por outra IA ou por um dev humano durante refatorações do motor base do `RoleRolls`.

Ela não repete a regra de negócio inteira. Ela responde uma pergunta mais prática:

`se eu mudar este tópico do sistema, onde eu mexo, o que pode quebrar e quais testes eu preciso vigiar?`

## Leitura Recomendada Antes de Refatorar

1. `docs/rolerolls-regras-de-negocio.md`
2. `docs/rolerolls-mapa-tecnico.md`
3. esta doc

## Regra de Uso

- trate `DefaultUniverses/**` como configuração de universo, não como definição do motor
- trate `Templates/`, `Creatures/`, `Rolls/`, `Attacks/`, `Itens/`, `Bonuses/`, `Campaigns/` e `Scenes/` como motor base
- antes de mudar semântica, confirme se o que existe hoje é regra do motor ou apenas configuração de universo

## 1. Estrutura Geral do Motor

### Arquivos principais

- `Templates/Entities/CampaignTemplate.cs`
- `Templates/Dtos/CampaignTemplateModel.cs`
- `Templates/Controllers/TemplateController.cs`
- `Campaigns/ApplicationServices/CampaignsService.cs`

### Arquivos auxiliares

- `Campaigns/Controllers/CampaignAttributesController.cs`
- `Campaigns/Controllers/CampaignAttributelessSkillsController.cs`
- `Campaigns/Controllers/CampaignDefencesController.cs`
- `Campaigns/Controllers/CampaignLifesController.cs`
- `Campaigns/Controllers/CampaignCreatureConditionsController.cs`

### Pontos de acoplamento

- o template concentra atributos, skills, minor skills, vitalidades, condições, defesas e item configuration
- a mesma estrutura aparece em entidade, DTO, service e controller
- mudanças de forma do template quase sempre exigem tocar todos esses níveis

### Riscos ao refatorar

- quebrar serialização do template
- quebrar criação de campanha nova
- criar divergência entre DTO e entidade
- deixar configuração possível no banco, mas impossível na API, ou o contrário

### Testes que precisam ser observados

- `UnitTests/README.md`
- indiretamente, qualquer teste que usa `BaseCreature` ou `LandOfHeroesTemplate.Template`

### Nota operacional

Se a mudança for estrutural, comece em `CampaignTemplate.cs` e percorra o espelho completo até controller e DTO.

## 2. Ficha da Criatura

### Arquivos principais

- `Creatures/Entities/Creature.cs`
- `Creatures/Entities/Attribute.cs`
- `Creatures/Entities/Skill.cs`
- `Creatures/Entities/SpecificSkill.cs`
- `Creatures/Entities/Defense.cs`
- `Creatures/Entities/Vitality.cs`

### Arquivos auxiliares

- `Templates/Entities/AttributeTemplate.cs`
- `Templates/Entities/SkillTemplate.cs`
- `Templates/Entities/SpecificSkillTemplate.cs`
- `Templates/Entities/DefenseTemplate.cs`
- `Templates/Entities/VitalityTemplate.cs`

### Pontos de acoplamento

- a ficha concreta nasce diretamente do template
- atributos, skills e specializações participam tanto de testes quanto de fórmulas
- vitalidades e defesas dependem do template e também do estado atual da criatura

### Riscos ao refatorar

- quebrar a criação de criatura a partir de template
- quebrar associação entre especialização e atributo
- quebrar cálculo de vitalidade máxima ao mexer na composição da criatura

### Testes que precisam ser observados

- `UnitTests/Core/BaseCreature.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`

### Nota operacional

Se a mudança mexer em `como a ficha nasce`, revise também `Creature.FromTemplate(...)` e a carga feita por `CreatureRepository`.

## 3. Atributos, Perícias e Especializações

### Arquivos principais

- `Templates/Entities/AttributeTemplate.cs`
- `Templates/Entities/SkillTemplate.cs`
- `Templates/Entities/SpecificSkillTemplate.cs`
- `Creatures/Entities/Attribute.cs`
- `Creatures/Entities/Skill.cs`
- `Creatures/Entities/SpecificSkill.cs`

### Arquivos auxiliares

- `Templates/Dtos/AttributeTemplateModel.cs`
- `Templates/Dtos/SkillTemplateModel.cs`
- `Templates/Dtos/SpecificSkillTemplateModel.cs`
- `Campaigns/ApplicationServices/CampaignsService.cs`

### Pontos de acoplamento

- `SkillTemplate` conhece suas specializações
- `SpecificSkillTemplate` pode herdar o atributo da skill ou definir um próprio
- `Skill.PointsLimit` e distribuição de pontos afetam a progressão real e os limites da ficha

### Riscos ao refatorar

- quebrar herança de atributo das especializações
- mudar limite de pontos sem revisar progressão
- alterar a estrutura da ficha e impactar diretamente combate, roll e fórmulas

### Testes que precisam ser observados

- `UnitTests/Rolls/SkillAndAttributeRollTests.cs`
- `UnitTests/Core/BaseCreature.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`

### Nota operacional

Se a refatoração mexer em skill e specialização, revise também `CreaturePropertyResolver.cs`.

## 4. Propriedades e Resolução de Valores

### Arquivos principais

- `Core/Entities/Property.cs`
- `Creatures/Entities/CreaturePropertyResolver.cs`
- `Creatures/Entities/Creature.cs`

### Arquivos auxiliares

- `Templates/Entities/FormulaToken.cs`
- `Bonuses/IHaveBonuses.cs`

### Pontos de acoplamento

- `PropertyType` define o que o motor consegue resolver genericamente
- a resolução de propriedade é usada por roll, fórmulas, defesa, block e combate
- bônus podem alterar valor-base e bônus acumulado

### Riscos ao refatorar

- quebrar roll sem perceber
- quebrar fórmulas de defesa e vitalidade ao mesmo tempo
- alterar semântica de skill/minor skill e provocar regressão em combate

### Testes que precisam ser observados

- `UnitTests/Rolls/SkillAndAttributeRollTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`

### Nota operacional

Se a dúvida for `de onde este número veio`, quase sempre a resposta passa por `CreaturePropertyResolver.cs`.

## 5. Testes, Successos, Complexity e Difficulty

### Arquivos principais

- `Rolls/RollInput.cs`
- `Rolls/Commands/RollDiceCommand.cs`
- `Rolls/Entities/Roll.cs`
- `Rolls/RollService.cs`

### Arquivos auxiliares

- `Campaigns/Controllers/CampaignScenesController.cs`
- `Rolls/Services/IRollSimulationService.cs`
- `Rolls/Services/RollSimulationService.cs`

### Pontos de acoplamento

- `RollInput` entra pela API
- `RollDiceCommand` é o contrato interno do engine
- `Roll.cs` concentra a semântica real de quantidade de dados, sorte, vantagem, complexity, difficulty e contagem de sucessos

### Riscos ao refatorar

- mudar sucesso de teste e quebrar combate inteiro
- mudar sorte e quebrar calibragem de balanceamento
- mudar advantage e criar efeitos colaterais em qualquer teste do sistema

### Testes que precisam ser observados

- `UnitTests/Rolls/SkillAndAttributeRollTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/SkillAndAttributeDcTests.cs`
- `UnitTests/README.md`

### Nota operacional

Refatoração segura em rolagem exige começar por `Roll.cs` e só depois revisar chamadas externas.

## 6. Fórmulas

### Arquivos principais

- `Templates/Entities/FormulaToken.cs`
- `Creatures/Entities/Creature.cs`
- `Creatures/Entities/CreaturePropertyResolver.cs`
- `Templates/Entities/DefenseTemplate.cs`
- `Templates/Entities/VitalityTemplate.cs`

### Arquivos auxiliares

- `Itens/ItemInstance.cs`
- `Creatures/Entities/Defense.cs`
- `Creatures/Entities/Vitality.cs`

### Pontos de acoplamento

- fórmulas dependem de propriedades, equipamento e valores derivados da criatura
- o mesmo pipeline serve tanto para defesa quanto para vitalidade
- qualquer mudança em token ou evaluator pode impactar muitos recursos ao mesmo tempo

### Riscos ao refatorar

- quebrar fórmulas de defesa e vitalidade com um único ajuste
- perder compatibilidade com fórmulas já persistidas
- mudar a semântica de `Tier`, `Growth`, `ArmorBonus` e valores derivados sem querer

### Testes que precisam ser observados

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`
- `UnitTests/README.md`

### Nota operacional

Se a refatoração tocar expressão, parser ou token, trate isso como hotspot de alto risco.

## 7. Progressão

### Arquivos principais

- `Creatures/Entities/Creature.cs`
- `Creatures/Entities/CreaturePropertyResolver.cs`
- `Templates/Entities/CampaignTemplate.cs`
- `Templates/Dtos/CampaignTemplateModel.cs`

### Arquivos auxiliares

- `UnitTests/Core/BaseCreature.cs`

### Pontos de acoplamento

- o motor possui nível, limites por nível e pontos por ficha
- os testes de balanceamento usam uma curva própria, diferente da progressão viva do runtime

### Riscos ao refatorar

- confundir progressão real com progressão de teste
- ajustar limites de pontos e quebrar a curva de combate
- alterar nível sem revisar fórmulas que dependem dele

### Testes que precisam ser observados

- `UnitTests/Rolls/SkillAndAttributeRollTests.cs`
- `UnitTests/Core/BaseCreature.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`

### Nota operacional

Sempre diferencie `regra viva do motor` de `curva usada para balanceamento`.

## 8. Combate

### Arquivos principais

- `Scenes/Controllers/SceneCreaturesController.cs`
- `Attacks/Services/AttackService.cs`
- `Creatures/Entities/CreatureAttack.cs`
- `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs`

### Arquivos auxiliares

- `Creatures/Entities/Creature.cs`
- `Creatures/Services/CreatureActionsService.cs`
- `Campaigns/Repositories/CreatureRepository.cs`

### Pontos de acoplamento

- o service monta o comando de ataque
- a criatura carrega o cálculo real do ataque
- o dano segue para vitalidades pela ordem configurada
- o aggregate completo da criatura precisa estar carregado

### Riscos ao refatorar

- alterar semântica de acerto, dano ou ordem de vitalidade com regressão silenciosa
- esquecer carregamento de dados necessários no repositório
- quebrar o fluxo HTTP sem quebrar o engine, ou o contrário

### Testes que precisam ser observados

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`
- `UnitTests/README.md`

### Nota operacional

Se o problema for `combate está errado`, leia primeiro `AttackService.cs` e `CreatureAttack.cs` juntos.

## 9. Categorias de Arma e Grip

### Arquivos principais

- `Itens/WeaponCategory.cs`
- `Itens/Templates/WeaponTemplate.cs`
- `Creatures/Entities/Equipment.cs`
- `Itens/GripType.cs`
- `Itens/Configurations/ItemConfiguration.cs`

### Arquivos auxiliares

- `Itens/EquipableSlot.cs`
- `Itens/ItemInstance.cs`

### Pontos de acoplamento

- a categoria da arma influencia o grip
- o grip influencia dificuldade do hit, bônus base e identidade da arma
- item configuration decide de onde vêm hit e dano por categoria

### Riscos ao refatorar

- quebrar equivalência entre categoria, grip e combate
- alterar categorias sem revisar mapeamento de item configuration
- mexer em shield e afetar regra de arma sem perceber

### Testes que precisam ser observados

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/RpgBalanceDesignTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`

### Nota operacional

Arma, grip e item configuration formam um tripé. Raramente faz sentido mudar só um dos três.

## 10. Defesa e Block

### Arquivos principais

- `Templates/Entities/DefenseTemplate.cs`
- `Creatures/Entities/Defense.cs`
- `Creatures/Entities/Creature.cs`
- `Itens/Configurations/ArmorDefinition.cs`
- `Itens/Templates/ArmorTemplate.cs`
- `Itens/ArmorCategory.cs`
- `Itens/ItemInstance.cs`

### Arquivos auxiliares

- `Attacks/Services/AttackService.cs`
- `Creatures/Entities/CreatureAttack.cs`
- `Creatures/Entities/CreatureDefend.cs`

### Pontos de acoplamento

- defesa pode ser fórmula configurada
- block mistura tabela fixa de armadura com propriedade configurada
- ataque e evade usam essas duas camadas de forma diferente

### Riscos ao refatorar

- corrigir defesa e quebrar block
- alterar tabela de armadura e bagunçar o balanceamento inteiro
- esquecer que `Attack` e `Evade` podem interpretar defesa de formas diferentes

### Testes que precisam ser observados

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/RpgBalanceDesignTests.cs`

### Nota operacional

Qualquer refatoração em mitigação deve revisar `ArmorDefinition.cs`, `CreatureAttack.cs` e `CreatureDefend.cs` em conjunto.

## 11. Dano e Mitigação

### Arquivos principais

- `Creatures/Entities/CreatureAttack.cs`
- `Creatures/Entities/CreatureDefend.cs`
- `Itens/GripType.cs`
- `Itens/Configurations/ArmorDefinition.cs`

### Arquivos auxiliares

- `Attacks/Services/AttackService.cs`
- `Creatures/Entities/Creature.cs`

### Pontos de acoplamento

- o dano nasce do excesso acima da defesa no fluxo principal de ataque
- o block reduz esse dano
- o fluxo de `Evade` recalcula hits e dano por outra via

### Riscos ao refatorar

- tocar o piso de dano e invalidar contratos antigos
- alterar agrupamento de sucessos e mudar toda a identidade das armas
- corrigir `Attack` e deixar `Evade` sem alinhamento

### Testes que precisam ser observados

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/RpgBalanceDesignTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`

### Nota operacional

Este é um dos tópicos de maior risco do sistema atual.

## 12. Vitalidades Configuráveis

### Arquivos principais

- `Templates/Entities/VitalityTemplate.cs`
- `Templates/Dtos/VitalityTemplateModel.cs`
- `Campaigns/Controllers/CampaignLifesController.cs`
- `Campaigns/ApplicationServices/CampaignsService.cs`
- `Creatures/Entities/Vitality.cs`

### Arquivos auxiliares

- `Templates/Entities/CampaignTemplate.cs`
- `Creatures/Entities/Creature.cs`

### Pontos de acoplamento

- vitalidade é configurada no template e instanciada na criatura
- fórmula, ordem de desgaste e thresholds vivem no mesmo template de vitalidade

### Riscos ao refatorar

- mudar a estrutura da vitalidade e quebrar criação de ficha
- separar demais cálculo, threshold e ordem, perdendo coerência do modelo

### Testes que precisam ser observados

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`

### Nota operacional

Se a campanha precisar de um novo tipo de recurso, a trilha quase sempre é `VitalityTemplate` -> `Vitality.cs` -> APIs de campanha.

## 13. Ordem de Desgaste

### Arquivos principais

- `Templates/Entities/VitalityTemplate.cs`
- `Templates/Entities/CampaignTemplate.cs`
- `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs`
- `Attacks/Services/AttackService.cs`

### Arquivos auxiliares

- `Creatures/Entities/Creature.cs`

### Pontos de acoplamento

- a ordem nasce no template
- o resolver percorre essa ordem durante o ataque
- o comando de ataque pode priorizar uma vitalidade específica

### Riscos ao refatorar

- inverter ordem sem perceber
- quebrar overflow de dano entre vitalidades
- mudar regra de prioridade e produzir inconsistência entre ataque básico e dano manual

### Testes que precisam ser observados

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/README.md`

### Nota operacional

O arquivo mais sensível aqui é `CreatureBasicAttackVitalityResolver.cs`.

## 14. Condições por Faixa de Vitalidade

### Arquivos principais

- `Templates/Entities/VitalityTemplate.cs`
- `Templates/Entities/CampaignTemplate.cs`
- `Creatures/Entities/Vitality.cs`

### Arquivos auxiliares

- `Templates/Entities/CreatureCondition.cs`

### Pontos de acoplamento

- thresholds vivem no template de vitalidade
- a vitalidade resolve a condição corrente olhando valor atual e máximo

### Riscos ao refatorar

- mudar thresholds e quebrar estados observáveis
- alterar normalização das condições e mudar `CurrentStatus`

### Testes que precisam ser observados

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`

### Nota operacional

Hoje o motor genérico embute a lógica de `30 porcento` e `0` como thresholds centrais.

## 15. Condições como Regra de Estado

### Arquivos principais

- `Templates/Entities/CreatureCondition.cs`
- `Templates/Dtos/CreatureConditionModel.cs`
- `Campaigns/Controllers/CampaignCreatureConditionsController.cs`
- `Campaigns/ApplicationServices/CampaignsService.cs`
- `Bonuses/Bonus.cs`

### Arquivos auxiliares

- `Bonuses/IHaveBonuses.cs`
- `Creatures/Entities/Vitality.cs`

### Pontos de acoplamento

- condição pode carregar bônus
- bônus entram em aplicações diferentes: property, hit, evasion e critical
- a condição pode existir como estado visível sem que toda a sua semântica esteja plenamente integrada ao combate

### Riscos ao refatorar

- assumir que condição visível já implica efeito completo no motor
- mexer em bônus e causar regressão em combate ou rolagem

### Testes que precisam ser observados

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/README.md`

### Nota operacional

Se a refatoração for sobre `efeito funcional das condições`, revise `CreatureCondition.cs`, `Bonus.cs` e `GetTotalBonus(...)` juntos.

## 16. Mapeamento entre Ficha e Combate

### Arquivos principais

- `Itens/Configurations/ItemConfiguration.cs`
- `Attacks/Services/AttackService.cs`
- `Creatures/Entities/CreaturePropertyResolver.cs`
- `Creatures/Entities/CreatureAttack.cs`

### Arquivos auxiliares

- `Core/Entities/Property.cs`
- `Templates/Entities/CampaignTemplate.cs`

### Pontos de acoplamento

- item configuration diz de onde vêm hit, dano, block e defesa
- attack command carrega esse mapa para o engine
- property resolver transforma o mapa em número jogável

### Riscos ao refatorar

- trocar a origem de hit ou dano sem revisar configuração da campanha
- corrigir combate no engine e esquecer o mapeamento que o alimenta

### Testes que precisam ser observados

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`

### Nota operacional

Se a pergunta for `de onde este ataque está puxando seus valores`, comece em `ItemConfiguration.cs`.

## 17. Fronteira entre Motor e Universo

### Arquivos principais do motor

- `Templates/Entities/CampaignTemplate.cs`
- `Templates/Dtos/CampaignTemplateModel.cs`
- `Templates/Controllers/TemplateController.cs`

### Arquivos de universo

- `DefaultUniverses/LandOfHeroes/**`
- `DefaultUniverses/TheFutureIsOutThere/**`

### Pontos de acoplamento

- universos default alimentam o banco com configurações prontas
- o template é a fronteira real entre configuração de universo e engine genérico

### Riscos ao refatorar

- tratar uma convenção de universo como se fosse regra do motor
- quebrar loaders ao mudar a forma do template

### Testes que precisam ser observados

- praticamente todos os testes atuais passam por `LandOfHeroesTemplate.Template`
- por isso, refatorações do motor devem ser avaliadas com cuidado extra para distinguir bug de engine de acoplamento indevido dos testes a um universo específico

### Nota operacional

Quando uma refatoração parecer pedir `mexer no universo`, pare e confira se o problema não está no motor ou nos testes acoplados ao universo default.

## 18. Checklist Rápido para Outra IA

Antes de refatorar qualquer tópico do motor base:

1. ler `docs/rolerolls-regras-de-negocio.md`
2. confirmar se a mudança é do motor ou do universo
3. localizar o tópico correspondente nesta doc
4. abrir os `arquivos principais`
5. revisar os `pontos de acoplamento`
6. listar os `riscos ao refatorar`
7. rodar ou vigiar os `testes que precisam ser observados`

Se a mudança for grande, a ordem mais segura de leitura continua sendo:

1. `Templates/Entities/CampaignTemplate.cs`
2. `Core/Entities/Property.cs`
3. `Templates/Entities/FormulaToken.cs`
4. `Creatures/Entities/Creature.cs`
5. `Creatures/Entities/CreaturePropertyResolver.cs`
6. `Rolls/Entities/Roll.cs`
7. `Attacks/Services/AttackService.cs`
8. `Creatures/Entities/CreatureAttack.cs`
9. `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs`
10. `Creatures/Entities/Vitality.cs`
11. `Itens/Configurations/ItemConfiguration.cs`
12. `Itens/GripType.cs`
13. `Bonuses/Bonus.cs`
14. `Bonuses/IHaveBonuses.cs`
