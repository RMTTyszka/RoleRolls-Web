# Balance de grips com atributos iniciais menores

## Objetivo

Cobrir o balance dos grips ofensivos quando a criatura inicia com `2` pontos no
atributo ofensivo e `1` ponto na especializacao ofensiva.

## Escopo

- Manter o teste atual para o perfil de referencia: atributo `3` e
  especializacao `1`.
- Adicionar um segundo teste que execute a mesma matriz para o perfil reduzido:
  atributo `2` e especializacao `1`.
- Cobrir os mesmos grips ofensivos, armaduras, niveis, numero de amostras e
  sementes deterministicas do teste existente.
- Manter as mesmas verificacoes de balance: superioridade da arma pesada de
  duas maos sobre a de uma mao por nivel, viabilidade agregada, ordem de dano
  entre grips e superioridade do dual wield sobre a arma equivalente unica.

## Desenho

Extrair o corpo de `GripTypeLevelScalingKeepsBalance` para um avaliador privado
parametrizado pelos valores iniciais de atributo e especializacao. Os testes
publicos chamarao esse avaliador com os perfis `3/1` e `2/1`.

A progressao continuara a ser calculada a partir do perfil recebido:

- atributo: `+1` nos niveis `6`, `11` e `16`;
- especializacao: `+1` nos niveis `4`, `8` e `12`.

Os logs identificarao o perfil executado. O novo teste compara os grips entre
si dentro do perfil `2/1`; ele nao exige que seus danos absolutos sejam maiores
ou iguais aos do perfil `3/1`.

## Fora de escopo

- Alterar regras de combate, atributos, especializacoes ou grips.
- Mudar os limiares de balance existentes.
- Comparar dano absoluto entre os dois perfis de atributos.
