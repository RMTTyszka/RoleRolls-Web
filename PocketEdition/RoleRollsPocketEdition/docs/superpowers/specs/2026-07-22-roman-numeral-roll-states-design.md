# Notação Romana para Estados de Rolagem — Design

## Objetivo

Padronizar o livro de regras para apresentar a intensidade dos estados de
rolagem em algarismos romanos, sem alterar a notação aritmética dos
modificadores numéricos.

## Convenção editorial

- Estados de rolagem — **Vantagem**, **Desvantagem**, **Sorte** e **Azar** —
  expressam seu grau sem sinal: `Vantagem I`, `Desvantagem III`, `Sorte II` e
  `Azar I`.
- O nome do estado determina sua direção; o grau romano determina a quantidade
  de dados adicionados, removidos ou rerrolados conforme a regra já existente.
- Modificadores que alteram diretamente o valor de um dado ou de uma
  propriedade mantêm sinal e algarismos arábicos, como `+2` e `-1`. Isso inclui
  **Buff**, **Debuff**, bônus de hit de armas e bônus de Evasion de armaduras.
- Fórmulas e demais valores matemáticos não são alterados.

## Alterações no livro

Modificar somente `docs/rolerolls-regras-de-negocio.md`:

1. Reescrever a regra geral de modificadores para explicar o grau romano dos
   quatro estados, preservando a regra numérica de Buffs e Debuffs.
2. Converter os estados na tabela de manobras para a forma sem sinal e com
   grau romano.
3. Ajustar o exemplo de manobras para referenciar os estados convertidos, sem
   alterar o Buff numérico de Evasion.

As tabelas de arma e armadura, os cálculos e os exemplos de bônus estáticos
permanecem inalterados por serem modificadores numéricos, não estados.

## Verificação

- Conferir que cada ocorrência de Vantagem, Desvantagem, Sorte ou Azar que
  informe magnitude use um algarismo romano e não contenha `+` ou `-`.
- Conferir que Buff, Debuff e bônus estáticos preservem sinais e magnitudes
  arábicas.
- Confirmar que não houve mudança em fórmulas, valores de balanceamento ou
  comportamento do sistema.
