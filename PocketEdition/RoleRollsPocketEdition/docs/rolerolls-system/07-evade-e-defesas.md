# Evasion e Defesas

## Objetivo da seção

Documentar a resolução de `Evasion` rolada pelo defensor e as duas formas de
defesa usadas pelo motor: a Defesa estática do ataque básico e a ação defensiva
iniciada pelo jogador.

## Fluxos expostos

- `POST .../creatures/{creatureId}/basic-attacks`: o jogador atacante rola o
  ataque básico contra uma Defesa estática do alvo.
- `POST .../creatures/{defenderId}/evades`: o jogador defensor resolve a
  Evasion contra valores estáticos de um atacante.

`EvadeInput` aceita somente `AttackerId`, `WeaponSlot`, `VitalityId`, `Luck` e
`Advantage`. A especialidade ofensiva, os bônus, a dificuldade, os dados e o
dano são resolvidos no servidor.

## Resolução de Evasion

`EvadeService` carrega atacante, defensor e `ItemConfiguration` da campanha e
chama `Creature.Evade(...)`. O domínio calcula:

```text
dados-base = total da especialidade ofensiva do atacante

dificuldade = 10
  + especialidade ofensiva
  + hit do grip
  + buffs de hit
  + diferença de nível
  + bônus de nível da arma
```

O defensor rola um d20 para cada dado-base e soma:

```text
especialidade de Evasion configurada na campanha
+ bônus de Evasion da armadura
+ bônus de nível da armadura
+ buffs de Evasion
```

Resultados maiores que a dificuldade evitam tentativas. Empates pertencem ao
atacante, mas têm excesso zero. Resultados menores geram `dificuldade -
resultado`; os excessos são ordenados, agrupados pela dificuldade da arma e
convertidos em dano com o mesmo bloqueio, piso mínimo e resolvedor de
vitalidades do ataque básico.

Vantagem adiciona dados e conserva os melhores resultados até a quantidade
base. Sorte positiva rerrola os menores resultados; sorte negativa rerrola os
maiores. Resultado alto permanece favorável.

## Configuração de campanha

`ItemConfiguration.EvadeProperty` define a propriedade defensiva usada pela
Evasion. O Land of Heroes a configura como a especialidade `Evasion`.

Essa propriedade é persistida em `ItemConfigurations` pelas colunas
`EvadeProperty_Id` e `EvadeProperty_Type`.

## Resultado e histórico de cena

`EvadeResponse` expõe atacante, defensor, arma, dados-base, dificuldade, bônus
de Evasion, dados rolados, resultados conservados, excessos, hits, bloqueio,
dano e as vitalidades desgastadas.

`ScenesService.ProcessEvadeAction(...)` registra uma ação de cena cujo ator é o
defensor. A descrição identifica defensor, atacante, arma, hits e dano, sem
registrar uma rolagem escondida do atacante.

## Arquivos principais

- `Attacks/Models/EvadeInput.cs`
- `Attacks/Models/EvadeResponse.cs`
- `Attacks/Services/EvadeService.cs`
- `Scenes/Controllers/SceneCreaturesController.cs`
- `Creatures/Entities/CreatureDefend.cs`
- `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs`
- `Itens/Configurations/ItemConfiguration.cs`
- `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`
