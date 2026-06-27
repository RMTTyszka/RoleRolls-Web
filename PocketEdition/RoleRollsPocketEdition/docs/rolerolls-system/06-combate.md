# Combate

## Objetivo da secao

Explicar como o metodo `Creature.Attack(...)` transforma valor de hit em sucessos, hits, block e dano aplicado ao alvo.

## Implementado hoje

- O servico de ataque monta um `AttackCommand` e chama `Creature.Attack(...)`.
- A arma efetiva vem do slot pedido no comando; se nao houver arma equipada, o metodo cai em uma arma padrao `Medium` contundente.
- O hit usa a `HitProperty` obtida por `ItemConfiguration.GetWeaponHitProperty(weaponCategory)`.
- Na pratica, o fluxo principal nao usa `AttackCommand.HitProperty` para escolher a propriedade de hit.
- A defesa do alvo entra como `complexity`.
- Cada sucesso guarda o excesso `totalRolado - complexity` quando esse valor e maior ou igual a zero.
- Os sucessos sao agrupados pela dificuldade da arma.
- O block do alvo combina `ArmorDefinition.TotalBlock(...)` com a `BlockProperty` configurada.
- O dano por hit hoje usa `max(chunkDamage + damageBonusPerHit - block, 1)`.
- Cada hit aplica dano imediatamente nas vitalidades do alvo, respeitando a ordem basica configurada.

## Assumido por testes/balance

- Os testes travam falha sem dano e pelo menos um cenario de acerto com dano fixo.
- Os testes de matriz numerica usam cenarios deterministas para comparar arma, armadura e nivel.
- Os modelos abstratos de balance tratam acerto e dano medio como base para calibrar dominancias entre `Light`, `Medium` e `Heavy`.

## Divergencias e observacoes

- Alguns testes antigos ainda esperam `Success = true` com `TotalDamage = 0` em combinacoes basicas de arma x armadura.
- O runtime atual aplica piso minimo `1` por hit em `Creature.Attack(...)`.
- O modelo de dano atual parte diretamente dos excessos acima da defesa e por isso esta no centro das pendencias de design.

## Fontes

- `Attacks/Services/AttackService.cs:72-87`
- `Creatures/Entities/CreatureAttack.cs:18-184`
- `Itens/GripType.cs:20-225`
- `Itens/Configurations/ArmorDefinition.cs:6-53`
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs:29-313`
- `UnitTests/README.md:83-150`

## Fluxo do servico

1. `AttackService` carrega atacante, alvo e `ItemConfiguration`.
2. O servico monta `AttackCommand`.
3. O servico chama `attacker.Attack(target, command, diceRoller)`.
4. O resultado do ataque vira `SceneAction` processada pelo fluxo de cena.

## Escolha da arma e do grip

- A categoria da arma vem do item equipado no slot pedido.
- O `GripType` atual do equipamento define hit, dano base e dificuldade da arma.
- Para o baseline principal:
  - `OneLightWeapon`: hit `+1`, bonus base de dano `3`, dificuldade `1`
  - `OneMediumWeapon`: hit `+0`, bonus base de dano `5`, dificuldade `2`
  - `TwoHandedHeavyWeapon`: hit `+0`, bonus base de dano `6`, dificuldade `3`

## Calculo de hit

- O valor de hit e lido por `GetHitValue(...)`.
- O numero de dados do ataque e `hitValue.Total`.
- O bonus de hit tambem inclui `hitValue.Total`.
- Alem disso, o hit soma:
  - bonus do grip
  - buffs de hit
  - diferenca de nivel entre atacante e alvo
  - `LevelBonus` da arma

## Calculo de sucessos acima da defesa

- O ataque roda um `Roll` com `difficulty = gripStats.AttackDifficult`.
- Depois do roll, o metodo relista os resultados ja somados com bonus.
- Cada dado com `totalRolado >= complexity` entra na lista de sucessos.
- O valor salvo para cada sucesso e o excesso `totalRolado - complexity`.

## Agrupamento por dificuldade da arma

- `Light`: 1 sucesso por hit
- `Medium`: 2 sucessos por hit
- `Heavy`: 3 sucessos por hit

O numero final de hits e `successes.Count / difficulty`.

## Block e reducao

- O block total do alvo combina armadura e propriedade de block.
- A armadura usa esta base no runtime atual:
  - `Light`: dodge `2`, block base `2`, block por tier `1`
  - `Medium`: dodge `1`, block base `4`, block por tier `2`
  - `Heavy`: dodge `-1`, block base `4`, block por tier `3`
- O ataque ainda aplica um mapa contextual de sorte entre arma e armadura:
  - `Light x Light`: `Luck +1`
  - `Light x Heavy`: `Luck -1`
  - `Heavy x Heavy`: `Luck +1`
  - `Heavy x Light`: `Luck -1`

## Aplicacao de dano nas vitalidades

- Para cada hit, o metodo soma os excessos do grupo daquele hit.
- Depois soma `damageBonusPerHit`.
- Depois reduz `block`.
- O resultado final de cada hit e truncado por `Math.Max(..., 1)`.
- Cada hit chama `ApplyBasicAttackDamage(...)`, que percorre as vitalidades do alvo em ordem configurada.

## Exemplo minimo

- defesa do alvo: `15`
- resultados finais dos dados: `15` e `15`
- excesso por dado: `0` e `0`
- com arma `Light`, os dois sucessos formam `2` hits
- com arma `Medium`, os dois sucessos formam `1` hit
- com arma `Heavy`, os dois sucessos nao formam hit suficiente

No runtime atual, empate com a defesa ainda conta como sucesso; o efeito final sobre dano depende do agrupamento por dificuldade da arma e do piso minimo de dano por hit.

## Divergencias atuais do modelo de dano

- O dano hoje nasce do excesso acima da defesa, nao de uma rolagem de dano independente por hit no fluxo principal de `Attack(...)`.
- Isso aproxima acerto e dano em uma mesma estrutura matematica.
- Os testes antigos que esperam dano zero em acertos basicos nao refletem integralmente o comportamento atual do metodo.
