# Vitalidades E Condicoes

## Objetivo da secao

Documentar formulas de vitalidade, ordem de consumo, cascata de dano e exposicao de condicoes por threshold.

## Implementado hoje

- `Life`, `Moral` e `Mana` usam formulas do template.
- `Vitality.MaxValue` e recalculado a partir da formula da vitalidade e do estado atual da criatura.
- Ataque basico percorre `BasicAttackOrder` em ordem crescente.
- Se uma vitalidade zera, o excesso de dano segue para a proxima vitalidade da ordem.
- `CurrentConditions` depende de `Value <= 0` e `currentPercent <= 30 porcento`.
- `CurrentStatus` e a primeira condicao normalizada da lista atual.

## Assumido por testes/balance

- Os testes mostram cenarios diretos de cascata de dano entre `Moral`, `Life` e `Mana` quando a ordem e configurada para isso.
- Os testes travam que `Moral <= 30 porcento` expoe `Shaken`.
- Os testes travam que `Moral = 0` expoe `Bleeding` e `Shaken`, com `Bleeding` como `CurrentStatus`.

## Divergencias e observacoes

- O template base nao inclui `Mana` na ordem padrao de dano basico, embora exista teste com uma configuracao que inclui.
- Condicao observavel e bonus de condicao realmente aplicado nao sao a mesma coisa hoje.
- O sistema expoe as condicoes correntes da vitalidade, mas isso nao significa que todos os bonuses de condicao entram automaticamente no calculo de combate.

## Fontes

- `Creatures/Entities/Vitality.cs:8-116`
- `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs:8-104`
- `Creatures/Entities/Creature.cs:176-208`
- `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs:47-173`
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs:124-198`

## Formulas base

- `Life = 4 * Vigor + 2 * Level + Growth`
- `Moral = 4 * Intuition + 2 * Level + Growth + 2 * Tier`
- `Mana = 10 + 2 * Intelligence`

## Ordem padrao de dano basico no template base

- `Moral`: `BasicAttackOrder = 1`
- `Life`: `BasicAttackOrder = 2`
- `Mana`: sem ordem padrao configurada

## Status observados no template base

- `Moral <= 30 porcento`: `Shaken`
- `Moral = 0`: `Bleeding`
- `Life <= 30 porcento`: `Debilitated`

## Cascata de dano

1. o ataque descobre a ordem basica das vitalidades
2. aplica dano na primeira vitalidade da ordem
3. se a vitalidade zerar, o excesso segue adiante
4. o processo continua ate acabar o dano ou acabar a ordem configurada
