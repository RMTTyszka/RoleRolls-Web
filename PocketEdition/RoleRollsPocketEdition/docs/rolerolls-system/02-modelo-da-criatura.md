# Modelo Da Criatura

## Objetivo da secao

Descrever quais partes formam uma criatura no sistema e que papel cada parte cumpre na regra.

## Implementado hoje

- Uma criatura combina capacidades amplas, especializacoes, defesas, vitalidades, equipamento e bonus.
- Os atributos representam aptidoes amplas.
- As pericias gerais agrupam campos maiores de acao.
- As especializacoes representam recortes mais concretos, como um tipo de arma ou uma forma especifica de resistencia.
- As defesas definem o alvo numerico que um adversario precisa superar.
- As vitalidades representam os reservatorios que absorvem desgaste e dano.
- O equipamento altera acerto, defesa, block e contexto do combate.
- As condicoes visiveis aparecem a partir do estado das vitalidades.

## Assumido por testes/balance

- Os testes de combate usam uma criatura-base mais forte do que a ficha inicial real.
- Esse baseline de teste parte de atributos mais altos, especializacoes ja distribuidas e equipamento medio.
- Esse ponto de partida existe para facilitar simulacao e comparacao entre armas, armaduras e niveis.

## Divergencias e observacoes

- A criatura-base dos testes nao deve ser confundida com a criatura inicial real do sistema.
- Para ler a regra de negocio corretamente, e preciso distinguir ficha inicial, ficha de simulacao e ficha de balance.

## Fontes

- `Creatures/Entities/Creature.cs:27-72`
- `Creatures/Entities/Creature.cs:81-112`
- `Creatures/Entities/Attribute.cs:7-35`
- `Creatures/Entities/Skill.cs:7-55`
- `Creatures/Entities/SpecificSkill.cs:6-37`
- `Creatures/Entities/Vitality.cs:8-44`
- `UnitTests/Core/BaseCreature.cs:15-53`

## Componentes

- atributos
- pericias gerais
- especializacoes
- defesas
- vitalidades
- equipamento
- inventario
- bonus
- condicoes observaveis
