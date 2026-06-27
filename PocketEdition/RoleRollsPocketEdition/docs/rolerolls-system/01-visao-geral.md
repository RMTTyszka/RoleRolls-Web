# Visao Geral

## Objetivo da secao

Explicar o que e o sistema base `RoleRolls`, quais sao seus limites e qual e o fluxo geral que liga ficha, teste, acerto, dano, desgaste e condicao.

## Implementado hoje

- O sistema gira em torno de uma criatura com capacidades, defesas, vitalidades e equipamento.
- Toda acao relevante comeca escolhendo qual capacidade sera usada.
- Essa capacidade vira uma rolagem com dados, bonus, complexidade e dificuldade.
- Em combate, os sucessos da rolagem viram hits e os hits viram dano.
- O dano desgasta vitalidades em ordem definida e esse desgaste pode expor condicoes.

## Assumido por testes/balance

- Os testes atuais leem o sistema principalmente por tres portas: rolagens, combate e balance de arma x armadura.
- A documentacao de testes existente resume bem o que ja esta congelado por contrato numerico e por tendencia.
- Parte do material de balance nao tenta descrever a regra inteira do sistema; tenta calibrar desempenho medio.

## Divergencias e observacoes

- O recorte dos testes nao cobre toda a regra viva do sistema.
- Parte do balance usa modelos abstratos, nao a mesma resolucao completa usada pelo jogo em producao.
- O tema de esquiva hoje esta dividido entre a regra principal de ataque e um fluxo paralelo muito usado pelos testes.

## Fontes

- `Creatures/Entities/Creature.cs:81-112`
- `Creatures/Entities/Creature.cs:240-314`
- `Rolls/Entities/Roll.cs:49-121`
- `Attacks/Services/AttackService.cs:38-87`
- `UnitTests/README.md:6-30`

## Mapa do fluxo

1. a criatura parte de uma ficha base
2. o sistema escolhe a capacidade usada na acao
3. essa capacidade vira teste
4. o teste produz sucessos
5. os sucessos viram efeito: acerto, protecao, desgaste ou dano
6. o desgaste atualiza vitalidades e condicoes
