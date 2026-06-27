# Rolls

## Objetivo da secao

Documentar como um teste e resolvido hoje e quais sao os elementos que definem sucesso, fracasso e intensidade do resultado.

## Implementado hoje

- Um teste nasce de um valor-base, que vira quantidade de dados.
- Cada dado tenta alcançar uma barra chamada `Complexity`.
- Cada dado que alcança essa barra conta como um sucesso.
- O teste completo passa quando o total de sucessos alcanca a `Difficulty` exigida.
- Quando o total de sucessos passa da dificuldade minima, o sistema ainda conta quantos blocos completos de sucesso foram formados.
- `Advantage` aumenta a quantidade de dados.
- `Luck` positiva melhora os menores dados; `Luck` negativa piora os maiores.
- O sistema marca criticos a partir de `20` e `1` naturais, mas eles continuam presos a essa logica geral de sucesso e fracasso.

## Assumido por testes/balance

- Os testes procuram combinacoes de `Complexity` e `Difficulty` proximas de 50 porcento por nivel.
- Os testes tambem verificam se sorte, azar e vantagem empurram a chance na direcao esperada.
- A base de dados usada nesses testes nao coincide com a ficha inicial real do sistema.

## Divergencias e observacoes

- A regra de `Luck` descrita em alguns docs de balance e mais contextual do que a regra de `Luck` realmente usada hoje.
- Em varios estudos de calibracao, a pericia vira uma pool abstrata de dados, e nao a leitura completa da ficha real.

## Fontes

- `Rolls/Entities/Roll.cs:49-121`
- `UnitTests/Rolls/SkillAndAttributeRollTests.cs:18-310`
- `UnitTests/README.md:50-81`

## Resumo do pipeline real

1. escolher a capacidade usada
2. transformar essa capacidade em dados
3. rolar os dados
4. aplicar sorte, azar e vantagem
5. contar sucessos contra a `Complexity`
6. comparar o total com a `Difficulty`

## Leitura pratica

- `Complexity` responde: quanto cada dado precisa atingir.
- `Difficulty` responde: quantos sucessos o teste inteiro precisa ter.
- O numero de blocos completos de sucesso importa bastante em combate, porque ele ajuda a transformar sucesso bruto em hits.
