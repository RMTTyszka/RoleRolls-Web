# RoleRolls — Livro de Regras do Sistema Base

## Como usar este livro

O `RoleRolls` é um motor de RPG em que a campanha define o conteúdo e o
sistema base resolve as regras. Este livro ensina a usar a ficha, realizar
testes, combater, aplicar desgaste e evoluir uma criatura.

Cada regra apresenta o procedimento geral e um exemplo do Land of Heroes, o
universo padrão. Os nomes, fórmulas e listas desse exemplo mostram uma campanha
concreta usando as estruturas do sistema.

## 1. A estrutura do jogo

O sistema base oferece:

- atributos, perícias e especialidades para representar capacidades;
- defesas, vitalidades e condições para representar proteção e estado;
- testes de d20 resolvidos por complexidade, dificuldade e sucessos;
- ataque básico, Evasion, bloqueio, dano e desgaste;
- fórmulas para calcular recursos e valores da ficha.

A campanha define os atributos, as perícias, as especialidades, as defesas, as
vitalidades, as condições, as fórmulas e as propriedades usadas por armas.

### Exemplo: Land of Heroes

O Land of Heroes usa os atributos `Agility`, `Charisma`, `Intelligence`,
`Intuition`, `Strength` e `Vigor`. Suas vitalidades são `Life`, `Moral` e
`Mana`; sua defesa principal é `Evasion`.

## 2. A ficha da criatura

### Atributos

Um atributo representa uma capacidade ampla. Ele fornece a base de uma
especialidade ligada a ele e pode participar de fórmulas de defesa e
vitalidade.

Uma criatura começa no nível 1 com `1` ponto em cada atributo. O limite de um
atributo é:

```text
4 + piso(nível / 6)
```

### Perícias e especialidades

Uma perícia reúne uma área de competência. Suas especialidades descrevem as
ações concretas que a criatura realiza dentro dessa área.

O valor de uma especialidade é:

```text
atributo ligado + pontos da especialidade + bônus aplicáveis
```

No Land of Heroes, `Combat` é uma perícia. `MeleeMediumWeapon`, `Evasion` e
`Concentrate` são especialidades de Combat. `Evasion` usa `Agility` como
atributo ligado.

Uma personagem com `Agility 3` e `Evasion 2` possui total de Evasion `5` antes
de bônus de equipamento e condições.

### Defesas

Uma defesa é o valor que uma ação ofensiva precisa alcançar ou superar. A
campanha calcula esse valor por fórmula.

No Land of Heroes:

```text
Evasion = 10 + Evasion + bônus de defesa da armadura + bônus de nível da armadura
```

Uma criatura com Evasion `5`, armadura leve (`+2`) e item de nível `0` possui
Evasion estática `17`. Esse valor é usado quando a campanha resolve uma Defesa
estática, como em ataques feitos por um jogador contra uma criatura controlada
pelo mestre.

### Vitalidades

Vitalidades armazenam recursos que absorvem desgaste, dano ou consumo. Cada
vitalidade possui uma fórmula de máximo, um valor atual e pode expor condições
quando cruza limites.

No Land of Heroes:

```text
Life  = 4 × Vigor + 2 × Level + Growth
Moral = 4 × Intuition + 2 × Level + Growth + 2 × Tier
Mana  = 10 + 2 × Intelligence
```

Uma criatura de nível 1 com `Vigor 3`, `Intuition 3` e `Intelligence 3` possui
`Life 14`, `Moral 16` e `Mana 16`.

### Condições

Condições expressam o estado atual de uma criatura. A campanha pode associar
uma condição à faixa crítica de `30%` de uma vitalidade e outra ao valor `0`.

No Land of Heroes:

- `Moral` em 30% ou menos expõe `Shaken`;
- `Moral` em 0 expõe `Bleeding` e `Shaken`;
- `Life` em 30% ou menos expõe `Debilitated`.

## 3. Progressão

Ao subir de nível, a criatura recebe pontos de especialidade em cada perícia.
Esses pontos pertencem à perícia que os concedeu e são distribuídos apenas
entre suas próprias especialidades.

Para uma perícia com `E` especialidades:

```text
reserva inicial = 2 + E
pontos recebidos por nível = teto(E / 3)
limite de uma especialidade = nível + 2
```

### Exemplo: Land of Heroes

`Awareness` possui quatro especialidades. Ela começa com seis pontos para
distribuir e concede dois pontos a cada nível. No nível 1, cada especialidade
de Awareness aceita até três pontos.

`Combat` possui onze especialidades. Ela começa com treze pontos para
distribuir e concede quatro pontos a cada nível. Os pontos de Combat fortalecem
somente especialidades de Combat.

Uma personagem de nível 1 pode distribuir os seis pontos de Awareness como
`Observe 3`, `Listen 3`, `Search 0` e `Feeling 0`. Ao alcançar o nível 2, ela
recebe mais dois pontos nessa reserva e o limite de cada especialidade passa a
ser quatro.

## 4. Testes

Todo teste segue seis passos:

1. escolher a propriedade que representa a ação;
2. determinar a quantidade de d20 e os bônus aplicáveis;
3. definir a Complexidade de cada dado;
4. definir a Dificuldade do teste completo;
5. rolar os dados e contar sucessos;
6. aplicar o efeito definido pela regra que iniciou o teste.

### Complexidade e Dificuldade

Complexidade é o resultado que cada dado precisa alcançar para gerar um
sucesso. Dificuldade é a quantidade de sucessos exigida pelo teste completo.

Cada resultado final igual ou maior que a Complexidade gera um sucesso. O teste
é bem-sucedido quando o total de sucessos alcança a Dificuldade.

```text
sucessos de resolução = piso(total de sucessos / Dificuldade)
```

### Exemplo: teste de especialidade

Uma exploradora usa uma especialidade com total `5` para procurar uma passagem.
O mestre define Complexidade `14` e Dificuldade `2`. Ela rola cinco d20 e obtém
`10`, `14`, `17`, `7` e `20`.

Os resultados `14`, `17` e `20` geram três sucessos. Como a ação exigia dois,
ela é bem-sucedida. A regra da cena pode usar o terceiro sucesso para aumentar
a qualidade da descoberta.

### Vantagem e Sorte

Vantagem adiciona dados à rolagem. Sorte positiva rerrola os menores dados e
conserva os maiores; sorte negativa rerrola os maiores dados e conserva os
menores. Em todos os testes, resultado alto é favorável.

Um resultado natural `20` que alcança a Complexidade é crítico. Um resultado
natural `1` que fica abaixo da Complexidade é falha crítica.

## 5. Ataque básico

O ataque básico resolve um ataque armado iniciado por um jogador. Ele usa a
arma equipada, a categoria da arma, a especialidade ofensiva configurada pela
campanha, a Defesa do alvo, bloqueio, dano e vitalidades.

### Procedimento

1. escolha a arma e a Defesa do alvo;
2. obtenha a especialidade ofensiva da categoria da arma;
3. role um d20 para cada ponto do total ofensivo;
4. some o bônus ofensivo a cada dado;
5. cada dado igual ou maior que a Defesa do alvo gera um sucesso;
6. calcule o excesso de cada sucesso;
7. agrupe excessos pela dificuldade da arma para formar hits;
8. aplique dano, bloqueio e vitalidades para cada hit.

O bônus ofensivo é:

```text
total da especialidade ofensiva
+ bônus de hit do grip
+ buffs de hit
+ diferença de nível entre atacante e alvo
+ bônus de nível da arma
```

O excesso de um sucesso é:

```text
resultado final do dado − Defesa do alvo
```

### Categorias de arma

As categorias determinam quantos sucessos formam um hit.

| Categoria | Sucessos por hit | Land of Heroes: bônus de hit | Bônus-base de dano por nível |
|---|---:|---:|---:|
| Leve | 1 | +1 | 3 |
| Média | 2 | +0 | 5 |
| Pesada, duas mãos | 3 | -1 | 8 |

Os excessos são ordenados do maior para o menor. Cada grupo completo forma um
hit; excessos fora de um grupo completo não formam hit.

### Dano e bloqueio

Para cada hit:

```text
dano = máximo(
  soma dos excessos do grupo
  + bônus de dano por hit
  − bloqueio do alvo,
  1
)
```

O bloqueio combina a proteção da armadura e a propriedade de bloqueio definida
pela campanha. No Land of Heroes, essa propriedade é `Vigor`.

| Armadura | Bônus de Evasion | Bloqueio da armadura |
|---|---:|---:|
| Leve | +2 | 2 + nível × 1 |
| Média | +1 | 4 + nível × 2 |
| Pesada | -1 | 4 + nível × 3 |

### Exemplo: ataque médio

Uma guerreira de nível 1 possui `MeleeMediumWeapon 4` e usa arma média. O alvo
tem Evasion `17`, `Vigor 2` e armadura leve. Nenhum dos lados possui bônus de
nível ou buffs.

O bônus ofensivo é `4`. A guerreira rola quatro dados: `15`, `13`, `9` e `4`.
Os totais são `19`, `17`, `13` e `8`. Os dois primeiros dados geram sucessos
com excessos `2` e `0`.

A arma média agrupa os dois excessos em um hit. Seu bônus de dano por hit é
`5`. O bloqueio do alvo é `3` da armadura leve mais `2` de Vigor, total `5`.

```text
dano = máximo(2 + 0 + 5 − 5, 1) = 2
```

## 6. Evasion rolada pelo defensor

Evasion resolve um ataque básico recebido por uma criatura controlada por
jogador. O atacante fornece valores estáticos; o defensor realiza todos os
d20 da resolução.

### Procedimento

1. obtenha a arma e a especialidade ofensiva do atacante;
2. calcule quantos dados o atacante rolaria em um ataque básico;
3. calcule a Dificuldade de Evasion;
4. o defensor rola essa quantidade de d20 de Evasion;
5. some o bônus de Evasion a cada dado;
6. transforme resultados que falharam em excessos;
7. agrupe excessos pela dificuldade da arma;
8. aplique dano, bloqueio e vitalidades.

```text
dados-base de Evasion = total da especialidade ofensiva do atacante

Dificuldade de Evasion = 10 + bônus ofensivo do atacante

resultado de Evasion = d20 + bônus de Evasion
```

O bônus de Evasion usa a especialidade defensiva definida pela campanha, os
bônus da armadura, o bônus de nível da armadura e buffs. No Land of Heroes, a
especialidade é `Evasion`.

Um resultado de Evasion estritamente maior que a Dificuldade evita uma
tentativa. Um resultado igual favorece o atacante e gera excesso `0`. Um
resultado menor gera:

```text
excesso = Dificuldade de Evasion − resultado de Evasion
```

Vantagem adiciona dados à rolagem defensiva; o defensor conserva somente os
melhores resultados até completar a quantidade-base. Sorte segue a regra geral
do sistema e favorece resultados altos quando é positiva.

### Exemplo: Evasion contra arma média

Um inimigo usa arma média e possui total ofensivo `4`. Seus bônus de arma e de
efeito somam `3`, portanto seu bônus ofensivo total é `7`. O defensor rola
quatro d20 de Evasion contra Dificuldade `17`.

Uma personagem com `Agility 3`, `Evasion 2` e armadura leve possui bônus de
Evasion `7`. Ela rola `20`, `15`, `12` e `8`, obtendo `27`, `22`, `19` e `15`.

Os três primeiros resultados evitam tentativas. O quarto gera excesso `2`. A
arma média exige dois excessos para formar hit, então o ataque termina sem
dano. Se duas falhas produzirem excessos `5` e `2`, elas formam um hit com
excesso total `7` antes de aplicar bônus de dano e bloqueio.

## 7. Ataque especial

O ataque especial resolve um teste ofensivo que poderes, magias, manobras e
outras regras usam para aplicar seus próprios efeitos.

O jogador escolhe uma especialidade, uma Defesa do alvo, Sorte e Vantagem. O
motor rola a especialidade escolhida contra a Defesa indicada com Dificuldade
`1`. A Defesa atual do alvo é a Complexidade.

O resultado informa sucessos, dados, bônus, especialidade e Defesa. A regra
que iniciou a ação aplica a consequência correspondente, como dano de magia,
condição, duração, deslocamento ou outro efeito.

### Exemplo: magia de medo

Uma conjuradora possui total `5` na especialidade usada pela magia. Ela usa um
ataque especial contra Evasion `17` e recebe bônus `5` em cada dado. Seus
resultados brutos são `12`, `9`, `7`, `4` e `2`; os totais são `17`, `14`,
`12`, `9` e `7`.

O resultado `17` gera um sucesso e satisfaz a Dificuldade `1`. A magia aplica
o efeito de medo definido em sua própria regra.

## 8. Área de ameaça e ataque de oportunidade

Uma criatura portando uma arma corpo a corpo ameaça um raio de `1,5 m` ao seu
redor. Uma criatura portando somente uma arma de ataque a distância não possui
área de ameaça.

Uma arma específica, como uma arma de haste, ou uma habilidade ou poder pode
aumentar essa área. A regra específica informa o novo alcance e prevalece sobre
o valor padrão.

### Gatilho

Uma criatura provoca ataque de oportunidade quando se desloca dentro da área de
ameaça de outra criatura e um trecho desse deslocamento não reduz a distância
até a criatura que ameaça. Deslocar-se lateralmente ou afastar-se satisfaz esse
gatilho.

Uma criatura pode entrar na área e continuar aproximando-se sem provocar o
ataque. Se, depois de entrar, ela mudar a direção para mover-se de lado ou para
longe, provoca o ataque nesse ponto do deslocamento.

Atacar outra criatura adjacente sem se deslocar não provoca ataque de
oportunidade.

### Resolução e limite

A criatura que ameaça pode realizar um Ataque Básico com a arma corpo a corpo
que concede a área de ameaça. Para esta regra, o ataque tem sucesso quando a
resolução forma ao menos um hit.

Cada criatura pode realizar no máximo um ataque de oportunidade por rodada.
Uma habilidade ou poder pode alterar esse limite.

Se o ataque tiver sucesso, o deslocamento que o provocou é interrompido no
local em que a criatura se encontra. Se falhar, o deslocamento continua.
Habilidades ou poderes podem interromper o deslocamento mesmo quando o ataque
falha, caso seu texto o determine.

### Exemplo: mudar direção dentro da área

Uma combatente com espada ameaça `1,5 m`. Uma exploradora entra nessa área e
segue em direção à combatente: não provoca ataque de oportunidade. Ainda dentro
da área, a exploradora muda a direção e tenta atravessar ao lado da combatente:
ela provoca o ataque. Se a combatente formar ao menos um hit, o movimento da
exploradora termina; caso contrário, ela continua o movimento.

Se uma criatura já adjacente à combatente atacar outro inimigo ao seu lado, sem
se deslocar, não há ataque de oportunidade.

## 9. Vitalidades, desgaste e condições

O ataque básico e a Evasion aplicam cada hit na ordem de vitalidades da
campanha. Quando uma vitalidade chega a zero, o dano restante segue para a
próxima vitalidade da ordem.

No Land of Heroes, a ordem padrão é:

1. `Moral`;
2. `Life`.

Um hit de dano `20` contra uma criatura com `Moral 6` e `Life 14` reduz Moral
a `0`, transfere `14` de dano para Life e deixa Life em `0`. As condições de
Moral e Life são atualizadas pelos seus limites.

## 10. Sequência de jogo

Em uma cena, use esta sequência:

1. descreva a intenção da criatura;
2. escolha a especialidade, a arma ou a regra que representa a ação;
3. defina Defesa, Complexidade e Dificuldade quando a regra pedir;
4. realize o teste correspondente;
5. transforme sucessos, excessos e hits no efeito da ação;
6. atualize dano, vitalidades e condições;
7. registre a consequência na cena e continue a ficção.

O sistema base organiza a resolução. A campanha usa essa estrutura para criar
suas próprias criaturas, perigos, poderes, itens e histórias.
