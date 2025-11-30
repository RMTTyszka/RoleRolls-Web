# Balance Summary (Codex Session)

Contexto rapido para retomar de outro computador.

## Mecanica de ataque usada nos testes
- Rola 5d20 (3 atributos + 2 pericias).
- Complexidade = 10 + dodge da armadura.
- Um sucesso conta se (roll + hitBonus - complexidade) >= 0. Empate gera excesso 0.
- Agrupa excessos em blocos do tamanho da dificuldade da arma (light=1, medium=2, heavy=3).
- Dano por hit = soma dos excessos do grupo + `DamageBonusPerHit` da arma - block da armadura (min 0).
- Nos testes por nivel adicionamos um bonus plano de nivel (`level - 1`) tanto no acerto quanto na esquiva (bonus de hit e complexidade/dodge recebem esse ajuste).
- Sorte/Azar nos testes: rerrola 1 dado (menor falho para sorte, maior bem-sucedido para azar), ignorando 1/20.
- Vantagem nos testes: adotamos +1 dado fixo com valor 15 (impacta mais heavy/medium; light ganha menos porque o block come parte do excesso). Deltas médios de dano com vantagem (vs baseline): Light ~+3.8, Medium ~+6.8, Heavy ~+7.7.

## Perfis baseline atuais
- Armas (HitBonus / DamageBonusPerHit por tier):
  - Light: diff 1, hit +1, dano +3 por tier.
  - Medium: diff 2, hit 0, dano +5 por tier.
  - Heavy: diff 3, hit 0, dano +6 por tier (+2 fixo).
- Armaduras (dodge / block):
  - Light: dodge 2, block 2 + 1 por tier.
  - Medium: dodge 1, block 4 + 2 por tier.
  - Heavy: dodge -1, block 4 + 3 por tier (mais block, penalidade de dodge mais leve).
- Tier: nivel 1 ja comeca no tier 1; a cada 2 niveis aumenta tier (tier = 1 + (level-1)/2).
- Dados: AttributeDice base 3, ganha +1 nos niveis 6, 11 e 16. SkillDice base 2, ganha +1 nos niveis 4, 8 e 12.

## Dominancia e viabilidade
- Dominancias esperadas:
  - Light vs Light > Heavy vs Light.
  - Medium vs Medium > Light vs Medium.
  - Medium vs Medium > Heavy vs Medium.
  - Heavy vs Heavy >= Medium vs Heavy.
- Razao de viabilidade minima por nivel: > 0.07 no teste de escala.
- Sorte/Azar:
  - Light x Light e Heavy x Heavy: sorte (+1) rerrola 1 dado falho (menor), somente se precisa, nunca rerrola 1 ou 20; fica com o melhor resultado.
  - Light x Heavy e Heavy x Light: azar (-1) rerrola 1 dado bem-sucedido (maior), somente se houve sucesso, nunca rerrola 1 ou 20; fica com o pior resultado. A rerrolagem usa o proprio alvo do ataque (complexidade/dodge/hit bonus) para decidir se houve sucesso.

## Testes adicionados
- `FixedExampleMatchesPrompt`: usa perfis neutros (hit 0, dano 0) para reproduzir o exemplo original.
- `AutoSearchBalancedProfiles`: busca combinando dodge/block e hit bonus (ainda com dano 0) e escreve a melhor combinacao.
- `LevelScalingKeepsBalance`: aplica perfis por tier/nivel (com dano por tier) e valida dominancias + razao minima em niveis 1-20.
- `HitPointsNeededForFourRounds`: para cada nivel, calcula o DPS medio considerando todas as combinacoes arma/armadura e registra o HP aproximado para aguentar ~4 turnos (ceil de 4x dano medio) no output.

## Observacoes de design
- Empate no teste (over=0) ainda conta como sucesso, mas pode gerar dano zero se o block for maior q ue o bonus de dano.
- Light segue com dodge 2 e subida de block de +1 por tier; medium abre distancia com +2 por tier; heavy ganhou block +3 por tier e mantem uma penalidade moderada de dodge (-1).
- Escalonamento de dano/hit das armas esta em 3/5/6 por tier (L/M/H), com +2 fixo na heavy para manter dominancias e viabilidade com os blocks mais inclinados.

## Caminho e execucao
- Arquivo principal: `UnitTests/Attacks/Services/AttackServiceTests/RpgBalanceDesignTests.cs`.
- Rodar testes: `dotnet test --filter RpgBalanceDesignTests`.

## Proximos passos possiveis
- Rerodar busca automatica se mudar premissas (dodge/block/dano por tier).
- Ajustar razao minima se quiser estreitar a faixa de viabilidade.
