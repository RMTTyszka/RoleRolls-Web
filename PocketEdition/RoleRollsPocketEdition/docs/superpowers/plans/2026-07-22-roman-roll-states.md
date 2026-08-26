# Notação Romana para Estados de Rolagem Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Mostrar a intensidade de Vantagem, Desvantagem, Sorte e Azar em algarismos romanos, preservando sinais e algarismos arábicos nos modificadores numéricos.

**Architecture:** A alteração é exclusivamente editorial e fica restrita ao livro de regras. A regra geral passa a definir o grau romano dos quatro estados; a tabela de manobras passa a usar essa convenção. Buffs, Debuffs e bônus estáticos continuam numéricos e assinados, sem alteração de comportamento.

**Tech Stack:** Markdown, Git, ripgrep.

---

### Task 1: Definir o grau romano dos estados de rolagem

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md:173-182`
- Reference: `docs/superpowers/specs/2026-07-22-roman-numeral-roll-states-design.md:8-19`

- [ ] **Step 1: Substituir a descrição dos estados por uma regra de grau romano**

  Substituir os dois primeiros parágrafos da seção `### Modificadores de rolagem` por:

  ```markdown
  Cada estado de rolagem possui um grau em algarismo romano: `I` corresponde a
  um, `II` a dois e assim por diante. Vantagem adiciona à rolagem a quantidade
  de dados indicada pelo grau; Desvantagem remove essa quantidade, até o mínimo
  de `0`. Vantagem e Desvantagem se cancelam antes da rolagem.

  Sorte permite rolar novamente a quantidade de menores resultados indicada
  pelo grau e conservar o maior resultado de cada nova rolagem. Azar faz o
  oposto: rola novamente a quantidade de maiores resultados indicada pelo grau
  e conserva o menor resultado de cada nova rolagem.
  ```

- [ ] **Step 2: Manter Buff e Debuff como modificadores numéricos assinados**

  Ajustar o parágrafo seguinte para:

  ```markdown
  Buff `+N` soma `N` ao valor estático da aplicação indicada. Debuff `-N`
  subtrai `N` desse valor. Buffs e Debuffs da mesma aplicação são somados.
  ```

- [ ] **Step 3: Verificar a seção de regra geral**

  Run: `rg -n -C 2 'Cada estado de rolagem|Sorte permite|Buff `\+N`|Debuff `-N`' docs/rolerolls-regras-de-negocio.md`

  Expected: a regra declara os graus `I` e `II` para os quatro estados e usa
  `+N` e `-N` exclusivamente para Buff e Debuff.

### Task 2: Converter a tabela e o exemplo de manobras

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md:284-295`
- Reference: `docs/superpowers/specs/2026-07-22-roman-numeral-roll-states-design.md:21-33`

- [ ] **Step 1: Aplicar graus romanos aos estados na tabela**

  Atualizar as células de efeito para exatamente:

  ```markdown
  | Tiro Livre (Open Shot) | Ação de Ataque; instantânea | Vantagem Acerto II. |
  | Ataque Completo (Full Attack) | Ação de Ataque; instantânea | Vantagem Acerto I; Desvantagem Evasion I; Debuff Evasion `-1`. |
  | Ataque Parcial (Partial Attack) | Ação de Ataque; instantânea | Desvantagem Acerto I. |
  | Ataque Cauteloso (Cautious Attack) | Ação de Ataque; instantânea | Desvantagem Acerto I; Vantagem Evasion I. |
  | Ataque Auxiliar (Auxiliar Attack) | Ação de Ataque; instantânea | Usuário: Desvantagem Acerto III. Alvo: Vantagem Evasion II. |
  | Defesa Total (Full Defense) | Ação Completa; 1 turno | Desvantagem Acerto III; Vantagem Evasion II; Buff Evasion `+2`. |
  | Cobrir Aliado (Cover Ally) | Ação de Ataque; instantânea | Usuário: Desvantagem Acerto I. Alvo: Vantagem Evasion I. |
  | Cobertura Total de Aliado (Full Cover Ally) | Ação de Ataque; instantânea | Usuário: Desvantagem Acerto III. Alvo: Vantagem Evasion II. |
  ```

- [ ] **Step 2: Associar o bônus estático do exemplo ao Buff assinado**

  Substituir a frase final do exemplo por:

  ```markdown
  um dado a mais para Evasion. Com Defesa Total, também aplica Buff Evasion
  `+2` ao resultado de Evasion: o Buff soma dois ao bônus estático de Evasion;
  não cria dados extras.
  ```

- [ ] **Step 3: Verificar a tabela e o exemplo**

  Run: `rg -n 'Open Shot|Full Attack|Partial Attack|Cautious Attack|Auxiliar Attack|Full Defense|Cover Ally|Full Cover Ally|Buff Evasion' docs/rolerolls-regras-de-negocio.md`

  Expected: todos os estados da tabela usam `I`, `II` ou `III`; o Debuff é
  `-1` e o Buff permanece `+2`.

### Task 3: Fazer a validação editorial final

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md`

- [ ] **Step 1: Garantir que nenhum estado com magnitude use sinal numérico**

  Run: `rg -n -P '(Vantagem|Desvantagem|Sorte|Azar)[^\n]*`[+-][0-9N]+' docs/rolerolls-regras-de-negocio.md`

  Expected: nenhuma saída.

- [ ] **Step 2: Garantir a preservação dos modificadores numéricos**

  Run: `rg -n 'Buff `\+N`|Debuff `-N`|Buff Evasion `\+2`|Debuff Evasion `-1`|\| Leve \| 1 \| \+1 \| 3 \||\| Pesada \| -1 \|' docs/rolerolls-regras-de-negocio.md`

  Expected: ocorrências para Buff, Debuff, bônus de hit e bônus de Evasion,
  todas com sinais e algarismos arábicos.

- [ ] **Step 3: Conferir alterações acidentais e espaços em branco**

  Run: `git diff --check && git diff -- docs/rolerolls-regras-de-negocio.md`

  Expected: nenhum erro de espaço e somente as alterações editoriais previstas
  na regra geral e nas manobras.

- [ ] **Step 4: Commit**

  ```bash
  git add docs/rolerolls-regras-de-negocio.md
  git commit -m "docs: use roman numerals for roll states"
  ```

  Expected: um commit contendo somente o livro de regras; não incluir as
  alterações preexistentes em `Creatures/Entities/Creature.cs`,
  `docs/rolerolls-system/04-resolucao-de-propriedades-e-formulas.md` nem
  `UnitTests/Creatures/`.
