# Threat Area and Opportunity Attack Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Document the base-system threat area and opportunity-attack rule in the RoleRolls rulebook.

**Architecture:** Add one self-contained rulebook section after Special Attack. It defines the default threat area, trigger, Basic Attack resolution, per-round limit, movement interruption, exceptions, and examples. Renumber the two following sections; no engine behavior changes in this plan.

**Tech Stack:** Markdown, Git, `rg`.

---

## File structure

- Modify: `docs/rolerolls-regras-de-negocio.md:343-358` — add the new combat-movement rule before Vitalities and renumber the following headings.
- No test files — this is an editorial-only change with no executable behavior.

### Task 1: Add threat-area rulebook section

**Files:**

- Modify: `docs/rolerolls-regras-de-negocio.md:343-358`
- Test: Not applicable; validate the Markdown text and repository diff.

- [ ] **Step 1: Inspect the insertion point and current headings**

Run:

```powershell
rg -n '^## (7\. Ataque especial|8\. Vitalidades, desgaste e condições|9\. Sequência de jogo)|^### Exemplo: magia de medo' docs\rolerolls-regras-de-negocio.md
```

Expected: headings for sections 7, 8 and 9, with the threat-area section absent.

- [ ] **Step 2: Insert the new section after the Special Attack example**

Insert this Markdown immediately before `## 8. Vitalidades, desgaste e condições`:

```markdown
## 8. Área de ameaça e ataque de oportunidade

Uma criatura portando uma arma corpo a corpo ameaça um raio de `1,5 m` ao seu
redor. Uma criatura portando somente uma arma de ataque a distância não possui
área de ameaça.

Uma arma específica, como uma arma de haste, ou uma habilidade ou poder pode
aumentar essa área. A regra específica informa o novo alcance e prevalece sobre
o valor padrão.

### Gatilho

Uma criatura provoca ataque de oportunidade quando se desloca dentro da área de
ameaça de outra criatura e um trecho desse deslocamento não reduz a distância
até a criatura que ameaça. Deslocar-se lateralmente ou afastar-se satisfaz esse
gatilho.

Uma criatura pode entrar na área e continuar aproximando-se sem provocar o
ataque. Se, depois de entrar, ela mudar a direção para mover-se de lado ou para
longe, provoca o ataque nesse ponto do deslocamento.

Atacar outra criatura adjacente sem se deslocar não provoca ataque de
oportunidade.

### Resolução e limite

A criatura que ameaça pode realizar um Ataque Básico com a arma corpo a corpo
que concede a área de ameaça. Para esta regra, o ataque tem sucesso quando a
resolução forma ao menos um hit.

Cada criatura pode realizar no máximo um ataque de oportunidade por rodada.
Uma habilidade ou poder pode alterar esse limite.

Se o ataque tiver sucesso, o deslocamento que o provocou é interrompido no
local em que a criatura se encontra. Se falhar, o deslocamento continua.
Habilidades ou poderes podem interromper o deslocamento mesmo quando o ataque
falha, caso seu texto o determine.

### Exemplo: mudar direção dentro da área

Uma combatente com espada ameaça `1,5 m`. Uma exploradora entra nessa área e
segue em direção à combatente: não provoca ataque de oportunidade. Ainda dentro
da área, a exploradora muda a direção e tenta atravessar ao lado da combatente:
ela provoca o ataque. Se a combatente formar ao menos um hit, o movimento da
exploradora termina; caso contrário, ela continua o movimento.

Se uma criatura já adjacente à combatente atacar outro inimigo ao seu lado, sem
se deslocar, não há ataque de oportunidade.
```

- [ ] **Step 3: Renumber the following sections**

Change the two headings directly after the inserted section:

```markdown
## 9. Vitalidades, desgaste e condições
```

```markdown
## 10. Sequência de jogo
```

- [ ] **Step 4: Validate editorial coverage and diff hygiene**

Run:

```powershell
rg -n 'Área de ameaça e ataque de oportunidade|1,5 m|um ataque de oportunidade por rodada|Ataque Básico|movimento.*interrompido|## 9\. Vitalidades|## 10\. Sequência' docs\rolerolls-regras-de-negocio.md
git diff --check
```

Expected: all required rule phrases and headings appear once; `git diff --check` produces no output.

- [ ] **Step 5: Inspect the completed diff**

Run:

```powershell
git diff -- docs/rolerolls-regras-de-negocio.md
```

Expected: only the new section and two heading-number changes appear. Do not stage or modify the pre-existing change in `UnitTests/Attacks/Services/AttackServiceTests/CreatureBalanceDesignTests.cs`.

- [ ] **Step 6: Commit the rulebook change only**

Run:

```powershell
git add -- docs/rolerolls-regras-de-negocio.md
git commit -m "docs: add opportunity attack rules"
```

Expected: one commit containing only `docs/rolerolls-regras-de-negocio.md`.
