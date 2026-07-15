# Evasion Rolada pelo Defensor

## Objetivo

Transformar `Evade` em uma ação defensiva explícita. Quando uma criatura
controlada por jogador recebe um ataque básico, o jogador rola todos os dados
da defesa. O atacante contribui com valores estáticos derivados de sua ficha,
arma, equipamento e bônus.

O desenho preserva a lógica central de combate: a categoria da arma agrupa
resultados em hits, o excesso alimenta o dano, o bloqueio reduz esse dano e as
vitalidades recebem o desgaste na ordem da campanha.

## Regra de jogo

### Valores do atacante

O atacante determina a quantidade-base de dados de Evasion e a dificuldade de
cada dado.

```text
dados-base de Evasion = total da especialidade ofensiva do atacante

dificuldade de Evasion = 10 + bônus ofensivo do atacante
```

O bônus ofensivo usa os mesmos componentes do ataque básico:

```text
total da especialidade ofensiva
+ bônus de hit do grip
+ buffs de hit
+ diferença de nível entre atacante e defensor
+ bônus de nível da arma
```

O atacante não rola dados durante a resolução de Evasion.

### Rolagem do defensor

O defensor rola um d20 para cada dado-base de Evasion. Cada resultado recebe o
bônus de Evasion:

```text
resultado de Evasion = d20 + bônus de Evasion
```

No Land of Heroes, o bônus de Evasion é formado por:

```text
Agility + pontos na especialidade Evasion
+ bônus de Evasion da armadura
+ bônus de nível da armadura
+ buffs de Evasion
```

Cada resultado estritamente maior que a dificuldade evita uma tentativa de
ataque. Um resultado igual à dificuldade favorece o atacante e produz excesso
zero. Um resultado menor produz:

```text
excesso = dificuldade de Evasion − resultado de Evasion
```

### Vantagem e sorte

Vantagem adiciona dados à rolagem defensiva. Depois da rolagem, o defensor
conserva apenas a quantidade-base de resultados mais altos. Dessa forma, a
Vantagem melhora a defesa sem criar tentativas ofensivas adicionais.

Sorte positiva rerrola os menores resultados e conserva os maiores; sorte
negativa rerrola os maiores resultados e conserva os menores. A direção desses
modificadores permanece igual à de todos os outros testes: resultado alto é
melhor.

### Hits, dano e desgaste

Os excessos são ordenados do maior para o menor e agrupados pela dificuldade da
arma do atacante:

- arma leve: um excesso por hit;
- arma média: dois excessos por hit;
- arma pesada: três excessos por hit.

Resultados que não completam um grupo não formam hit. Para cada hit, o dano é:

```text
máximo(
  soma dos excessos do grupo
  + bônus de dano por hit
  − bloqueio do defensor,
  1
)
```

O bônus de dano por hit e o bloqueio seguem as fórmulas atuais do ataque
básico. O dano final percorre a ordem de vitalidades configurada e atualiza as
condições expostas pelos limites dessas vitalidades.

## Exemplo: Land of Heroes

Um inimigo usa uma arma média e tem total ofensivo `4`. Seus bônus de arma e
de efeito somam `3`, portanto seu bônus ofensivo é `7`; o defensor rola quatro
dados de Evasion contra dificuldade `17`.

O personagem possui `Agility 3`, `Evasion 2` e armadura leve, que fornece `+2`
de Evasion. Seu bônus de Evasion é `7`.

Ele rola `20`, `15`, `12` e `8`:

```text
27, 22, 19 e 15 após o bônus de Evasion
```

Os três primeiros resultados evitam tentativas; o último produz excesso `2`.
Uma arma média precisa de dois excessos para formar um hit, portanto esse
ataque não causa dano.

Se os resultados que falharam produzissem excessos `5` e `2`, a arma média
formaria um hit com excesso total `7`; em seguida, o sistema aplicaria bônus
de dano, bloqueio e vitalidades.

## Arquitetura

### Configuração da campanha

Adicionar uma propriedade explícita de Evasion à configuração de itens da
campanha. Ela identifica a especialidade usada na rolagem defensiva. O Land of
Heroes a configura como a especialidade `Evasion`.

Essa configuração evita deduzir uma especialidade a partir de fórmulas de
Defesa e mantém o motor configurável por campanha.

### Contrato e serviço

Adicionar uma ação do defensor:

```text
POST /campaigns/{campaignId}/scenes/{sceneId}/creatures/{defenderId}/evades
```

`EvadeInput` contém `AttackerId`, `WeaponSlot`, `VitalityId` opcional, `Luck`
e `Advantage`. O contrato não aceita propriedade ofensiva, dano, bloqueio ou
resultado de dado fornecidos pelo cliente.

`EvadeResponse` informa atacante, defensor, arma, quantidade-base de dados,
dificuldade, bônus de Evasion, dados rolados, resultados conservados,
excessos, hits, bloqueio, dano total e vitalidades desgastadas.

Criar `IEvadeService` e `EvadeService`. O serviço carrega as duas criaturas e
a configuração da campanha, cria um comando de domínio e registra uma ação de
cena própria.

### Domínio

Substituir o fluxo paralelo atual de `CreatureDefend.Evade(...)` por um comando
e resultado próprios. A resolução de domínio:

1. lê arma, grip, especialidade ofensiva e bônus do atacante;
2. calcula a quantidade-base de dados e a dificuldade;
3. resolve a especialidade de Evasion do defensor;
4. rola e seleciona os resultados defensivos;
5. transforma falhas em excessos;
6. agrupa excessos em hits;
7. aplica dano, bloqueio e vitalidades com as mesmas regras do ataque básico.

O ataque básico e o ataque especial permanecem fluxos independentes.

## Estratégia de testes

Criar testes determinísticos antes da implementação para provar:

1. a quantidade-base de dados é igual ao total ofensivo do atacante;
2. a dificuldade inclui especialidade ofensiva, grip, buffs, diferença de
   nível e bônus de nível da arma;
3. um resultado defensivo maior evita a tentativa;
4. um empate produz excesso zero;
5. uma falha produz o excesso exato;
6. Vantagem mantém somente os melhores resultados até a quantidade-base;
7. Sorte positiva e negativa preservam a direção normal de resultados altos;
8. armas leve, média e pesada agrupam excessos em `1`, `2` e `3`;
9. bloqueio, dano mínimo e cascata de vitalidades continuam corretos;
10. a resposta e o histórico de cena expõem a resolução defensiva;
11. ataque básico e ataque especial preservam seus contratos e seus testes de
    regressão.

## Documentação

O livro de regras explicará o fluxo de ataque do jogador e o fluxo de Evasion
do jogador como procedimentos de mesa. Cada regra receberá exemplo do Land of
Heroes. A documentação técnica registrará a nova rota, os contratos e a
substituição do fluxo anterior de `Evade`.

## Fora de escopo

- rolagens de Evasion por criaturas controladas pelo mestre;
- um estado persistido de ataque pendente;
- alterações nas regras de ataque especial;
- ajustes de balanceamento após a primeira implementação.
