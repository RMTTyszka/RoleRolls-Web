# Regras de Equipamento de Combate Alinhadas ao Runtime — Design

## Objetivo

Reescrever a parte de armas e armaduras do livro de regras para descrever o runtime atual, removendo a atribuição incorreta ao Land of Heroes e tornando explícita a interação do equipamento com Vantagem, Desvantagem, Sorte, Azar, Buff e Debuff.

## Fonte de verdade e escopo

O código é a fonte de verdade. A mudança é somente em `docs/rolerolls-regras-de-negocio.md`; não altera regras nem comportamento do runtime.

As fontes são `Equipment.RefreshGripType`, `GripTypeDefinition.Stats`, `Creature.BasicAttack`, `Creature.Evade`, `ArmorDefinition` e `Roll.Process`. O livro deve identificar a regra como genérica do sistema, sem atribuí-la ao Land of Heroes.

## Estrutura da seção de equipamento

### Grips e ataque básico

Substituir a tabela limitada de categorias por uma seção de grips. Explicar que o grip efetivo deriva das armas nas mãos principal e secundária; um `GripType` explícito do item tem precedência sobre o default da categoria.

Documentar, para cada perfil de grip usado pelo ataque básico, estes campos:

- bônus de Acerto;
- bônus fixo de dano por hit;
- bônus de dano por hit multiplicado pelo nível do atacante;
- sucessos exigidos para formar um hit.

Os perfis são: arma leve em uma mão, arma média em uma mão, arma pesada em duas mãos, duas armas leves, duas armas médias, arma pesada em uma mão, arma média em duas mãos e escudo leve, médio ou pesado. Os campos de dano mágico e de atributo não entram no caminho atual de ataque básico/Evasion e não devem ser apresentados como regra desses fluxos.

O dano por hit do ataque básico deve ficar explícito como:

```text
dano por hit antes do bloqueio = soma dos excessos do grupo
  + bônus fixo do grip
  + (bônus do grip por nível × nível do atacante)
```

### Armaduras e Evasion

Preservar as categorias `Nenhuma`, `Leve`, `Média` e `Pesada` e documentar:

- bônus de Evasion da armadura;
- bloqueio-base;
- bloqueio adicional por nível do defensor;
- bônus de nível do item de peitoral, aplicado separadamente à Evasion.

O bloqueio total deve ser apresentado como:

```text
bloqueio = bloqueio-base da armadura
  + (bloqueio por nível × nível do defensor)
  + propriedade de bloqueio da campanha
```

### Estados de rolagem e equipamento

Usar graus romanos para os estados: `Vantagem I`, `Desvantagem II`, `Sorte I` e `Azar I`. Buffs e Debuffs continuam valores numéricos assinados, como `+2` e `-1`.

Documentar o comportamento efetivo atual:

- no ataque básico, Vantagem de Acerto é o maior valor entre o comando e os bônus ativos de Vantagem; ela adiciona dados à rolagem;
- em Evasion, Vantagem de Evasion adiciona dados e somente os melhores dados até a quantidade-base permanecem;
- Buff de Acerto ou Evasion soma ao valor estático correspondente;
- Sorte/Azar é um valor assinado: o positivo rerrola os menores resultados e mantém o maior; o negativo rerrola os maiores e mantém o menor;
- a combinação arma–armadura altera Sorte somente para arma leve ou pesada contra armadura leve ou pesada; combinações com armadura média, sem armadura, arma média ou escudos resultam em zero;
- Desvantagem e Debuff pertencem ao modelo de bônus e podem aparecer nas manobras, mas os caminhos atuais de `BasicAttack` e `Evade` não os consultam.

Incluir a matriz explícita: leve×leve `Sorte I`, leve×pesada `Azar I`, pesada×leve `Azar I`, pesada×pesada `Sorte I`; as demais combinações são neutras.

## Coerência editorial

Atualizar os exemplos de ataque e Evasion para nomear os valores da nova tabela de grips, sem alterar seus cálculos. A tabela de manobras continua a usar grau romano para estados; Buff e Debuff mantêm sinais arábicos. A documentação deve deixar clara a limitação atual de Desvantagem e Debuff em vez de afirmar um efeito ainda ausente do runtime.

## Verificação

- Conferir cada valor de grip contra `GripTypeDefinition.Stats`.
- Conferir cada valor de armadura contra `ArmorDefinition`.
- Conferir a matriz de Sorte contra `ResolveWeaponVsArmorLuck`.
- Conferir que nenhum estado de rolagem com grau use `+` ou `-`.
- Conferir que Buff e Debuff conservem notação numérica assinada.
- Revisar o diff para garantir que nenhum código ou documento técnico não relacionado seja modificado.
