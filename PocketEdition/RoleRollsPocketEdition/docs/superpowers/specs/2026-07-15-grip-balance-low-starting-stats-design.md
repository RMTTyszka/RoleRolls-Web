# Relatorio comparativo de dano por grip

## Objetivo

Permitir a analise visual do impacto de valores iniciais diferentes no dano dos
grips ofensivos. O primeiro cenario compara uma criatura com `2` pontos no
atributo ofensivo e `1` ponto na especializacao ofensiva contra a referencia
atual, de atributo `3` e especializacao `1`.

## Escopo

- Manter intacto o teste de balance existente para o perfil de referencia.
- Adicionar um teste de diagnostico configuravel por pontos iniciais de
  atributo e especializacao; o primeiro caso sera `2/1`.
- Limitar o diagnostico aos grips padrao: arma leve em uma mao, arma media em
  uma mao e arma pesada em duas maos.
- Para cada nivel, grip padrao e tipo de armadura, comparar o dano medio do
  cenario configurado com o dano medio da referencia `3/1`.
- Usar os tres tipos de armadura, niveis, amostras e sementes deterministicas
  da matriz existente.
- Registrar no output o dano da referencia, o dano do cenario, a diferenca
  absoluta e a diferenca percentual de cada grip em cada nivel.

## Desenho

Extrair a montagem de atacante e a execucao da matriz para receber os pontos
iniciais de atributo e especializacao, bem como a lista de grips a simular. O
teste diagnostico sera um `[Theory]`: o perfil analisado vira `InlineData`,
para que novos cenarios, como atributo `3`, possam ser incluidos sem mudar a
logica.

Para cada nivel, o teste executara duas matrizes independentes com a mesma
semente: a referencia `3/1` e o perfil configurado. A diferenca sera calculada
e exibida para cada combinacao de grip e armadura.

A progressao continuara a ser calculada a partir de cada perfil:

- atributo: `+1` nos niveis `6`, `11` e `16`;
- especializacao: `+1` nos niveis `4`, `8` e `12`.

O output identificara o perfil executado e exibira uma tabela textual por
nivel. O teste nao tera assercoes de balance ou de dano: ele passa quando a
simulacao e o relatorio forem executados sem erro.

## Fora de escopo

- Alterar regras de combate, atributos, especializacoes ou grips.
- Mudar os limiares de balance existentes.
- Criar uma regra de aprovacao para a diferenca de dano entre perfis.
