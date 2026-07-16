# Combat Maneuvers and Roll Modifiers Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Document roll modifiers and Land of Heroes standard combat maneuvers in player-facing language.

**Architecture:** Change only the business-rule book. Centralize common modifier semantics in Tests, then reference them from Evasion. Insert maneuvers between Basic Attack and rolled Evasion, preserving existing rules while renumbering later sections.

**Tech Stack:** Markdown, Git, PowerShell.

---

## File structure

- Modify: `docs/rolerolls-regras-de-negocio.md` — player rulebook; receives modifier rules, maneuver table, cross-reference, and section renumbering.
- Create: `docs/superpowers/plans/2026-07-16-combat-maneuvers-roll-modifiers.md` — this execution plan.

### Task 1: Define common roll modifiers

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md:171-178`
- Test: `docs/rolerolls-regras-de-negocio.md` through exact-text searches

- [ ] **Step 1: Replace the short modifier section**

Replace `### Vantagem e Sorte` with `### Modificadores de rolagem`. State these exact player rules: Vantagem `+N` rolls `N` extra dice; Desvantagem `+N` rolls `N` fewer dice, never below zero; both cancel before rolling; Sorte `+N` rerolls the `N` lowest results and keeps the higher result; Azar `-N` rerolls the `N` highest results and keeps the lower result; Buff `+N` adds `N` static value; Debuff `+N` subtracts `N` static value. State that Acerto is offensive, Evasion is defensive, property is ability-specific, and Buffs/Debuffs for same application sum.

```markdown
### Modificadores de rolagem

Cada modificador possui magnitude `N`. Vantagem `+N` adiciona `N` dados à
rolagem; Desvantagem `+N` remove `N` dados, até o mínimo de `0`. Vantagem e
Desvantagem se cancelam antes da rolagem.

Sorte `+N` permite rolar novamente os `N` menores resultados e conservar o
maior resultado de cada nova rolagem. Azar `-N` faz o oposto: rola novamente os
`N` maiores resultados e conserva o menor resultado de cada nova rolagem.

Buff `+N` soma `N` ao valor estático da aplicação indicada. Debuff `+N`
subtrai `N` desse valor. Buffs e Debuffs da mesma aplicação são somados.

As aplicações são: **Acerto**, para rolagens ofensivas; **Evasion**, para
rolagens defensivas; e **propriedade**, quando uma habilidade identifica outra
rolagem.
```

- [ ] **Step 2: Preserve critical-result rules below modifiers**

Keep the existing critical-success and critical-failure paragraphs immediately after the new modifier rules. Do not change their mechanical thresholds.

- [ ] **Step 3: Update rolled Evasion to reference common rules**

Replace the Evasion-only modifier paragraph with this text, preserving the adjacent excess and damage rules.

```markdown
Vantagem, Desvantagem, Sorte, Azar, Buffs e Debuffs seguem a regra geral de
modificadores. Quando Vantagem cria dados extras na Evasion, o defensor
conserva somente os melhores resultados até completar a quantidade-base;
resultado alto permanece favorável.
```

- [ ] **Step 4: Verify modifier wording**

Run: `rg -n "Modificadores de rolagem|Desvantagem|Azar|Buff|Debuff|quantidade-base" docs/rolerolls-regras-de-negocio.md`

Expected: one common modifier heading, all six modifier names, and the Evasion selection rule.

### Task 2: Add standard combat maneuvers

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md:265-343`
- Test: `docs/rolerolls-regras-de-negocio.md` through exact-text searches

- [ ] **Step 1: Insert the new section before rolled Evasion**

Insert `## 6. Manobras de combate` after the basic-attack example and before rolled Evasion. Include the exact table below. `Ataque Parcial`, `Ataque Cauteloso`, and `Defesa Total` name the Portuguese player-facing maneuvers; parenthetical code names preserve template traceability.

```markdown
## 6. Manobras de combate

Manobras são escolhas de combate. Uma manobra **instantânea** modifica o
próximo ataque básico dentro da Ação de Ataque. Uma manobra de **Ação Completa**
consome essa ação. Os modificadores seguem a seção anterior.

| Manobra | Ação e duração | Alvos | Efeito |
| --- | --- | --- | --- |
| Tiro Aberto (Open Shot) | Ação de Ataque; instantânea | Usuário | Vantagem Acerto `+2`. |
| Ataque Completo (Full Attack) | Ação de Ataque; instantânea | Usuário | Vantagem Acerto `+1`; Desvantagem Evasion `+1`; Debuff Evasion `+1`. |
| Ataque Parcial (Partial Attack) | Ação de Ataque; instantânea | Usuário | Desvantagem Acerto `+1`. |
| Ataque Cauteloso (Cautious Attack) | Ação de Ataque; instantânea | Usuário | Desvantagem Acerto `+1`; Vantagem Evasion `+1`. |
| Ataque Auxiliar (Auxiliar Attack) | Ação de Ataque; instantânea | Usuário e alvo | Usuário: Desvantagem Acerto `+3`. Alvo: Vantagem Evasion `+2`. |
| Defesa Total (Full Defense) | Ação Completa; 1 turno | Usuário | Desvantagem Acerto `+3`; Vantagem Evasion `+2`; Buff Evasion `+2`. |
| Cobrir Aliado (Cover Ally) | Ação de Ataque; instantânea | Usuário e alvo | Usuário: Desvantagem Acerto `+1`. Alvo: Vantagem Evasion `+1`. |
| Cobertura Total de Aliado (Full Cover Ally) | Ação de Ataque; instantânea | Usuário e alvo | Usuário: Desvantagem Acerto `+3`. Alvo: Vantagem Evasion `+2`. |
```

- [ ] **Step 2: Add player examples beneath table**

Add this text after the table.

```markdown
Exemplo: com Ataque Cauteloso, a criatura rola um dado a menos para Acerto e
um dado a mais para Evasion. Com Defesa Total, também soma `+2` ao resultado de
Evasion: o Buff soma dois ao bônus estático de Evasion; não cria dados extras.
```

- [ ] **Step 3: Renumber following top-level sections**

Rename the existing headings exactly as follows, without altering their content: `## 6. Evasion rolada pelo defensor` to `## 7. Evasion rolada pelo defensor`; `## 7. Ataque especial` to `## 8. Ataque especial`; `## 8. Área de ameaça e ataque de oportunidade` to `## 9. Área de ameaça e ataque de oportunidade`; `## 9. Vitalidades, desgaste e condições` to `## 10. Vitalidades, desgaste e condições`; `## 10. Sequência de jogo` to `## 11. Sequência de jogo`.

- [ ] **Step 4: Verify maneuvers and heading order**

Run: `rg -n "^## [6-9]\\.|Open Shot|Full Attack|Partial Attack|Cautious Attack|Auxiliar Attack|Full Defense|Cover Ally|Full Cover Ally" docs/rolerolls-regras-de-negocio.md`

Expected: section 6 is Maneuvers, all eight template names occur once in the table, and rolled Evasion is section 7.

### Task 3: Review and record documentation change

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md`
- Create: `docs/superpowers/plans/2026-07-16-combat-maneuvers-roll-modifiers.md`
- Test: Markdown structure and Git diff checks

- [ ] **Step 1: Inspect intended diff**

Run: `git diff --check; git diff -- docs/rolerolls-regras-de-negocio.md`

Expected: no whitespace errors; diff contains only modifier rules, maneuver section, Evasion cross-reference, and renumbered headings.

- [ ] **Step 2: Confirm scope is documentation only**

Run: `git status --short; git diff --name-only`

Expected: only `docs/rolerolls-regras-de-negocio.md` is changed by implementation. No C# source, template, or test file changes.

- [ ] **Step 3: Commit book update**

Run: `git add docs/rolerolls-regras-de-negocio.md && git diff --cached --check && git commit -m "docs: add combat maneuvers and modifiers"`

Expected: one commit containing only the rulebook update.
