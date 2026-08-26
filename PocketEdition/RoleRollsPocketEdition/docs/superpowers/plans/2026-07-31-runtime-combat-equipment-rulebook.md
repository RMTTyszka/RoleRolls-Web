# Regras de Equipamento de Combate Alinhadas ao Runtime Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Tornar o livro de regras fiel ao runtime de armas, grips, armaduras e estados de rolagem, usando graus romanos apenas para estados.

**Architecture:** A mudança restringe-se a `docs/rolerolls-regras-de-negocio.md`. A seção de equipamento passa a derivar os valores de `GripTypeDefinition.Stats` e `ArmorDefinition`; a seção de modificadores declara os estados exibidos e limita explicitamente o que `BasicAttack` e `Evade` consomem hoje.

**Tech Stack:** Markdown, Git, ripgrep.

---

### Task 1: Corrigir a convenção e a disponibilidade dos estados de rolagem

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md:173-182`
- Modify: `docs/rolerolls-regras-de-negocio.md:284-295`
- Reference: `Creatures/Entities/CreatureBasicAttack.cs:31-47`
- Reference: `Creatures/Entities/CreatureDefend.cs:32-41`
- Reference: `Rolls/Entities/Roll.cs:49-105`

- [ ] **Step 1: Substituir a regra geral por graus romanos e comportamento atual**

  Substituir os parágrafos atuais de `### Modificadores de rolagem` por:

  ```markdown
  Estados de rolagem usam um grau em algarismo romano: `I` corresponde a um,
  `II` a dois e assim por diante. Vantagem adiciona à rolagem a quantidade de
  dados indicada pelo grau. No ataque básico e em Evasion, se houver Vantagem
  no comando e em um bônus ativo, vale o maior grau; os graus não se somam.

  Sorte e Azar rerrolam a quantidade de dados indicada pelo grau. Sorte rerrola
  os menores resultados e conserva o maior de cada par; Azar rerrola os maiores
  e conserva o menor. Internamente, Sorte é positiva e Azar é negativo.

  Buff `+N` soma `N` ao valor estático da aplicação indicada. Debuff `-N`
  subtrai `N` desse valor. Buffs de Acerto e Evasion participam do ataque básico
  e de Evasion.

  Desvantagem e Debuff pertencem ao modelo de bônus e podem aparecer em
  manobras. No runtime atual, os caminhos de ataque básico e Evasion não os
  consultam; portanto não removem dados nem subtraem valores nesses fluxos.
  ```

- [ ] **Step 2: Aplicar a convenção de apresentação à tabela de manobras**

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

- [ ] **Step 3: Ajustar o exemplo de Defesa Total**

  Substituir o fim do exemplo por:

  ```markdown
  um dado a mais para Evasion. Com Defesa Total, também aplica Buff Evasion
  `+2` ao resultado de Evasion: o Buff soma dois ao bônus estático de Evasion;
  não cria dados extras.
  ```

- [ ] **Step 4: Verificar estados e manobras**

  Run: `rg -n -P '(Vantagem|Desvantagem|Sorte|Azar)[^\n]*`[+-][0-9N]+' docs/rolerolls-regras-de-negocio.md`

  Expected: nenhuma saída; estados com magnitude usam grau romano, enquanto Buff e Debuff conservam `+` ou `-`.

### Task 2: Documentar todos os grips usados pelo ataque básico

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md:224-257`
- Reference: `Itens/GripType.cs:28-41`
- Reference: `Itens/GripType.cs:206-222`
- Reference: `Creatures/Entities/Equipment.cs:143-159`
- Reference: `Creatures/Entities/CreatureBasicAttack.cs:23-25,31-33,41,64-65`

- [ ] **Step 1: Substituir “Categorias de arma” por “Grips de arma”**

  Explicar antes da tabela que o equipamento calcula o grip efetivo a partir das mãos principal e secundária. Um `GripType` definido no template da arma tem precedência; sem ele, a categoria determina o padrão: leve = uma mão leve, média = uma mão média, pesada = duas mãos pesada, e cada categoria de escudo define o escudo correspondente.

- [ ] **Step 2: Inserir a tabela completa de valores consumidos pelo ataque básico**

  Inserir esta tabela, sem referência ao Land of Heroes:

  ```markdown
  | Grip efetivo | Bônus de Acerto | Bônus fixo por hit | Bônus por nível do atacante | Sucessos por hit |
  |---|---:|---:|---:|---:|
  | Arma leve, uma mão | +1 | 0 | 3 | 1 |
  | Arma média, uma mão | +0 | 0 | 5 | 2 |
  | Arma pesada, duas mãos | -1 | 2 | 8 | 3 |
  | Duas armas leves | -1 | 0 | 3 | 1 |
  | Duas armas médias | -1 | 0 | 4 | 2 |
  | Arma pesada, uma mão | -1 | 0 | 8 | 3 |
  | Arma média, duas mãos | +2 | 8 | 5 | 3 |
  | Escudo leve | +0 | 4 | 0 | 1 |
  | Escudo médio | +1 | 8 | 0 | 2 |
  | Escudo pesado | +3 | 12 | 0 | 3 |
  ```

- [ ] **Step 3: Explicar as fórmulas que usam a tabela**

  Manter o bônus ofensivo e acrescentar que o bônus de nível da arma é `piso(nível do item / 2)`. Depois da tabela, declarar:

  ```text
  bônus de dano por hit = bônus fixo do grip
    + (bônus do grip por nível × nível do atacante)

  dano = máximo(
    soma dos excessos do grupo
    + bônus de dano por hit
    − bloqueio do alvo,
    1
  )
  ```

  Remover a fórmula de dano anterior para que a regra apareça uma única vez.

- [ ] **Step 4: Verificar os grips contra o runtime**

  Run: `rg -n 'Arma leve, uma mão|Duas armas leves|Arma média, duas mãos|Escudo pesado|Bônus fixo por hit|bônus do grip por nível' docs/rolerolls-regras-de-negocio.md`

  Expected: os dez grips ativos estão documentados com suas quatro magnitudes de combate, sem a expressão “Land of Heroes: bônus de hit”.

### Task 3: Completar armaduras, Sorte por equipamento e Evasion

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md:253-257`
- Modify: `docs/rolerolls-regras-de-negocio.md:322-335`
- Reference: `Itens/Configurations/ArmorDefinition.cs:10-44`
- Reference: `Itens/ItemInstance.cs:11-18`
- Reference: `Creatures/Entities/CreatureBasicAttack.cs:112-147`
- Reference: `Creatures/Entities/CreatureDefend.cs:30-57`

- [ ] **Step 1: Substituir a tabela de armadura pela tabela completa**

  Usar esta tabela e informar que o nível da linha de bloqueio é o nível do defensor:

  ```markdown
  | Armadura | Bônus de Evasion | Bloqueio-base | Bloqueio por nível |
  |---|---:|---:|---:|
  | Nenhuma | +0 | 0 | 0 |
  | Leve | +2 | 2 | 1 |
  | Média | +1 | 4 | 2 |
  | Pesada | -1 | 4 | 3 |
  ```

  Em seguida, declarar:

  ```text
  bloqueio = bloqueio-base
    + (bloqueio por nível × nível do defensor)
    + propriedade de bloqueio da campanha
  ```

- [ ] **Step 2: Documentar a contribuição da armadura para Evasion**

  Acrescentar à seção de Evasion:

  ```text
  bônus de Evasion = total da especialidade defensiva
    + bônus da armadura
    + piso(nível do peitoral / 2)
    + Buffs de Evasion
  ```

  Explicar que a armadura não fornece Sorte própria no runtime atual.

- [ ] **Step 3: Inserir a matriz de Sorte entre arma e armadura**

  Adicionar após as armaduras:

  ```markdown
  | Arma | Armadura leve | Armadura média | Armadura pesada | Sem armadura |
  |---|---|---|---|---|
  | Leve | Sorte I | Neutro | Azar I | Neutro |
  | Média | Neutro | Neutro | Neutro | Neutro |
  | Pesada | Azar I | Neutro | Sorte I | Neutro |
  | Escudo | Neutro | Neutro | Neutro | Neutro |
  ```

  Explicar que o modificador entra na Sorte da rolagem ofensiva e defensiva; a matriz não cria Vantagem, Desvantagem, Buff ou Debuff.

- [ ] **Step 4: Atualizar os dois exemplos de combate**

  No ataque médio, identificar `+0`, `0`, `5` e `2` como os valores do grip de arma média em uma mão. No exemplo de Evasion, explicitar que a armadura leve fornece `+2` e que a fórmula de bloqueio soma `2 + (1 × nível)` antes da propriedade `Vigor`.

- [ ] **Step 5: Verificar armaduras, matriz e Evasion**

  Run: `rg -n 'Nenhuma|Bloqueio-base|piso\(nível do peitoral / 2\)|Armadura leve|Sorte I|Azar I|armadura não fornece Sorte' docs/rolerolls-regras-de-negocio.md`

  Expected: as quatro armaduras, a fórmula de Evasion, a fórmula de bloqueio e a matriz de Sorte aparecem uma vez cada.

### Task 4: Validar a fidelidade editorial e isolar o commit

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md`
- Reference: `docs/superpowers/specs/2026-07-31-runtime-combat-equipment-rulebook-design.md:1-79`

- [ ] **Step 1: Conferir os valores documentados contra as fontes do runtime**

  Run: `rg -n 'new GripTypeStats|DefenseBonus1|DamageReductionByLevel|BaseDamageReduction|ResolveWeaponVsArmorLuck' Itens/GripType.cs Itens/Configurations/ArmorDefinition.cs Creatures/Entities/CreatureBasicAttack.cs`

  Expected: os valores do livro correspondem aos dez grips, quatro armaduras e quatro combinações não neutras de Sorte.

- [ ] **Step 2: Procurar notações incompatíveis**

  Run: `rg -n -P '(Vantagem|Desvantagem|Sorte|Azar)[^\n]*`[+-][0-9N]+' docs/rolerolls-regras-de-negocio.md`

  Expected: nenhuma saída.

- [ ] **Step 3: Conferir o diff e espaços em branco**

  Run: `git diff --check && git diff -- docs/rolerolls-regras-de-negocio.md`

  Expected: nenhum erro de espaços; somente a regra geral, a seção de equipamento, os exemplos e a tabela de manobras são alterados.

- [ ] **Step 4: Commit**

  ```bash
  git add docs/rolerolls-regras-de-negocio.md
  git commit -m "docs: align combat equipment rules with runtime"
  ```

  Expected: o commit contém somente o livro de regras e não inclui as alterações preexistentes em `Creatures/Entities/Creature.cs`, `docs/rolerolls-system/04-resolucao-de-propriedades-e-formulas.md`, `UnitTests/Creatures/` nem o plano anterior.
