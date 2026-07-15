# Divergencias E Pendencias

## Objetivo da secao

Centralizar conflitos entre runtime, testes e docs auxiliares para facilitar futuras revisoes de regra do sistema base.

## Implementado hoje

- O runtime real tem um conjunto proprio de regras para propriedade, roll, combate, vitalidade e condicao.
- Em varios pontos, esse comportamento nao coincide exatamente com os modelos usados pelos testes de balance e pelos docs auxiliares.

## Assumido por testes/balance

- Os testes frequentemente assumem progressao, sorte, baseline de skill e matrizes numericas que simplificam ou reinterpretam o runtime.

## Divergencias e observacoes

- Esta secao existe justamente porque o estado atual do sistema nao cabe em uma unica camada de verdade.

## Fontes

- `UnitTests/README.md:32-225`
- `UnitTests/Core/BaseCreature.cs:15-113`
- `Creatures/Entities/CreatureAttack.cs:18-184`
- `Creatures/Entities/CreatureDefend.cs:25-125`
- `Attacks/Services/AttackService.cs:38-87`
- `Rolls/Entities/Roll.cs:49-121`

## Lista inicial de divergencias

- baseline de progressao dos testes diferente do estado inicial real da criatura
- `Luck` do runtime mais simples que `Luck` dos modelos abstratos
- `Attack` atual usa excessos acima da defesa agrupados por dificuldade da arma
- testes antigos ainda esperam ataques com dano zero em cenarios onde o runtime atual tem piso minimo de dano
- `Mana` nao faz parte da ordem padrao de dano basico do template base
- ha parametros expostos em `AttackCommand` que nao participam da resolucao principal

## Pendencias abertas

- revisar o modelo de dano
- decidir se os testes antigos devem virar historia do sistema ou contrato vivo
- decidir se o resolver de `Skill` precisa refletir melhor a intuicao de atributo + pontos de skill

## Leitura pratica das pendencias

- Se a discussao for sobre dano, a primeira leitura e `06-combate.md`.
- Se a discussao for sobre esquiva, a primeira leitura e `07-evade-e-defesas.md`.
- Se a discussao for sobre progressao e pools de dados, a primeira leitura e `03-progressao.md` e `05-rolls.md`.
