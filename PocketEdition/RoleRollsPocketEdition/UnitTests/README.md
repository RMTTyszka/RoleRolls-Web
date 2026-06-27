# Comportamento atual coberto pelos testes unitarios

Este documento resume o que o projeto faz hoje segundo os testes em `UnitTests/`.
Se houver divergencia entre este texto e os testes, os testes sao a fonte de verdade.

## Escopo

- Hoje existem 49 testes (`[Fact]`) em 6 arquivos, mais 1 fixture de apoio (`BaseCreature`).
- Os testes ficam dentro do proprio projeto web, nao em um projeto de testes separado.
- A cobertura atual esta concentrada em 3 temas:
  - rolagens, CDs, sorte e vantagem
  - ataque, evasao, vitalidades e condicoes
  - balanceamento entre arma, armadura, nivel e HP esperado

## Onde cada arquivo ajuda

- `UnitTests/Core/BaseCreature.cs`
  - fixture usada pelos testes de combate
- `UnitTests/Rolls/SkillAndAttributeRollTests.cs`
  - pipeline real de `Roll`
- `UnitTests/Attacks/Services/AttackServiceTests/SkillAndAttributeDcTests.cs`
  - modelo probabilistico usado para calibrar CDs e teste de poder
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
  - cenarios diretos de ataque, vitalidade e matriz numerica de dano
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`
  - cenarios diretos de evasao e matriz numerica de dano com evasao
- `UnitTests/Attacks/Services/AttackServiceTests/RpgBalanceDesignTests.cs`
  - modelo abstrato de balanceamento arma x armadura
- `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`
  - as mesmas ideias de balance, mas passando pelo pipeline real de `Creature.Attack`

## Fixture base de combate

Os testes de combate partem desta configuracao em `BaseCreature`:

- criatura criada a partir de `LandOfHeroesTemplate.Template`
- todos os atributos com `3` pontos
- todas as specific skills com `1` ponto
- arma `Medium` contundente equipada na `MainHand`
- armadura `Medium` equipada no peito
- `FullRestore()` ao final da montagem

O helper `WithLevel(level)` sobe a criatura um nivel por vez e distribui pontos assim:

- atributos sobem nos niveis `6`, `11` e `16`
- skills gerais e specific skills sobem nos niveis `4`, `8` e `12`

Isso e importante porque varios testes de balance usam exatamente essa progressao como premissa.

## Regras de roll e CD que os testes assumem hoje

Os testes de rolagem fixam estas expectativas:

- atributo parte de `3` dados e ganha `+1` nos niveis `6`, `11` e `16`
- no pipeline real de `Roll`, a parte de skill parte de `1` dado e ganha `+1` nos niveis `4`, `8` e `12`
- no modelo probabilistico usado para balance de ataques (`SkillAndAttributeDcTests` e `RpgBalanceDesignTests`), a skill base usada e `2`
- os testes procuram CDs entre complexidade `10` e `22`
- para cada nivel, deve existir pelo menos uma combinacao de `complexity/difficulty` perto de `50%` de chance
- a dificuldade minima cresce apenas quando o atributo sobe de patamar:
  - niveis `1-5`: minima `1`
  - niveis `6-10`: minima `2`
  - niveis `11-15`: minima `3`
  - niveis `16-20`: minima `4`

Efeitos fixados pelos testes:

- `Luck +1` aumenta a chance de sucesso perto da CD de 50%
- `Luck -1` reduz a chance de sucesso perto da CD de 50%
- `Advantage +1 dado` aumenta a chance de sucesso
- `Disadvantage -1 dado` reduz a chance de sucesso

Semantica atual de sorte/azar nos modelos de simulacao:

- sorte positiva rerrola o menor dado que falhou, ignorando `1` e `20`, e fica com o melhor resultado
- azar negativo rerrola o maior dado que teve sucesso, ignorando `1` e `20`, e fica com o pior resultado

`SkillAndAttributeDcTests` tambem fixa o comportamento esperado do teste de poder (`PE`):

- passos sao derivados da margem positiva acima de `10`
- `k` menor gera mais passos em media
- com o mesmo `k`, `v` maior gera bonus medio maior

## Contratos diretos de ataque e evasao

Os testes de `AttackTests` e `EvadeTests` travam estes comportamentos basicos:

- quando um ataque falha, `Success` deve ser `false` e `TotalDamage` deve ser `0`
- quando um ataque acerta, o alvo pode tomar dano; um dos cenarios fixos valida `TotalDamage = 10`
- nas 9 combinacoes deterministicas de `PerformBasicAttack` (`Light/Medium/Heavy` x `Light/Medium/Heavy`), o ataque acerta mas o `TotalDamage` esperado hoje e `0`
- o resultado preserva referencias de `Attacker`, `Target` e `Weapon`
- a defesa de evasao nao sobe so porque o nivel bruto da criatura mudou
- quando `Evade` falha em todas as rolagens, o dano final e positivo
- quando `Evade` tem sucesso, todos os hits sao negados e o dano final e `0`

## Vitalidades e condicoes

Os testes atuais mostram que:

- ataque basico consome vitalidades respeitando `BasicAttackOrder`
- no cenario coberto, a ordem configurada e `Moral -> Life -> Mana`
- com `Moral = 1` e `Life = 1`, o ataque consome essas duas primeiro e `Mana` permanece intacta
- uma vitalidade em `<= 30%` expoe a condicao `Shaken`
- uma vitalidade em `0` expoe `Bleeding` e `Shaken`
- quando a vitalidade esta em `0`, o `CurrentStatus` observado no teste e `Bleeding`

## O que esta travado com numeros exatos

Alguns testes sao contratos numericos fortes, nao apenas tendencias:

- `AttackTests.cs`
  - fixa uma matriz completa de dano medio para `Creature.Attack`
  - cobre niveis `1..20`
  - cobre todas as combinacoes `Light/Medium/Heavy weapon` x `Light/Medium/Heavy armor`
- `EvadeTests.cs`
  - fixa outra matriz completa de dano medio para `Creature.Evade`
  - tambem cobre niveis `1..20` e as 9 combinacoes de arma x armadura
- `RpgBalanceDesignTests.FixedExampleMatchesPrompt`
  - com rolagens `[12, 8, 14, 15, 9]`, sem armadura e perfis neutros:
  - arma `Light` gera `3` hits, danos `[2, 4, 5]` e total `11`
  - arma `Heavy` gera `1` hit e total `11`

Esse exemplo mostra a regra central do modelo abstrato:

- sucessos sao agrupados pelo tamanho da dificuldade da arma
- `Light` usa grupos de `1`
- `Medium` usa grupos de `2`
- `Heavy` usa grupos de `3`
- o dano por hit e a soma dos excessos do grupo, somada ao bonus da arma e reduzida pelo block da armadura, com piso em `0`

## O que os testes validam por tendencia ou relacao

Os testes de balance nao fixam todos os numeros do sistema real, mas fixam varias relacoes:

- `Luck +1` aumenta dano medio por arma e por nivel
- um dado extra aumenta dano medio por arma e por nivel
- no modelo abstrato, `1` sucesso automatico extra tambem aumenta dano medio
- no pipeline real, `Advantage = 1` tambem deve aumentar dano medio
- arma leve deve ser a melhor resposta contra armadura leve
- arma media deve ser a melhor resposta contra armadura media
- arma pesada deve ser pelo menos tao boa quanto a media contra armadura pesada

No modelo abstrato de `RpgBalanceDesignTests`:

- a razao `min / max` do dano medio agregado precisa ser maior que `0.25`
- por nivel, a viabilidade minima precisa ficar acima de `0.07`

No pipeline real de `CreatureBalanceDesignTests`:

- a razao `min / max` agregada precisa ficar acima de `0.1` quando houver dano positivo
- a media por nivel usa um piso mais frouxo, acima de `0.06`

## Perfis de balance atualmente assumidos pelos testes abstratos

`RpgBalanceDesignTests` usa um modelo proprio para estudar balance. Hoje ele assume:

- armas baseline
  - `Light`: `Difficulty 1`, `HitBonus +1`
  - `Medium`: `Difficulty 2`, `HitBonus 0`
  - `Heavy`: `Difficulty 3`, `HitBonus 0`
- armaduras baseline
  - `Light`: `DodgeBonus 2`, `Block 2`
  - `Medium`: `DodgeBonus 1`, `Block 4`
  - `Heavy`: `DodgeBonus 0`, `Block 6`

Na versao por nivel/tier desse mesmo teste:

- o tier comeca em `1` no nivel `1`
- o tier sobe a cada `2` niveis
- bonus de dano por tier
  - `Light`: `3 * tier`
  - `Medium`: `5 * tier`
  - `Heavy`: `6 * tier + 2`
- block por tier
  - `Light`: `2 + 1 * tier`
  - `Medium`: `4 + 2 * tier`
  - `Heavy`: `4 + 3 * tier`
- armadura pesada passa a usar `DodgeBonus -1` nesse modelo escalado
- o modelo abstrato tambem embute um mapa de sorte contextual:
  - `Light x Light` e `Heavy x Heavy` usam sorte positiva
  - `Light x Heavy` e `Heavy x Light` usam azar negativo
  - os demais pares usam sorte neutra

## HP esperado pelos testes

Os dois conjuntos de testes de balance tratam HP da mesma forma conceitual:

- calculam o dano medio entre combinacoes de arma x armadura
- transformam isso em HP recomendado com `ceil(dano_medio * 4)`
- a ideia e estimar folego para aproximadamente `4` turnos

Os testes nao fixam uma tabela unica de HP no codigo. Eles fixam o metodo de estimativa e a tolerancia da aproximacao:

- no modelo abstrato, a estimativa com menos amostras precisa ficar entre `0.65x` e `1.4x` da simulacao mais robusta
- no pipeline real, a estimativa com menos amostras precisa ficar entre `0.65x` e `1.5x` da simulacao mais robusta

## Testes que sao mais de observacao do que de contrato

Alguns testes existem principalmente para gerar saida util no `ITestOutputHelper`:

- `AttackTests.T4` (`Attack and Evade test`)
  - loga dano medio de ataque e evasao; nao tem asserts finais
- `CreatureBalanceDesignTests.LogCreatureVitalitiesPerLevel`
  - loga as vitalidades por nivel; nao trava valores em assert
- `CreatureBalanceDesignTests.HitPointsNeededForFourRounds`
  - loga HP medio necessario; so exige que exista algum dano positivo
- `RpgBalanceDesignTests.AutoSearchBalancedProfiles`
  - busca combinacoes candidatas e exige apenas que exista pelo menos uma combinacao valida

## Leitura pratica do estado atual

Se voce quiser entender o sistema rapidamente hoje, a ordem mais util e:

1. `UnitTests/Core/BaseCreature.cs`
2. `UnitTests/Rolls/SkillAndAttributeRollTests.cs`
3. `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
4. `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`
5. `UnitTests/Attacks/Services/AttackServiceTests/RpgBalanceDesignTests.cs`
6. `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`

## Documentos complementares ja existentes

- `UnitTests/Attacks/Services/AttackServiceTests/BalanceSummary.md`
- `UnitTests/Attacks/Services/AttackServiceTests/HitPointScalingFormulas.md`

Esses dois arquivos continuam uteis como referencia de balance, mas este `README` e o panorama mais amplo do que os testes cobrem hoje.
