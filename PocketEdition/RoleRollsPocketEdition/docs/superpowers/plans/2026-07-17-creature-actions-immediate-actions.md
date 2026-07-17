# Creature Actions and Immediate Actions Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add player-facing turn actions and immediate-action timing to the rulebook.

**Architecture:** Append one self-contained section after the current game sequence. The section defines the three actions recovered each own turn and uses Immediate Action as the common category for Reaction and Interruption. No application code changes.

**Tech Stack:** Markdown, Git, PowerShell.

---

## File structure

- Modify: `docs/rolerolls-regras-de-negocio.md` — append section 12 with action economy and timing rules.
- Create: `docs/superpowers/plans/2026-07-17-creature-actions-immediate-actions.md` — this implementation plan.

### Task 1: Add creature action economy

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md:440-452`
- Test: `docs/rolerolls-regras-de-negocio.md` through exact-text searches

- [ ] **Step 1: Append section 12 after the existing final paragraph**

Keep every existing line in section 11. Add the following Markdown after its
last paragraph.

```markdown
## 12. Ações da criatura

No início de cada turno próprio, uma criatura recupera uma **Ação de
Movimento**, uma **Ação de Ataque** e uma **Ação Imediata**. Usar uma dessas
ações não consome as outras.

Uma Ação Imediata somente pode ser usada quando uma regra apresenta seu
gatilho. A criatura pode usar no máximo uma Ação Imediata por turno, exceto
quando uma regra aumentar esse limite.

Uma regra que concede Ação Imediata deve identificar um destes tipos:

- **Ação Imediata de Interrupção:** resolve antes da ação que a causou. A ação
  causadora continua ou não conforme o efeito da Interrupção.
- **Ação Imediata de Reação:** resolve depois que a ação que a causou termina.
  Ela não altera uma consequência já resolvida, salvo quando sua própria regra
  declarar isso.

Esta regra não classifica automaticamente poderes, manobras ou o ataque de
oportunidade existentes como Reação ou Interrupção. Cada regra existente mantém
seu funcionamento atual até receber classificação explícita em uma mudança
posterior.
```

- [ ] **Step 2: Confirm all required mechanics occur once**

Run: `rg -n "^## 12\. Ações da criatura$|Ação de Movimento|Ação de Ataque|Ação Imediata|Ação Imediata de Interrupção|Ação Imediata de Reação|no máximo uma Ação Imediata por turno" docs/rolerolls-regras-de-negocio.md`

Expected: section 12 and all three action names occur; the two immediate-action types and the one-per-turn limit are present.

### Task 2: Review documentation scope

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md`
- Test: Markdown and Git scope checks

- [ ] **Step 1: Inspect the section and existing document ending**

Run: `Get-Content docs/rolerolls-regras-de-negocio.md | Select-Object -Skip 438 -First 48`

Expected: section 11 remains intact, section 12 follows it, and section 12 ends with the compatibility statement.

- [ ] **Step 2: Check Markdown whitespace and changed files**

Run: `git diff --check; git status --short; git diff -- docs/rolerolls-regras-de-negocio.md`

Expected: no whitespace errors. The pre-existing manobra-table edit remains separate from the new section-12 hunk; no C# source or test file is modified.

### Task 3: Record only the new section

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md`
- Test: staged Git diff

- [ ] **Step 1: Stage only section 12**

The rulebook already contains a separate, unstaged manobra-table edit. Run
`git add -p docs/rolerolls-regras-de-negocio.md`; answer `n` for the manobra
table hunk and `y` for the section-12 hunk. Do not stage the existing table
edit.

- [ ] **Step 2: Verify staged scope**

Run: `git diff --cached --check; git diff --cached --name-only; git diff --cached -- docs/rolerolls-regras-de-negocio.md`

Expected: the staged diff contains only the appended section 12; the unstaged
diff still contains the manobra-table edit.

- [ ] **Step 3: Commit the section**

Run: `git commit -m "docs: define creature immediate actions"`

Expected: one commit records only the new action-economy section.
