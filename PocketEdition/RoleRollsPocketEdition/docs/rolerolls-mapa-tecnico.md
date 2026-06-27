# RoleRolls - Mapa Técnico do Sistema Base

## Propósito

Esta doc é a companheira técnica de `docs/rolerolls-regras-de-negocio.md`.

Ela existe para responder:

- quais arquivos do motor base tratam cada assunto da regra de negócio
- por onde uma IA ou dev humano deve entrar ao refatorar cada tópico
- onde termina o motor genérico e onde começa um `DefaultUniverse`

## Como Usar Esta Doc

Leia esta doc junto com `docs/rolerolls-regras-de-negocio.md`.

- a doc de regra diz `o que o sistema quer fazer`
- esta doc diz `onde isso está implementado hoje`

## Limite de Escopo

Considere como motor base, primeiro, estes blocos:

- `Core/`
- `Templates/`
- `Creatures/`
- `Rolls/`
- `Attacks/`
- `Itens/`
- `Bonuses/`
- `Campaigns/`
- `Scenes/`

Considere como conteúdo de universo, e não como regra base:

- `DefaultUniverses/**`

Os arquivos em `DefaultUniverses/` são bons exemplos de configuração e seed, mas não devem ser tratados como definição do motor genérico.

## Entry Points Mais Importantes

Se outra IA for refatorar o motor, estes são os pontos de entrada mais importantes:

- `Templates/Controllers/TemplateController.cs`
  Entrada para ler e gravar um `CampaignTemplate` completo.
- `Campaigns/ApplicationServices/CampaignsService.cs`
  Mutações granulares da configuração de campanha: atributos, skills, minor skills, defesas, vitalidades e condições.
- `Rolls/RollService.cs`
  Entrada principal do fluxo de rolagem.
- `Attacks/Services/AttackService.cs`
  Entrada principal do fluxo de ataque.
- `Campaigns/Repositories/CreatureRepository.cs`
  Carrega o aggregate completo da criatura para roll, ataque, dano e cura.
- `Scenes/Controllers/SceneCreaturesController.cs`
  Endpoint HTTP principal para ataques em cena.
- `Campaigns/Controllers/CampaignScenesController.cs`
  Endpoints HTTP para rolls, dano manual e cura.

## 1. O Que É o RoleRolls

### Arquivos principais

- `Templates/Entities/CampaignTemplate.cs`
  Aggregate central da configuração do sistema por campanha.
- `Templates/Dtos/CampaignTemplateModel.cs`
  DTO espelho do aggregate de template.
- `Templates/Controllers/TemplateController.cs`
  CRUD macro de templates.
- `Campaigns/ApplicationServices/CampaignsService.cs`
  Edição granular do template já associado a uma campanha.

### Observação de refatoração

Se a mudança mexer em `o que uma campanha pode definir`, o ponto de partida quase sempre é `CampaignTemplate` e seus DTOs.

## 2. O Que É Fixo no Motor

### Arquivos principais

- `Core/Entities/Property.cs`
  Tipos genéricos de propriedade que o motor entende: atributo, skill, minor skill, defesa, vitalidade e condição.
- `Templates/Entities/FormulaToken.cs`
  Tipos genéricos de token para fórmulas: propriedade, criatura, equipamento e valor manual.
- `Rolls/Commands/RollDiceCommand.cs`
  Contrato interno mínimo da rolagem.
- `Rolls/Entities/Roll.cs`
  Regra base de sucessos, vantagem, sorte, complexity e difficulty.
- `Attacks/Services/AttackService.cs`
  Contratos de ataque expostos para o resto do sistema.
- `Itens/Configurations/ItemConfiguration.cs`
  Mapeamento genérico entre categorias de arma, propriedades de hit, propriedades de dano, defesa da armadura e block.
- `Bonuses/Bonus.cs`
  Modelo genérico de bônus.
- `Bonuses/IHaveBonuses.cs`
  Regras genéricas de soma e precedência de bônus.

### Observação de refatoração

Mudanças no núcleo do motor quase sempre atravessam mais de um desses arquivos ao mesmo tempo.

## 3. Estrutura Básica da Ficha

### Atributos

- `Templates/Entities/AttributeTemplate.cs`
  Define o atributo no template.
- `Creatures/Entities/Attribute.cs`
  Instancia o atributo na ficha da criatura.

### Perícias

- `Templates/Entities/SkillTemplate.cs`
  Define a perícia no template.
- `Creatures/Entities/Skill.cs`
  Instancia a perícia na criatura.

### Especializações

- `Templates/Entities/SpecificSkillTemplate.cs`
  Define a especialização no template, inclusive atributo próprio quando houver.
- `Creatures/Entities/SpecificSkill.cs`
  Instancia a especialização na criatura.

### Defesas

- `Templates/Entities/DefenseTemplate.cs`
  Define defesa por nome e fórmula no template.
- `Creatures/Entities/Defense.cs`
  Instancia a defesa na criatura.

### Vitalidades

- `Templates/Entities/VitalityTemplate.cs`
  Define nome, fórmula, ordem de desgaste e condições por threshold.
- `Creatures/Entities/Vitality.cs`
  Instancia a vitalidade, calcula máximo e resolve condições correntes.

### Condições

- `Templates/Entities/CreatureCondition.cs`
  Define nome, descrição e bônus de uma condição.

### Equipamento

- `Creatures/Entities/Equipment.cs`
  Slots equipados, grip atual e armadura atual.
- `Itens/ItemInstance.cs`
  Instância de item com nível, bônus e acesso a template de arma/armadura.

### Aggregate principal

- `Creatures/Entities/Creature.cs`
  Aggregate que junta todos os blocos acima.

## 4. O Que a Campanha Pode Configurar

### Arquivos principais de configuração

- `Templates/Entities/CampaignTemplate.cs`
  Permite adicionar, remover e atualizar atributos, skills, minor skills, vitalidades e condições.
- `Templates/Dtos/AttributeTemplateModel.cs`
- `Templates/Dtos/SkillTemplateModel.cs`
- `Templates/Dtos/SpecificSkillTemplateModel.cs`
- `Templates/Dtos/DefenseTemplateModel.cs`
- `Templates/Dtos/VitalityTemplateModel.cs`
- `Templates/Dtos/CreatureConditionModel.cs`
- `Itens/Configurations/ItemConfiguration.cs`
  Mapeamento configurável de combate.

### Controllers e services de configuração

- `Campaigns/Controllers/CampaignAttributesController.cs`
- `Campaigns/Controllers/CampaignAttributelessSkillsController.cs`
- `Campaigns/Controllers/CampaignDefencesController.cs`
- `Campaigns/Controllers/CampaignLifesController.cs`
- `Campaigns/Controllers/CampaignCreatureConditionsController.cs`
- `Campaigns/ApplicationServices/CampaignsService.cs`

### Observação de refatoração

Se a mudança for `a campanha deve poder configurar X`, quase sempre você precisa tocar:

- entidade de template
- DTO do template
- service de campanha
- controller correspondente

## 5. Testes

### Arquivos principais

- `Rolls/RollInput.cs`
  Contrato externo de entrada da rolagem.
- `Rolls/Commands/RollDiceCommand.cs`
  Contrato interno usado pelo resolvedor de roll.
- `Rolls/RollService.cs`
  Orquestra rolagem em cena e rolagem ligada à criatura.
- `Rolls/Entities/Roll.cs`
  Resolve a rolagem propriamente dita.
- `Creatures/Entities/Creature.cs`
  Ponto em que a criatura transforma uma propriedade em roll executável.
- `Campaigns/Controllers/CampaignScenesController.cs`
  Endpoints HTTP para roll público, roll por criatura e simulação.

### Arquivos de apoio para calibração

- `Rolls/Services/IRollSimulationService.cs`
- `Rolls/Services/RollSimulationService.cs`
- `Rolls/Services/CdSimulationResult.cs`

### Observação de refatoração

Se a mudança afetar `Complexity`, `Difficulty`, `Luck`, `Advantage` ou contagem de sucessos, o eixo central é `Roll.cs`.

## 6. Fórmulas

### Arquivos principais

- `Templates/Entities/FormulaToken.cs`
  Modelo genérico de token de fórmula.
- `Core/Entities/Property.cs`
  Tipos de propriedade que uma fórmula pode referenciar.
- `Creatures/Entities/Creature.cs`
  Métodos `ApplyFormula`, `GetFormulaDetails`, `ResolveCreatureValue`, `ResolveEquipmentValue` e `EvaluateExpression`.
- `Creatures/Entities/CreaturePropertyResolver.cs`
  Resolução de propriedade simples antes de entrar na fórmula.
- `Templates/Entities/DefenseTemplate.cs`
  Defesa como fórmula.
- `Templates/Entities/VitalityTemplate.cs`
  Vitalidade como fórmula.
- `Itens/ItemInstance.cs`
  `LevelBonus` de item, usado por fórmulas de equipamento.

### Observação de refatoração

Se a mudança for `como fórmulas leem propriedades, nível ou equipamento`, o hotspot real está em `Creature.cs` e `CreaturePropertyResolver.cs`.

## 7. Progressão

### Arquivos principais

- `Creatures/Entities/Creature.cs`
  `Level`, `LevelUp()`, `AddPointToAttribute()`, `AddPointToSkill()`, `AddPointToSpecificSkill()`.
- `Creatures/Entities/CreaturePropertyResolver.cs`
  `MaxAttributePoints` atual.
- `Templates/Entities/CampaignTemplate.cs`
  `MaxAttributePoints` e `TotalAttributePoints` do template.
- `Templates/Dtos/CampaignTemplateModel.cs`
  Exposição desses limites no DTO.

### Arquivos de calibração, não de regra base

- `UnitTests/Core/BaseCreature.cs`
  Curva de progressão usada pelos testes de balanceamento.

### Observação de refatoração

Hoje progressão real de runtime e progressão de balanceamento não são a mesma coisa. Se a refatoração tocar progressão, compare `Creature.cs` com `UnitTests/Core/BaseCreature.cs` antes de decidir o alvo.

## 8. Combate

### Entry points

- `Scenes/Controllers/SceneCreaturesController.cs`
  Endpoint HTTP para ataque em cena.
- `Attacks/Services/AttackService.cs`
  Orquestra o ataque.

### Motor de combate

- `Creatures/Entities/CreatureAttack.cs`
  Regra principal de ataque, hits, dano e block.
- `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs`
  Regra de aplicação do dano nas vitalidades.
- `Creatures/Entities/Creature.cs`
  `TakeDamage()` e `Heal()`.
- `Creatures/Services/CreatureActionsService.cs`
  Entrada de dano e cura manuais fora do fluxo de ataque.

### Observação de refatoração

Se a mudança afetar a semântica do combate, o primeiro arquivo a ler é `CreatureAttack.cs`.

## 9. Categorias de Arma

### Arquivos principais

- `Itens/WeaponCategory.cs`
  Enum das categorias de arma.
- `Itens/Templates/WeaponTemplate.cs`
  Template de arma com categoria e metadados.
- `Creatures/Entities/Equipment.cs`
  Traduz arma equipada em `GripType`.
- `Itens/GripType.cs`
  Estatísticas operacionais por grip: hit, dano base, dificuldade, bônus defensivo de escudo.
- `Itens/Configurations/ItemConfiguration.cs`
  Mapeia cada categoria para propriedade de hit e dano.

### Observação de refatoração

Se a mudança for sobre identidade de arma, leia `WeaponCategory.cs`, `Equipment.cs`, `GripType.cs` e `ItemConfiguration.cs` em conjunto.

## 10. Defesa e Block

### Arquivos principais

- `Templates/Entities/DefenseTemplate.cs`
  Define defesas da campanha.
- `Creatures/Entities/Defense.cs`
  Instancia essas defesas na criatura.
- `Creatures/Entities/Creature.cs`
  Resolve defesa via fórmula.
- `Itens/Configurations/ArmorDefinition.cs`
  Tabela genérica de dodge e block por categoria de armadura.
- `Itens/Templates/ArmorTemplate.cs`
  Categoria de armadura.
- `Itens/ArmorCategory.cs`
  Enum de categorias.
- `Itens/ItemInstance.cs`
  Exposição de bônus defensivos do item.

### Observação de refatoração

Mudanças em `defesa` e `block` podem estar divididas entre fórmula configurada e tabela fixa de armadura.

## 11. Dano e Mitigação

### Arquivos principais

- `Creatures/Entities/CreatureAttack.cs`
  Transformação de sucessos em hits e hits em dano.
- `Itens/GripType.cs`
  Dificuldade da arma e bônus base por grip.
- `Itens/Configurations/ArmorDefinition.cs`
  Block total por armadura e nível.
- `Attacks/Services/AttackService.cs`
  Contratos de ataque e resultado.

### Arquivos auxiliares relacionados

- `Creatures/Entities/CreatureDefend.cs`
  Fluxo paralelo de `Evade`, importante porque também recalcula dano.

### Observação de refatoração

Se a refatoração for sobre `piso de dano`, `dano por margem`, `block` ou `quanto um hit gera`, o núcleo está em `CreatureAttack.cs` e `CreatureDefend.cs`.

## 12. Vitalidades Configuráveis

### Arquivos principais

- `Templates/Entities/VitalityTemplate.cs`
  Define nome, fórmula e thresholds de condição da vitalidade.
- `Templates/Dtos/VitalityTemplateModel.cs`
  DTO para CRUD de vitalidade.
- `Campaigns/Controllers/CampaignLifesController.cs`
  Endpoint de configuração de vitalidades.
- `Campaigns/ApplicationServices/CampaignsService.cs`
  Adição, remoção e atualização de vitalidades no template da campanha.
- `Creatures/Entities/Vitality.cs`
  Instância viva da vitalidade na criatura.

### Observação de refatoração

Se a mudança for `a campanha deve poder modelar outro tipo de recurso`, comece por `VitalityTemplate` e siga até `Vitality.cs`.

## 13. Ordem de Desgaste

### Arquivos principais

- `Templates/Entities/VitalityTemplate.cs`
  `BasicAttackOrder` e thresholds por vitalidade.
- `Templates/Entities/CampaignTemplate.cs`
  `GetBasicAttackVitalityRules()` resolve a ordem configurada no template.
- `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs`
  Converte a ordem em aplicação real de dano.
- `Attacks/Services/AttackService.cs`
  `AttackCommand` aceita override de vitalidade principal.

### Observação de refatoração

Se a mudança for `como o dano caminha entre recursos`, o centro está em `CreatureBasicAttackVitalityResolver.cs`.

## 14. Condições por Faixa de Vitalidade

### Arquivos principais

- `Templates/Entities/VitalityTemplate.cs`
  Guarda `ConditionAtThirtyPercent` e `ConditionAtZero`.
- `Templates/Entities/CampaignTemplate.cs`
  Resolve regras básicas de ataque a partir dessas condições.
- `Creatures/Entities/Vitality.cs`
  Resolve condições correntes a partir do valor atual da vitalidade.

### Observação de refatoração

Hoje os thresholds genéricos embutidos no motor são `30 porcento` e `0`. Se isso mudar, `Vitality.cs` e `VitalityTemplate.cs` entram juntos.

## 15. Condições como Regra de Estado

### Arquivos principais

- `Templates/Entities/CreatureCondition.cs`
  Estrutura da condição.
- `Templates/Dtos/CreatureConditionModel.cs`
  DTO da condição.
- `Campaigns/Controllers/CampaignCreatureConditionsController.cs`
  CRUD de condições.
- `Campaigns/ApplicationServices/CampaignsService.cs`
  Mutações de condições na campanha.
- `Bonuses/Bonus.cs`
  Bônus carregados pela condição.

### Observação de refatoração

Se a mudança for sobre `o que uma condição pode carregar`, os arquivos centrais são `CreatureCondition.cs` e `Bonus.cs`.

## 16. Mapeamento entre Ficha e Combate

### Arquivos principais

- `Itens/Configurations/ItemConfiguration.cs`
  Define de onde cada categoria de arma puxa hit, dano, defesa e block.
- `Attacks/Services/AttackService.cs`
  `AttackInput` e `AttackCommand` carregam esses mapeamentos para o fluxo de ataque.
- `Creatures/Entities/CreaturePropertyResolver.cs`
  Resolve a propriedade escolhida em valor real.
- `Creatures/Entities/CreatureAttack.cs`
  Usa o mapeamento configurado para produzir o ataque.

### Observação de refatoração

Se a pergunta for `de onde o combate puxa seus números`, a resposta técnica passa por `ItemConfiguration.cs` antes de qualquer arquivo de universo.

## 17. O Que Não É Regra Base do RoleRolls

### Diretórios que não devem ser tratados como motor base

- `DefaultUniverses/LandOfHeroes/**`
- `DefaultUniverses/TheFutureIsOutThere/**`

### Como usar esses arquivos corretamente

- use-os como exemplo de configuração
- use-os para verificar seed e sincronização com banco
- não use-os como fonte primária para definir a regra genérica do motor

### Arquivos de fronteira importantes

- `DefaultUniverses/*Loader.cs`
  Sincronizam configurações default com o banco.
- `Templates/Entities/CampaignTemplate.cs`
  É o verdadeiro ponto de fronteira entre motor e universo.

## 18. Resumo Técnico Curto

Se outra IA for refatorar o motor base, a ordem mais segura de leitura é esta:

1. `docs/rolerolls-regras-de-negocio.md`
2. `Templates/Entities/CampaignTemplate.cs`
3. `Core/Entities/Property.cs`
4. `Templates/Entities/FormulaToken.cs`
5. `Creatures/Entities/Creature.cs`
6. `Creatures/Entities/CreaturePropertyResolver.cs`
7. `Rolls/Entities/Roll.cs`
8. `Attacks/Services/AttackService.cs`
9. `Creatures/Entities/CreatureAttack.cs`
10. `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs`
11. `Creatures/Entities/Vitality.cs`
12. `Itens/Configurations/ItemConfiguration.cs`
13. `Itens/GripType.cs`
14. `Bonuses/Bonus.cs`
15. `Bonuses/IHaveBonuses.cs`

Essa sequência permite entender primeiro a estrutura genérica do sistema, depois a resolução de propriedade e roll, e só então o combate e o desgaste.
