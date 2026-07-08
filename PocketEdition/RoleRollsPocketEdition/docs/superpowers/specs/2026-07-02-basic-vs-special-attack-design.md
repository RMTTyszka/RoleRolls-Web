# Basic Vs Special Attack Design

## Context

RoleRolls hoje tem um unico fluxo de ataque no codigo e no endpoint.

Isso mistura duas regras de negocio diferentes:

- `basic attack`
- `special attack`

O problema nao e so nomenclatura. O fluxo atual deixa implicito que todo ataque pertence ao mesmo modelo, quando a regra desejada e:

- `basic attack` depende de arma e de `ItemConfiguration`
- `special attack` nao depende de arma
- `special attack` nao depende de `ItemConfiguration`
- `special attack` e sempre `MinorSkill x Defense`
- `special attack` nunca calcula dano, vitalidade ou efeito automatico
- `special attack` so resolve teste e devolve resultado para o mestre decidir consequencia

O objetivo desta mudanca e tornar essa diferenca estrutural e incontestavel tanto na documentacao quanto no codigo.

## Decision Summary

Adotar separacao forte entre `basic attack` e `special attack` em quatro niveis:

1. documentacao
2. contratos HTTP
3. modelos de entrada e saida
4. fluxo de dominio e services

Nao usar um unico `AttackInput` com `Kind`.

Nao usar inferencia implicita por combinacao de campos.

Nao tratar `special attack` como variacao de `basic attack`.

## Business Rules

### Basic Attack

`basic attack` e ataque do combate armado.

Regras:

- usa arma equipada
- usa `WeaponSlot`
- usa `ItemConfiguration` da campanha
- propriedade ofensiva e determinada pela configuracao da categoria da arma
- propriedade de dano e determinada pela configuracao da categoria da arma
- pode usar override de defesa alvo, quando isso ja fizer parte do fluxo atual
- pode aplicar dano automaticamente
- pode aplicar desgaste de vitalidade automaticamente
- pode gerar status automatico por thresholds de vitalidade, como hoje

### Special Attack

`special attack` e teste ofensivo sem arma.

Regras:

- nao usa arma
- nao usa `WeaponSlot`
- nao usa `ItemConfiguration`
- sempre usa `MinorSkill`
- sempre escolhe defesa alvo explicitamente
- nunca calcula dano
- nunca aplica vitalidade
- nunca aplica efeito
- nunca gera status automatico por desgaste
- so retorna resultado do teste para o mestre decidir efeito narrativo ou mecanico

### Resolution Rule For Special Attack

Como o modelo atual de defesa expõe um unico valor, o `special attack` sera resolvido assim:

- `complexity = defense value`
- `difficulty = 1`

Assim, `special attack` vira um teste direto de `MinorSkill x Defense`.

Se futuramente o jogo precisar de `special attack` com varios sucessos obrigatorios, isso deve entrar como extensao propria do fluxo de `special attack`, nunca como reaproveitamento do fluxo de dano do `basic attack`.

## Documentation Changes

Atualizar `docs/rolerolls-regras-de-negocio.md` para separar claramente:

- o que e `basic attack`
- o que e `special attack`
- quais dependencias cada um possui
- o que cada um resolve automaticamente
- o que cada um nunca resolve automaticamente

Atualizar `docs/rolerolls-mapa-tecnico.md` para separar claramente:

- entry points de `basic attack`
- entry points de `special attack`
- arquivos responsaveis por resolucao de dano do `basic attack`
- arquivos responsaveis por resolucao de teste do `special attack`
- diferenca entre configuracao por campanha e escolha explicita de `MinorSkill x Defense`

Estrutura esperada nas docs:

- secao `Basic Attack`
- secao `Special Attack`
- secao `Diferenca Estrutural`

## HTTP Contract

### New Endpoints

Criar endpoints distintos:

- `POST /campaigns/{campaignId}/scenes/{sceneId}/creatures/{creatureId}/basic-attacks`
- `POST /campaigns/{campaignId}/scenes/{sceneId}/creatures/{creatureId}/special-attacks`

### Legacy Endpoint

O endpoint atual:

- `POST /campaigns/{campaignId}/scenes/{sceneId}/creatures/{creatureId}/attacks`

deve ficar marcado como legado e, se for mantido neste ciclo, deve encaminhar apenas para o fluxo de `basic attack`.

`special attack` nunca deve entrar pelo endpoint generico legado.

Objetivo:

- evitar quebra imediata do cliente atual
- tornar o contrato novo explicito
- permitir migracao sem manter ambiguidade conceitual

## Request Models

### BasicAttackInput

`BasicAttackInput` deve carregar apenas campos de `basic attack`.

Campos esperados:

- `TargetId`
- `WeaponSlot`
- `DefenseId` opcional
- `VitalityId` opcional
- `Luck`
- `Advantage`
- `CombatManeuverIds`

Campos que nao devem existir em `BasicAttackInput`:

- `HitProperty`
- `HitAttribute`
- `DamageAttribute`
- `SpecialSkillId`

Motivo:

- ofensiva do `basic attack` vem da campanha e da categoria da arma
- nao deve existir campo publico que finja que o cliente escolhe manualmente a skill ofensiva do `basic attack`

### SpecialAttackInput

`SpecialAttackInput` deve carregar apenas campos de `special attack`.

Campos esperados:

- `TargetId`
- `SpecialSkillId`
- `DefenseId`
- `Luck`
- `Advantage`

Campos que nao devem existir em `SpecialAttackInput`:

- `WeaponSlot`
- `VitalityId`
- `Damage`
- `ItemConfiguration`
- qualquer campo de arma

## Result Models

Usar tipos diferentes, nao um resultado unico com campos nulos.

### Shared Roll Resolution Fields

Os dois resultados podem compartilhar base tecnica comum para metadados de teste:

- `AttackerId`
- `TargetId`
- `Success`
- `DefenseId`
- `Complexity`
- `Difficulty`
- `NumberOfSuccesses`
- `NumberOfRollSuccesses`
- `Bonus`
- `Luck`
- `Advantage`
- `RolledDices`

### BasicAttackResult

Campos proprios:

- `WeaponSlot`
- `WeaponName`
- `TotalDamage`
- `Block`
- `DamageBonus`

### SpecialAttackResult

Campos proprios:

- `SpecialSkillId`
- `SpecialSkillName`

Campos que `SpecialAttackResult` nao deve ter:

- `Weapon`
- `WeaponName` derivado de arma
- `Damage`
- `TotalDamage`
- `Vitality`
- `Block`

## Domain And Service Structure

Separar o fluxo de aplicacao e dominio.

### Application Layer

Separar services:

- `IBasicAttackService`
- `BasicAttackService`
- `ISpecialAttackService`
- `SpecialAttackService`

Responsabilidades:

- carregar atacante e alvo
- construir comando proprio
- chamar metodo correto da criatura
- enviar resultado para cena/historico
- devolver DTO correto ao controller

### Domain Layer

Separar comandos e metodos:

- `BasicAttackCommand`
- `SpecialAttackCommand`
- `Creature.BasicAttack(...)`
- `Creature.SpecialAttack(...)`

`Creature.BasicAttack(...)` continua com:

- arma
- hit por configuracao
- dano
- block
- vitalidade

`Creature.SpecialAttack(...)` faz apenas:

1. resolver `MinorSkill` do atacante
2. resolver `Defense` do alvo
3. montar `RollDiceCommand`
4. processar rolagem
5. devolver resultado do teste

`Creature.SpecialAttack(...)` nao pode:

- tocar em `ApplyBasicAttackDamage`
- tocar em `GetWeaponDamageProperty`
- tocar em `GetWeaponHitProperty`
- depender de `GripType`
- depender de `ArmorDefinition.TotalBlock`

## Scene History And Notifications

O fluxo de cena precisa refletir a diferenca.

### Basic Attack History

Mantem descricao orientada a dano, por exemplo:

- atacante
- alvo
- arma
- dano causado
- status gerados por vitalidade

### Special Attack History

Precisa de descricao propria, por exemplo:

- atacante
- `MinorSkill` usada
- defesa alvo enfrentada
- sucesso ou falha

Sem:

- arma
- dano
- vitalidade
- status automatico

Para isso, `ScenesService` nao deve mais assumir que todo ataque escreve texto no formato:

- `attacked ... with weapon ... and caused damage`

Separar handlers de historico:

- `ProcessBasicAttackAction(...)`
- `ProcessSpecialAttackAction(...)`

## Testing Strategy

### Existing Tests

Preservar testes atuais de `basic attack` e `evade`.

Eles continuam validando comportamento de:

- arma
- dano
- block
- vitalidade
- ordem de desgaste

### New Tests For Special Attack

Adicionar cobertura especifica para `special attack`:

1. sucesso quando `MinorSkill` supera `Defense`
2. falha quando `MinorSkill` nao supera `Defense`
3. nao depende de arma equipada
4. nao consulta `ItemConfiguration`
5. nao altera vitalidade do alvo
6. nao produz dano
7. retorna dados completos da resolucao do teste
8. gera historico de cena sem arma e sem dano

### Regression Tests For Separation

Adicionar testes que provem a fronteira conceitual:

1. `basic attack` nao aceita `SpecialSkillId`
2. `special attack` nao aceita `WeaponSlot`
3. endpoint legado `/attacks` so executa `basic attack`
4. endpoint `/special-attacks` nao executa fluxo de dano

## Migration Notes

Mudanca deve ser feita em fases curtas:

1. criar modelos e services separados
2. criar endpoint novo de `special attack`
3. adaptar endpoint novo de `basic attack`
4. manter endpoint legado `/attacks` como alias de `basic attack`
5. atualizar historico de cena
6. atualizar docs
7. adicionar testes de regressao

## Non Goals

Esta mudanca nao define:

- catalogo de efeitos de `special attack`
- automacao de dano para `special attack`
- automacao de condicoes para `special attack`
- integracao direta com `Powers`

Esses pontos podem vir depois, mas nao devem contaminar a separacao estrutural desta entrega.

## Why This Design

Esta separacao e escolhida porque modela verdade de negocio, nao coincidencia tecnica.

Se `basic attack` e `special attack` continuarem como variacoes do mesmo contrato, o codigo continuara permitindo mistura conceitual:

- campos de arma em ataque sem arma
- campos de dano em ataque sem dano
- configuracao de campanha num fluxo que deveria ser `MinorSkill x Defense`

O resultado seria doc dizendo uma coisa e codigo permitindo outra.

Esta proposta faz o contrario:

- doc separa
- endpoint separa
- input separa
- result separa
- service separa
- dominio separa

Assim, a diferenca deixa de depender de convencao e passa a ser garantida por estrutura.
