# Ações da Criatura e Ações Imediatas

## Objetivo

Adicionar ao livro de regras a economia básica de ações por turno e distinguir
os dois momentos de uma Ação Imediata: Reação e Interrupção.

## Escopo

O livro receberá uma nova seção `12. Ações da criatura`, após `11. Sequência de
jogo`. A mudança é editorial; não altera poderes, manobras, resolvedores ou
contratos da API.

## Economia de ações

No início de cada turno próprio, uma criatura recupera:

- uma **Ação de Movimento**;
- uma **Ação de Ataque**;
- uma **Ação Imediata**.

Usar uma dessas ações não consome as outras. Uma Ação Imediata somente pode ser
usada quando uma regra apresenta seu gatilho. A criatura pode usar no máximo
uma Ação Imediata por turno, exceto quando uma regra aumentar esse limite.

## Momentos da Ação Imediata

Uma regra que concede Ação Imediata deve identificar um destes tipos:

- **Ação Imediata de Interrupção:** resolve antes da ação que a causou. A ação
  causadora continua ou não conforme o efeito da Interrupção.
- **Ação Imediata de Reação:** resolve depois que a ação que a causou termina.
  Ela não altera uma consequência já resolvida, salvo quando sua própria regra
  declarar isso.

Esses nomes preservam a distinção do livro antigo: Interrupção ocorre antes do
gatilho; Reação ocorre depois dele.

## Limites e compatibilidade

Esta regra não classifica automaticamente poderes, manobras ou o ataque de
oportunidade existentes como Reação ou Interrupção. Cada regra existente mantém
seu funcionamento atual até receber classificação explícita em uma mudança
posterior.

## Validação

A revisão editorial deve confirmar que a nova seção contém:

1. as três ações recuperadas no início do turno;
2. o limite de uma Ação Imediata por turno;
3. Reação como efeito posterior ao gatilho;
4. Interrupção como efeito anterior ao gatilho;
5. ausência de alteração em regras já existentes.
