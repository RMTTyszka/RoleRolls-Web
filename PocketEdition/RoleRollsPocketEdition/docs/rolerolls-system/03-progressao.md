# Progressao

## Objetivo da secao

Registrar como a progressao funciona de fato hoje e como os testes usam uma progressao propria para balance e calibracao.

## Implementado hoje

- A ficha inicial real parte do nivel `1`.
- Cada atributo comeca em `1`.
- Cada especializacao comeca em `0`.
- O sistema atual sobe nivel, mas nao distribui automaticamente os pontos da mesma forma que os testes de balance distribuem.
- Existem limites de pontos por nivel, entao a progressao real hoje e mais uma progressao de teto do que uma progressao automatica de ganho.

## Assumido por testes/balance

- A progressao de simulacao dos testes distribui pontos manualmente ao longo dos niveis.
- Os atributos sobem em `6`, `11` e `16`.
- As pericias gerais e as especializacoes sobem em `4`, `8` e `12`.
- Essa progressao e parte importante das curvas de chance, dano e viabilidade estudadas nos testes.

## Divergencias e observacoes

- Hoje existem duas leituras de progressao: a progressao viva do sistema e a progressao usada para balance.
- Isso significa que qualquer conversa sobre nivel precisa dizer de qual das duas esta falando.

## Fontes

- `Creatures/Entities/Creature.cs:63-72`
- `Creatures/Entities/Creature.cs:688-716`
- `Creatures/Entities/Attribute.cs:23-30`
- `Creatures/Entities/Skill.cs:15-16`
- `UnitTests/Core/BaseCreature.cs:81-113`

## Limites observados hoje

- teto por especializacao: `3 + Level - 1`
- piso por especializacao: `0`
- teto por atributo: `4 + Level / 6`
- limite interno da pericia geral: `3 + numero de especializacoes - 1`
