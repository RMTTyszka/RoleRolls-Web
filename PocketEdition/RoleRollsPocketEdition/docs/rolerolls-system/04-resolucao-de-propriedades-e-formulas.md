# Resolucao De Propriedades E Formulas

## Objetivo da secao

Explicar como o sistema transforma uma capacidade da ficha em valor jogavel e como as formulas montam defesa, vitalidade e escalonamento.

## Implementado hoje

- Quando a regra pede um atributo, o sistema usa os pontos desse atributo.
- Quando a regra pede uma especializacao, o sistema combina o atributo ligado a ela com os pontos da propria especializacao.
- Defesas e vitalidades nao sao valores fixos; elas nascem de formulas.
- Condicoes, por si so, nao viram numero jogavel direto.
- Depois do valor-base, o sistema ainda pode aplicar bonus adicionais.

## Assumido por testes/balance

- Os testes costumam simplificar essa leitura transformando atributo e pericia em pools abstratas de dados.
- Essa simplificacao ajuda a estudar chance e balance, mas nao substitui a regra completa da ficha.

## Divergencias e observacoes

- A leitura abstrata dos testes e mais simples do que a leitura completa da ficha.
- A pericia geral, hoje, pesa menos do que a intuicao natural de `atributo + pontos da pericia` faria imaginar.

## Fontes

- `Creatures/Entities/CreaturePropertyResolver.cs:13-178`
- `Creatures/Entities/Creature.cs:316-577`
- `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs:47-257`
- `UnitTests/Rolls/SkillAndAttributeRollTests.cs:18-310`

## Formulas base do template

- `Life = 4 * Vigor + 2 * Level + Growth`
- `Moral = 4 * Intuition + 2 * Level + Growth + 2 * Tier`
- `Mana = 10 + 2 * Intelligence`
- `Evasion = 10 + Evasion + DefenseBonus1 + ArmorBonus`

## Escalonadores recorrentes

- `Tier = 1 + floor((Level - 1) / 2)`
- `Growth = floor(Tier * Tier / 2)`
- `ArmorBonus` acompanha o bonus de nivel do equipamento no peito
- `DefenseBonus1` e `DefenseBonus2` acompanham o perfil defensivo da armadura
