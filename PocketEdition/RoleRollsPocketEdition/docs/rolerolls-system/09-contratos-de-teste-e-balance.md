# Contratos De Teste E Balance

## Objetivo da secao

Separar o que os testes travam com numeros exatos, o que travam por tendencia e o que e apenas observacao de balance.

## Implementado hoje

- O repositorio contem testes de roll, ataque, evade, balance abstrato e balance via `Creature.Attack(...)`.
- A leitura consolidada do que esses testes cobrem hoje esta resumida em `UnitTests/README.md`.

## Assumido por testes/balance

- Existe um baseline abstrato de armas e armaduras usado para estudar dominancia e viabilidade.
- Existe uma leitura de HP esperado para aproximadamente quatro turnos de sobrevivencia media.
- Parte das regras de sorte, vantagem e skill base dos docs de balance pertence ao modelo abstrato, nao ao runtime real.

## Divergencias e observacoes

- Nem todo numero de balance e uma regra viva do sistema de producao.
- Parte dos testes numericos usa cenarios deterministicos ou pools abstratos, nao simulacao completa do runtime.

## Fontes

- `UnitTests/README.md:8-225`
- `UnitTests/Attacks/Services/AttackServiceTests/BalanceSummary.md:5-55`
- `UnitTests/Attacks/Services/AttackServiceTests/HitPointScalingFormulas.md:1-114`
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs:200-686`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs:117-387`

## Contratos numericos fortes

- `AttackTests` trava matrizes de dano medio por nivel, arma e armadura.
- `EvadeTests` trava outra matriz numerica por nivel, arma e armadura.
- Existem cenarios diretos que travam:
  - falha de ataque com `TotalDamage = 0`
  - acerto de ataque com `TotalDamage = 10`
  - sucesso total de `Evade` com dano final `0`

## Contratos por tendencia

- `Luck +1` deve ajudar em cenarios proximos de 50 porcento.
- `Advantage +1 dado` deve aumentar chance de sucesso e, em varios modelos, dano medio.
- O sistema de balance procura manter respostas preferenciais por tipo de arma contra tipo de armadura.
- Os testes de roll procuram sempre alguma combinacao de `complexity/difficulty` proxima de 50 porcento por nivel.

## Modelos abstratos de balance

- Baseline abstrato de armas:
  - `Light`: dificuldade `1`, hit `+1`
  - `Medium`: dificuldade `2`, hit `0`
  - `Heavy`: dificuldade `3`, hit `0`
- Baseline abstrato de armaduras:
  - `Light`: dodge `2`, block `2`
  - `Medium`: dodge `1`, block `4`
  - `Heavy`: dodge `0` ou `-1` dependendo do teste, com block mais alto
- O HP recomendado costuma ser aproximado como `ceil(danoMedio * 4)`.

## Testes de observacao

- Alguns testes existem mais para logar tabelas e curvas do que para travar um contrato unico do runtime.
- Isso vale especialmente para saidas de balance, vitalidades por nivel e HP recomendado.
