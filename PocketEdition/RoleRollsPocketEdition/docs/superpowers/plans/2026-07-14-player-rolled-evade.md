# Plano de Implementação — Evasion Rolada pelo Defensor

> Executar os passos em ordem e manter cada etapa verde antes de avançar.
> Nenhum passo altera o contrato de ataque básico ou de ataque especial.

## 1. Preparar a configuração de campanha

Arquivos principais:

- `Itens/Configurations/ItemConfiguration.cs`
- modelos e persistência da configuração de itens
- `DefaultUniverses/LandOfHeroes/LandOfHeroesTemplate.cs`

1. Escrever um teste que prove que uma campanha escolhe explicitamente a
   especialidade usada em Evasion.
2. Adicionar a propriedade configurável de Evasion à configuração de itens.
3. Configurar o Land of Heroes para usar a especialidade `Evasion`.
4. Criar a migração necessária e verificar a serialização da configuração.

Critério: a resolução defensiva obtém a especialidade configurada sem inspecionar
tokens de fórmula de Defesa.

## 2. Criar o contrato da ação defensiva

Arquivos principais:

- `Attacks/Models/EvadeInput.cs`
- `Attacks/Models/EvadeResponse.cs`
- `Attacks/Services/EvadeService.cs`
- `Scenes/Controllers/SceneCreaturesController.cs`
- `UnitTests/Attacks/Models/AttackContractTests.cs`
- testes de rota de `SceneCreaturesController`

1. Escrever testes que exigem a rota
   `POST .../creatures/{defenderId}/evades`.
2. Escrever testes para os campos permitidos de `EvadeInput`: `AttackerId`,
   `WeaponSlot`, `VitalityId`, `Luck` e `Advantage`.
3. Escrever testes que rejeitam campos de propriedade ofensiva, dano, bloqueio
   e resultados fornecidos pelo cliente.
4. Criar `EvadeInput`, `EvadeResponse`, `IEvadeService` e `EvadeService`.
5. Injetar `IEvadeService` no controlador e expor a rota do defensor.

Critério: o contrato deixa explícito que o defensor inicia a resolução e que o
servidor calcula todos os valores do atacante.

## 3. Substituir a resolução de domínio atual

Arquivos principais:

- `Creatures/Entities/CreatureDefend.cs`
- novo comando e resultado de domínio para Evasion
- `Creatures/Entities/CreatureBasicAttack.cs`
- `Rolls/Entities/Roll.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`

1. Escrever testes determinísticos para a quantidade-base de d20: ela deve
   ser igual ao total da especialidade ofensiva do atacante.
2. Escrever testes para a Dificuldade de Evasion, formada por `10` mais o
   bônus ofensivo do ataque básico: especialidade, grip, buffs, diferença de
   nível e bônus de nível da arma.
3. Escrever testes para resultados defensivos maior, igual e menor que a
   dificuldade. Eles devem provar, respectivamente: tentativa evitada,
   excesso `0` e excesso exato.
4. Criar comando e resultado próprios para Evasion, incluindo dados-base,
   dificuldade, bônus defensivo, dados conservados, excessos e hits.
5. Reescrever `CreatureDefend.Evade(...)` para usar o novo comando, sem rolar
   dados ofensivos.
6. Extrair helpers compartilhados do ataque básico apenas quando eles
   representarem a mesma fórmula de negócio; manter os dois fluxos públicos
   separados.

Critério: a única chamada a `IDiceRoller` na Evasion pertence aos dados do
defensor; nenhuma rolagem aleatória de dano ou de ataque permanece nesse fluxo.

## 4. Aplicar vantagem, sorte, hits e dano

Arquivos principais:

- resolvedor de Evasion criado na etapa 3
- `Rolls/Entities/Roll.cs`, caso a seleção de resultados precise de suporte
- `Itens/GripType.cs`
- `Itens/Configurations/ArmorDefinition.cs`
- `Creatures/Entities/CreatureBasicAttackVitalityResolver.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/EvadeTests.cs`

1. Escrever um teste em que Vantagem adiciona dados e preserva somente os
   melhores resultados até a quantidade-base.
2. Escrever testes para Sorte positiva rerrolar resultados baixos e Sorte
   negativa rerrolar resultados altos.
3. Escrever uma matriz determinística de agrupamento: arma leve usa um excesso
   por hit, média usa dois e pesada usa três; sobras não formam hit.
4. Escrever testes de dano por hit: soma de excessos, bônus por nível e grip,
   bloqueio e piso mínimo `1`.
5. Reutilizar o resolvedor de vitalidades para testar cascata de dano e
   condições do Land of Heroes.

Critério: o cálculo de dano de Evasion produz os mesmos blocos de dano que o
ataque básico produz quando recebe a mesma lista de excessos.

## 5. Integrar cena e resposta pública

Arquivos principais:

- `Scenes/Services/IScenesService.cs`
- `Scenes/Services/ScenesService.cs`
- `Scenes/Services/SceneActionDescriptionBuilder.cs`
- testes de cena e de resposta de ataques

1. Escrever um teste para o histórico de cena com atacante, defensor, arma,
   resultado da Evasion, hits e dano.
2. Adicionar `ProcessEvadeAction(...)` ao serviço de cenas.
3. Criar descrição de cena própria para Evasion.
4. Mapear todos os dados de domínio para `EvadeResponse`.
5. Testar a integração serviço–cena com rolagens determinísticas.

Critério: a cena mostra que o defensor realizou Evasion e não descreve uma
rolagem escondida do atacante.

## 6. Regressão, documentação e balanceamento

Arquivos principais:

- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- `UnitTests/Attacks/Services/AttackServiceTests/SpecialAttackTests.cs`
- `UnitTests/Attacks/Models/AttackContractTests.cs`
- `docs/rolerolls-regras-de-negocio.md`
- `docs/rolerolls-system/06-combate.md`
- `docs/rolerolls-system/07-evade-e-defesas.md`
- `docs/rolerolls-mapa-tecnico.md`

1. Executar e preservar os testes de ataque básico e ataque especial.
2. Atualizar os testes de Evasion que representam o fluxo antigo.
3. Atualizar a documentação técnica para a nova rota e para a separação entre
   ataque rolado pelo atacante e Evasion rolada pelo defensor.
4. Registrar no livro de regras o procedimento e o exemplo de Land of Heroes.
5. Executar a suíte completa:

   ```powershell
   dotnet test RoleRollsPocketEdition/RoleRollsPocketEdition.sln
   ```
6. Executar as simulações de balanceamento como diagnóstico separado e registrar
   as médias antes de qualquer ajuste numérico.

Critério: a suíte de regras passa; as simulações mostram valores observáveis
para arma, armadura e nível sem redefinir a regra aprovada.
