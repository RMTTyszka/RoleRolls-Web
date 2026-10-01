# Criação de atributos no Land of Heroes

## Objetivo

Formalizar as regras de criação de atributos e separar as regras gerais do
RoleRolls das regras próprias da campanha Land of Heroes.

## Regras do RoleRolls

- O teto de um atributo é `3 + piso(nível / 6)`.
- Portanto, o teto é `3` do nível 1 ao 5, `4` do 6 ao 11, `5` do 12 ao 17 e
  `6` do 18 ao 20.
- Na criação de uma criatura, o RoleRolls recomenda começar com `2` pontos por
  atributo. É orientação de design; não é um orçamento obrigatório do motor.

## Regras do Land of Heroes

- A campanha possui seis atributos: Agilidade, Carisma, Inteligência,
  Intuição, Força e Vigor.
- Na criação, a personagem deve distribuir exatamente `12` pontos entre esses
  seis atributos.
- Cada atributo deve receber ao menos `1` ponto e, no nível 1, no máximo `3`.
- A distribuição base recomendada é `2/2/2/2/2/2`.
- Distribuições que conservam os 12 pontos e respeitam os limites, como
  `2/3/2/2/2/1`, são válidas.

## Consequências de implementação

- O teto precisa ser validado como regra de domínio, não apenas como detalhe do
  método técnico que acrescenta um ponto.
- A criação e a atualização de uma criatura do Land of Heroes devem rejeitar
  totais diferentes de `12`, bem como valores abaixo de `1` ou acima do teto.
- `TotalAttributePoints` é o ponto de configuração da campanha que deve
  expressar o orçamento de `12` do Land of Heroes.
- O fixture `BaseCreature` usado por testes de combate continua sendo um perfil
  de teste e não define a regra de criação da campanha. Seus atributos `3`
  devem ser descritos como baseline de combate, não como ficha inicial.

## Verificação planejada

- Cobrir distribuições válidas no nível 1, incluindo `2/3/2/2/2/1`.
- Rejeitar total diferente de `12`, atributo `0` e atributo `4` no nível 1.
- Confirmar a progressão do teto nos níveis 6, 12 e 18.
- Atualizar o livro para apresentar somente essas regras de negócio e remover a
  interpretação de valores-padrão de construtores C# como regra.
