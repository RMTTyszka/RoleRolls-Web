# Balance Summary (Codex Session)

Contexto rapido para retomar de outro computador.

## Mecanica de ataque usada nos testes
- Rola 5d20 (3 atributos + 2 pericias).
- Complexidade = 10 + dodge da armadura.
- Um sucesso conta se (roll + hitBonus - complexidade) >= 0. Empate gera excesso 0.
- Agrupa excessos em blocos do tamanho da dificuldade da arma (light=1, medium=2, heavy=3).
- Dano por hit = soma dos excessos do grupo + `DamageBonusPerHit` da arma - block da armadura (min 0).

## Perfis baseline atuais
- Armas (HitBonus / DamageBonusPerHit por tier):
  - Light: diff 1, hit +1, dano +3 por tier.
  - Medium: diff 2, hit 0, dano +5 por tier.
  - Heavy: diff 3, hit 0, dano +7 por tier.
- Armaduras (dodge / block):
  - Light: dodge 2, block 2 + 1 por tier.
  - Medium: dodge 1, block 4 + 2 por tier.
  - Heavy: dodge -1, block 6 + 3 por tier (block alto compensado por dodge negativo).
- Tier: nivel 1 ja comeca no tier 1; a cada 2 niveis aumenta tier (tier = 1 + (level-1)/2).

## Dominancia e viabilidade
- Dominancias esperadas:
  - Light vs Light > Heavy vs Light.
  - Medium vs Medium > Light vs Medium.
  - Medium vs Medium > Heavy vs Medium.
  - Heavy vs Heavy >= Medium vs Heavy.
- Razao de viabilidade minima por nivel: > 0.07 no teste de escala.

## Testes adicionados
- `FixedExampleMatchesPrompt`: usa perfis neutros (hit 0, dano 0) para reproduzir o exemplo original.
- `AutoSearchBalancedProfiles`: busca combinando dodge/block e hit bonus (ainda com dano 0) e escreve a melhor combinacao.
- `LevelScalingKeepsBalance`: aplica perfis por tier/nivel (com dano por tier) e valida dominancias + razao minima em niveis 1-20.
- `HitPointsNeededForFourRounds`: para cada nivel e armadura, calcula o pior DPS medio (maior entre as armas) e registra o HP aproximado para aguentar ~4 turnos (ceil de 4x dano medio) no output.

## Observacoes de design
- Empate no teste (over=0) ainda conta como sucesso, mas pode gerar dano zero se o block for maior que o bonus de dano.
- Light segue com dodge 2 e subida de block de +1 por tier; medium abre distancia com +2 por tier; heavy ganha block +3 por tier, mas com dodge -1 para continuar punida por acertos.
- Escalonamento de dano/hit das armas subiu para 3/5/7 por tier (L/M/H) para manter dominancias e viabilidade com os blocks mais inclinados.

## Caminho e execucao
- Arquivo principal: `UnitTests/Attacks/Services/AttackServiceTests/RpgBalanceDesignTests.cs`.
- Rodar testes: `dotnet test --filter RpgBalanceDesignTests`.

## Proximos passos possiveis
- Rerodar busca automatica se mudar premissas (dodge/block/dano por tier).
- Ajustar razao minima se quiser estreitar a faixa de viabilidade.
