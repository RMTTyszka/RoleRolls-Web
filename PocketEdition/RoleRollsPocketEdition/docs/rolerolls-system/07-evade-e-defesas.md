# Evade E Defesas

## Objetivo da secao

Documentar a defesa de `Evasion`, o metodo `Evade(...)` e o lugar que ele ocupa hoje no sistema base.

## Implementado hoje

- O template base define `Evasion = 10 + Evasion + DefenseBonus1 + ArmorBonus`.
- `Creature.DefenseValue(...)` resolve defesas por formula.
- O metodo `Evade(...)` existe dentro do aggregate `Creature`.
- O fluxo principal do servico de ataque em producao chama `Attack(...)`, nao `Evade(...)`.
- Dentro de `Evade(...)`, o sistema monta uma rolagem defensiva propria e compara o numero de sucessos defensivos com os sucessos ofensivos inferidos.

## Assumido por testes/balance

- `EvadeTests` trata `Evade(...)` como parte importante do contrato de combate.
- Os testes cobrem dois cenarios diretos: falha total da defesa com dano positivo e sucesso da defesa com dano final zero.
- Os testes tambem travam uma matriz numerica extensa de dano medio ao passar por `Evade(...)` em niveis e armaduras diferentes.

## Divergencias e observacoes

- O metodo `Evade(...)` e exercitado diretamente nos testes, mas nao e o caminho principal exposto pelo servico atual.
- `Evade(...)` funciona hoje mais como um fluxo paralelo de resolucao do que como uma simples reacao ao `AttackResult` do metodo `Attack(...)`.
- Isso torna a relacao entre `Attack` e `Evade` uma das areas mais sensiveis para revisao futura.

## Fontes

- `Creatures/Entities/Creature.cs:74-79`
- `Creatures/Entities/CreatureDefend.cs:25-125`
- `Attacks/Services/AttackService.cs:38-46`
- `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs:217-257`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs:29-387`

## Evasion no template base

- Formula: `10 + Evasion + DefenseBonus1 + ArmorBonus`
- O valor de `Evasion` dentro da formula vem da minor skill `Evasion`.
- `DefenseBonus1` depende da armadura equipada no peito.
- `ArmorBonus` vem do `LevelBonus` do item equipado no peito.

## O que o metodo `Evade(...)` faz

1. identifica a arma do atacante
2. calcula um total de hit ofensivo
3. infere quantos sucessos ofensivos precisam ser evitados
4. roda uma rolagem defensiva separada
5. converte sucessos restantes em hits e dano

## Relacao entre `Attack` e `Evade`

- `Attack(...)` e o caminho principal do servico de producao.
- `Evade(...)` existe no aggregate e e validado por testes diretos.
- Hoje, os dois metodos nao formam um unico pipeline integrado.
