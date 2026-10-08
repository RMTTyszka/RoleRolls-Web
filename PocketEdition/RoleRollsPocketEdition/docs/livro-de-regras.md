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
- ataque básico, Evasão, bloqueio, dano e desgaste;
- fórmulas para calcular recursos e valores da ficha.

A campanha define os atributos, as perícias, as especialidades, as defesas, as
vitalidades, as condições, as fórmulas e as propriedades usadas por armas.

### Exemplo: Land of Heroes

O Land of Heroes usa os atributos Agilidade, Carisma, Inteligência, Intuição,
Força e Vigor. Suas vitalidades são Vida, Moral e Mana; sua defesa principal é
Evasão.

## 2. A ficha da criatura

### Atributos

Um atributo representa uma capacidade ampla. Ele fornece a base de uma
especialidade ligada a ele e pode participar de fórmulas de defesa e
vitalidade.

O RoleRolls limita os pontos de um atributo por nível:

```text
3 + piso(nível / 6)
```

Portanto, o teto é `3` nos níveis 1–5, `4` nos níveis 6–11, `5` nos níveis
12–17 e `6` nos níveis 18–20. O sistema recomenda começar com `2` pontos por
atributo. A campanha, porém, define o orçamento total, o piso e qualquer regra
de criação específica.

No Land of Heroes, por exemplo, a personagem distribui exatamente `12` pontos
entre seis atributos, com mínimo `1` e máximo `3` no nível 1. Consulte o
[Livro de Campanha do Land of Heroes](land-of-heroes/livro-de-regras.md) para
as regras completas dessa campanha.

### Perícias e especialidades

Uma perícia reúne uma área de competência. Suas especialidades descrevem as
ações concretas que a criatura realiza dentro dessa área.

O valor de uma especialidade é:

```text
atributo ligado + pontos da especialidade + bônus aplicáveis
```

No Land of Heroes, Combate é uma perícia. Arma Corpo a Corpo Média, Evasão e
Concentração são especialidades de Combate. Evasão usa Agilidade como atributo
ligado.

Uma personagem com Agilidade `3` e Evasão `2` possui total de Evasão `5` antes
de bônus de equipamento e condições.

### Defesas

Uma defesa é o valor que uma ação ofensiva precisa alcançar ou superar. A
campanha calcula esse valor por fórmula.

No Land of Heroes:

```text
Evasão = 10 + Evasão + bônus de defesa da armadura + bônus de nível da armadura
```

Uma criatura com Evasão `5`, armadura leve (`+2`) e item de nível `0` possui
Evasão estática `17`. Esse valor é usado quando a campanha resolve uma Defesa
estática, como em ataques feitos por um jogador contra uma criatura controlada
pelo mestre.

### Vitalidades

Vitalidades armazenam recursos que absorvem desgaste, dano ou consumo. Cada
vitalidade possui uma fórmula de máximo, um valor atual e pode expor condições
quando cruza limites.

No Land of Heroes:

```text
Vida = 4 × Vigor + 2 × Nível + Crescimento
Moral = 4 × Intuição + 2 × Nível + Crescimento + 2 × Grau
Mana = 2 + nível / 6
```

Uma criatura de nível 1 com Vigor `3` e Intuição `3` possui Vida `14`, Moral
`16` e Mana `2`.

### Condições

Condições expressam o estado atual de uma criatura. A campanha pode associar
uma condição à faixa crítica de `30%` de uma vitalidade e outra ao valor `0`.

No Land of Heroes:

- Moral em 30% ou menos expõe Abalado;
- Moral em 0 expõe Sangrando e Abalado;
- Vida em 30% ou menos expõe Debilitado.

## 3. Progressão

### Grau, crescimento e arredondamento

O RoleRolls disponibiliza Grau (`Tier`) e Crescimento (`Growth`) como valores
derivados do nível. Eles não são pontos distribuídos pelo jogador. Cada
campanha escolhe se os utiliza e em quais fórmulas; sua definição e seu
cálculo pertencem ao sistema base. Para criaturas de nível 1 ou superior:

| Valor derivado | Fórmula |
|---|---|
| Grau (`Tier`) | `1 + piso((nível - 1) / 2)` |
| Crescimento (`Growth`) | `piso(Grau × Grau / 2)` |

`piso` significa arredondar para baixo. No nível 1, Grau é `1` e Crescimento
é `0`. Esses valores podem participar de fórmulas de defesa, vitalidade e
outros recursos definidos pela campanha.

O resultado final de toda fórmula numérica é arredondado para baixo, depois
de avaliar a expressão inteira. Não arredonde cada termo isoladamente.
Por exemplo, `5 / 2` resulta em `2` e `-6 / 5` resulta em `-2`.

### Pontos de especialidade

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

Percepção possui quatro especialidades. Ela começa com seis pontos para
distribuir e concede dois pontos a cada nível. No nível 1, cada especialidade
de Percepção aceita até três pontos.

Combate possui onze especialidades. Ela começa com treze pontos para
distribuir e concede quatro pontos a cada nível. Os pontos de Combate fortalecem
somente especialidades de Combate.

Uma personagem de nível 1 pode distribuir os seis pontos de Percepção como
Observar `3`, Ouvir `3`, Procurar `0` e Sentir `0`. Ao alcançar o nível 2, ela
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
sucessos de resolução = total de sucessos / Dificuldade
```

### Exemplo: teste de especialidade

Uma exploradora usa uma especialidade com total `5` para procurar uma passagem.
O mestre define Complexidade `14` e Dificuldade `2`. Ela rola cinco d20 e obtém
`10`, `14`, `17`, `7` e `20`.

Os resultados `14`, `17` e `20` geram três sucessos. Como a ação exigia dois,
ela é bem-sucedida. A regra da cena pode usar o terceiro sucesso para aumentar
a qualidade da descoberta.

### Modificadores de rolagem

Estados de rolagem usam um grau em algarismo romano: `I` corresponde a um,
`II` a dois e assim por diante. Vantagem adiciona à rolagem a quantidade de
dados indicada pelo grau. No ataque básico e em Evasão, se houver Vantagem
no comando e em um bônus, vale o maior grau; os graus não se somam.

Sorte e Azar rerrolam a quantidade de dados indicada pelo grau. Sorte rerrola
os menores resultados e conserva o maior de cada par; Azar rerrola os maiores
e conserva o menor. Internamente, Sorte é positiva e Azar é negativo.

Bônus `+N` soma `N` ao valor estático da aplicação indicada. Penalidade `-N`
subtrai `N` desse valor. Bônus de Acerto e Evasão participam do ataque básico
e de Evasão.

Desvantagem e Penalidade pertencem ao modelo de bônus e podem aparecer em
manobras. No sistema atual, os caminhos de ataque básico e Evasão não os
consultam; portanto não removem dados nem subtraem valores nesses fluxos.

As aplicações são: **Acerto**, para rolagens ofensivas; **Evasão**, para
rolagens defensivas; e **propriedade**, quando uma habilidade identifica outra
rolagem. Em todos os testes, resultado alto é favorável.

Um resultado natural `20` que alcança a Complexidade é crítico. Um resultado
natural `1` que fica abaixo da Complexidade é falha crítica.

## 5. Ataque básico

O ataque básico resolve um ataque armado iniciado por um jogador. Ele usa a
arma equipada, a categoria da arma, a especialidade ofensiva configurada pela
campanha, a Defesa do alvo, bloqueio, dano e vitalidades.

### Turnos e rodadas

Um **turno** é a oportunidade individual de uma criatura agir. No início do
seu turno, ela recupera as ações descritas neste livro.

Uma **rodada** é um ciclo completo de iniciativa: começa com a primeira
criatura na ordem e termina quando cada criatura participante teve um turno.
A iniciativa determina essa ordem. Se uma criatura for derrotada antes de seu
turno, ela não age; ainda assim, a rodada parcial conta como uma rodada.

Para medir um combate, conte as rodadas iniciadas até a derrota. O dano médio
por rodada de uma criatura é o dano total que ela causou dividido pelo total
de rodadas do combate.

### Iniciativa

Cada template de campanha define `IniciativePropertyId`: o ID da propriedade
que determina iniciativa. Ele pode apontar para atributo, perícia,
especialidade, Defesa ou vitalidade; o tipo é resolvido pela propriedade que a
criatura possui.

Para cada criatura participante, obtenha o valor total dessa propriedade,
incluindo seus bônus aplicáveis, e role `max(1, 1 + valor da propriedade)`
d20. A iniciativa da criatura é apenas o maior dado rolado.

A ordem fica do maior score para o menor. Em empate de score, age primeiro
quem tiver maior valor da propriedade. Persistindo empate, role novamente
somente para as criaturas empatadas, com a mesma quantidade de d20, até
definir a ordem. O score original não muda por causa desse desempate.

Cada cena mantém uma única lista de iniciativa ativa. Ela pode ser rolada
para todos os participantes, receber uma criatura com rolagem individual ou
remover uma criatura; posição, score, valor da propriedade e dados iniciais
ficam registrados na cena.

### Procedimento

1. escolha a arma e a Defesa do alvo;
2. obtenha a especialidade ofensiva da categoria da arma;
3. role um d20 para cada ponto do total ofensivo;
4. some o bônus ofensivo a cada dado;
5. cada dado igual ou maior que a Defesa do alvo gera um sucesso;
6. calcule o excesso de cada sucesso;
7. agrupe excessos pela dificuldade da empunhadura para formar acertos;
8. aplique dano, bloqueio e vitalidades para cada acerto.

O bônus ofensivo é:

```text
total da especialidade ofensiva
+ bônus de Acerto da empunhadura
+ Bônus de Acerto
+ diferença de nível entre atacante e alvo
+ bônus de nível da arma
```

O bônus de nível da arma é o nível do item dividido por `2`.

O excesso de um sucesso é:

```text
resultado final do dado − Defesa do alvo
```

### Empunhaduras de arma

O equipamento determina a empunhadura efetiva pelas armas das mãos principal e
secundária. Quando o modelo de uma arma define uma empunhadura, ela tem
precedência. Sem essa definição, arma leve usa empunhadura leve de uma mão,
arma média usa empunhadura média de uma mão, arma pesada usa empunhadura
pesada de duas mãos, e cada categoria de escudo usa a empunhadura equivalente.

| Empunhadura efetiva | Bônus de Acerto | Bônus fixo por acerto | Bônus por nível do atacante | Sucessos por acerto |
|---|---:|---:|---:|---:|
| Arma leve, uma mão | +1 | 0 | 3 | 1 |
| Arma média, uma mão | +0 | 0 | 5 | 2 |
| Arma pesada, duas mãos | -1 | 2 | 8 | 3 |
| Duas armas leves | -1 | 0 | 3 | 1 |
| Duas armas médias | -1 | 0 | 4 | 2 |
| Arma pesada, uma mão | -1 | 0 | 8 | 3 |
| Arma média, duas mãos | +2 | 8 | 5 | 3 |
| Escudo leve | +0 | 4 | 0 | 1 |
| Escudo médio | +1 | 8 | 0 | 2 |
| Escudo pesado | +3 | 12 | 0 | 3 |

Os excessos são ordenados do maior para o menor. Cada grupo completo forma um
acerto; excessos fora de um grupo completo não formam acerto.

### Dano e bloqueio

O bônus de dano por acerto é:

```text
bônus de dano por acerto = bônus fixo da empunhadura
  + (bônus da empunhadura por nível × nível do atacante)
```

Para cada acerto:

```text
dano = máximo(
  soma dos excessos do grupo
  + bônus de dano por acerto
  − bloqueio do alvo,
  1
)
```

Cada acerto causa no mínimo `1` ponto de dano depois de aplicar o bloqueio.

O bloqueio combina a proteção da armadura e a propriedade de bloqueio definida
pela campanha. No Land of Heroes, essa propriedade é `Vigor`.

| Armadura | Bônus de Evasão | Bloqueio-base | Bloqueio por nível |
|---|---:|---:|---:|
| Nenhuma | +0 | 0 | 0 |
| Leve | +2 | 2 | 1 |
| Média | +1 | 4 | 2 |
| Pesada | -1 | 4 | 3 |

O nível desta fórmula é o do defensor:

```text
bloqueio = bloqueio-base
  + (bloqueio por nível × nível do defensor)
  + propriedade de bloqueio da campanha
```

### Sorte por arma e armadura

Durante o ataque básico e a Evasão, a categoria da arma do atacante pode
alterar a Sorte da rolagem conforme a armadura do defensor. A matriz usa a
categoria da arma, não a empunhadura. Todas as combinações ausentes são
neutras; armas médias, escudos e armas sem categoria não alteram a Sorte.

| Arma | Armadura leve | Armadura média | Armadura pesada | Sem armadura |
|---|---|---|---|---|
| Leve | Sorte I | Neutro | Azar I | Neutro |
| Média | Neutro | Neutro | Neutro | Neutro |
| Pesada | Azar I | Neutro | Sorte I | Neutro |
| Escudo | Neutro | Neutro | Neutro | Neutro |

### Exemplo: ataque com arma média

Uma guerreira de nível 1 possui Arma Corpo a Corpo Média `4` e usa arma média. O alvo
tem Evasão `17`, Vigor `2` e armadura leve. Nenhum dos lados possui bônus de
nível ou bônus.

Arma média sem empunhadura explícita usa empunhadura média de uma mão: bônus de
Acerto `+0`, bônus fixo `0`, bônus por nível `5` e dois sucessos por acerto.
O bônus ofensivo é `4`. A guerreira rola quatro dados: `15`, `13`, `9` e `4`.
Os totais são `19`, `17`, `13` e `8`. Os dois primeiros dados geram sucessos
com excessos `2` e `0`.

A empunhadura agrupa os dois excessos em um acerto. Seu bônus de dano por
acerto é `5`. O bloqueio do alvo é `2 + (1 × 1)` da armadura leve mais `2` de
Vigor, total `5`.

```text
dano = máximo(2 + 0 + 5 − 5, 1) = 2
```

## 6. Manobras de combate

Manobras são escolhas de combate. Uma manobra **instantânea** modifica o
próximo ataque básico dentro da Ação de Ataque. Uma manobra de **Ação Completa**
consome essa ação. Os modificadores seguem a seção anterior.

| Manobra | Ação e duração | Efeito |
| --- | --- | --- |
| Tiro Livre | Ação de Ataque; instantânea | Vantagem de Acerto II. |
| Ataque Completo | Ação de Ataque; instantânea | Vantagem de Acerto I; Desvantagem de Evasão I; Penalidade de Evasão `-1`. |
| Ataque Parcial | Ação de Ataque; instantânea | Desvantagem de Acerto I. |
| Ataque Cauteloso | Ação de Ataque; instantânea | Desvantagem de Acerto I; Vantagem de Evasão I. |
| Ataque Auxiliar | Ação de Ataque; instantânea | Usuário: Desvantagem de Acerto III. Alvo: Vantagem de Evasão II. |
| Defesa Total | Ação Completa; 1 turno | Desvantagem de Acerto III; Vantagem de Evasão II; Bônus de Evasão `+2`. |
| Cobrir Aliado | Ação de Ataque; instantânea | Usuário: Desvantagem de Acerto I. Alvo: Vantagem de Evasão I. |
| Cobertura Total de Aliado | Ação de Ataque; instantânea | Usuário: Desvantagem de Acerto III. Alvo: Vantagem de Evasão II. |

Exemplo: com Ataque Cauteloso, a criatura rola um dado a menos para Acerto e
um dado a mais para Evasão. Com Defesa Total, também aplica Bônus de Evasão
`+2` ao resultado de Evasão: o Bônus soma dois ao bônus estático de Evasão;
não cria dados extras.

## 7. Evasão rolada pelo defensor

Evasão resolve um ataque básico recebido por uma criatura controlada por
jogador. O atacante fornece valores estáticos; o defensor realiza todos os
d20 da resolução.

### Procedimento

1. obtenha a arma e a especialidade ofensiva do atacante;
2. calcule quantos dados o atacante rolaria em um ataque básico;
3. calcule a Dificuldade de Evasão;
4. o defensor rola essa quantidade de d20 de Evasão;
5. some o bônus de Evasão a cada dado;
6. transforme resultados que falharam em excessos;
7. agrupe excessos pela dificuldade da empunhadura;
8. aplique dano, bloqueio e vitalidades para cada acerto.

```text
dados-base de Evasão = total da especialidade ofensiva do atacante

Dificuldade de Evasão = 10 + bônus ofensivo do atacante

bônus de Evasão = total da especialidade defensiva
  + bônus da armadura
  + nível do peitoral / 2
  + Bônus de Evasão

resultado de Evasão = d20 + bônus de Evasão
```

O peitoral é o item que fornece o bônus de nível de Evasão. A armadura não
fornece Sorte própria. No Land of Heroes, a especialidade defensiva é
Evasão.

Um resultado de Evasão maior que a Dificuldade evita uma tentativa. O empate
favorece o atacante, conta para formar um acerto e gera excesso `0`. Um resultado
menor gera:

```text
excesso = Dificuldade de Evasão − resultado de Evasão
```

Quando Vantagem cria dados extras na Evasão, o defensor conserva somente os
melhores resultados até completar a quantidade-base; resultado alto permanece
favorável. Sorte e Azar também seguem a regra geral, inclusive a matriz entre
arma e armadura. Desvantagem e Penalidade não são consultadas pelo fluxo atual.

### Exemplo: Evasão contra arma média

Um inimigo usa arma média e possui total ofensivo `4`. Seus bônus de arma e de
efeito somam `3`, portanto seu bônus ofensivo total é `7`. O defensor rola
quatro d20 de Evasão contra Dificuldade `17`.

Uma personagem com Agilidade `3`, Evasão `2` e armadura leve possui bônus de
Evasão `7`: `5` da especialidade e `+2` da armadura. Ela rola `20`, `15`,
`12` e `8`, obtendo `27`, `22`, `19` e `15`.

Os três primeiros resultados evitam tentativas. O quarto gera excesso `2`. A
empunhadura média de uma mão exige dois excessos para formar um acerto, então o
ataque termina sem dano. Se duas falhas produzirem excessos `5` e `2`, elas
formam um acerto com excesso total `7` antes de aplicar bônus de dano e
bloqueio.

## 8. Ataque especial

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
ataque especial contra Evasão `17` e recebe bônus `5` em cada dado. Seus
resultados brutos são `12`, `9`, `7`, `4` e `2`; os totais são `17`, `14`,
`12`, `9` e `7`.

O resultado `17` gera um sucesso e satisfaz a Dificuldade `1`. A magia aplica
o efeito de medo definido em sua própria regra.

## 9. Área de ameaça e ataque de oportunidade

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
resolução forma ao menos um acerto.

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
ela provoca o ataque. Se a combatente formar ao menos um acerto, o movimento da
exploradora termina; caso contrário, ela continua o movimento.

Se uma criatura já adjacente à combatente atacar outro inimigo ao seu lado, sem
se deslocar, não há ataque de oportunidade.

## 10. Vitalidades, desgaste e condições

O ataque básico e a Evasão aplicam cada acerto na ordem de vitalidades da
campanha. Quando uma vitalidade chega a zero, o dano restante segue para a
próxima vitalidade da ordem.

No Land of Heroes, a ordem padrão é:

1. Moral;
2. Vida.

Um acerto que causa `20` de dano contra uma criatura com Moral `6` e Vida `14`
reduz Moral a `0`, transfere `14` de dano para Vida e deixa Vida em `0`. As
condições de Moral e Vida são atualizadas pelos seus limites.

## 11. Sequência de jogo

Em uma cena, use esta sequência:

1. descreva a intenção da criatura;
2. escolha a especialidade, a arma ou a regra que representa a ação;
3. defina Defesa, Complexidade e Dificuldade quando a regra pedir;
4. realize o teste correspondente;
5. transforme sucessos, excessos e acertos no efeito da ação;
6. atualize dano, vitalidades e condições;
7. registre a consequência na cena e continue a ficção.

O sistema base organiza a resolução. A campanha usa essa estrutura para criar
suas próprias criaturas, perigos, poderes, itens e histórias.

## 12. Ações da criatura

No início de cada turno próprio, uma criatura recupera uma **Ação de
Movimento**, uma **Ação de Ataque** e uma **Ação Imediata**. Usar uma dessas
ações não consome as outras.

Uma Ação Imediata somente pode ser usada quando uma regra apresenta seu
gatilho. A criatura pode usar no máximo uma Ação Imediata por turno, exceto
quando uma regra aumentar esse limite.

Uma regra que concede Ação Imediata deve identificar um destes tipos:

- **Ação Imediata de Interrupção:** resolve antes da ação que a causou. A ação
  causadora continua ou não conforme o efeito da Interrupção.
- **Ação Imediata de Reação:** resolve depois que a ação que a causou termina.
  Ela não altera uma consequência já resolvida, salvo quando sua própria regra
  declarar isso.

Esta regra não classifica automaticamente poderes, manobras ou o ataque de
oportunidade existentes como Reação ou Interrupção. Cada regra existente mantém
seu funcionamento atual até receber classificação explícita em uma mudança
posterior.
