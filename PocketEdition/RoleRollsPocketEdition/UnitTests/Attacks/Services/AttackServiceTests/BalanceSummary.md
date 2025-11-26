# Balance Summary (Codex Session)

Contexto rápido para retomar de outro computador.

## Mecânica de ataque usada nos testes
- Rola 5d20 (3 atributos + 2 perícias).
- Complexidade = 10 + dodge da armadura.
- Um sucesso conta se (roll + hitBonus - complexidade) >= 0. Empate gera excesso 0.
- Agrupa excessos em blocos do tamanho da dificuldade da arma (light=1, medium=2, heavy=3).
- Dano por hit = soma dos excessos do grupo + `DamageBonusPerHit` da arma - block da armadura (min 0).

## Perfis baseline atuais
- Armas (HitBonus / DamageBonusPerHit por tier):
  - Light: diff 1, hit +1, dano +2 por tier.
  - Medium: diff 2, hit 0, dano +4 por tier.
  - Heavy: diff 3, hit 0, dano +7 por tier.
- Armaduras (dodge / block):
  - Light: dodge 2, block 2 + 1 por tier.
  - Medium: dodge 1, block 4 + 1 por tier.
  - Heavy: dodge 0, block 6 + 2 por tier.
- Tier: nível 1 já começa no tier 1; a cada 2 níveis aumenta tier (tier = 1 + (level-1)/2).

## Dominação e viabilidade
- Dominâncias esperadas:
  - Light vs Light > Heavy vs Light.
  - Medium vs Medium > Light vs Medium.
  - Medium vs Medium > Heavy vs Medium.
  - Heavy vs Heavy >= Medium vs Heavy.
- Razão de viabilidade mínima por nível: > 0.07 no teste de escala.

## Testes adicionados
- `FixedExampleMatchesPrompt`: usa perfis neutros (hit 0, dano 0) para reproduzir o exemplo original.
- `AutoSearchBalancedProfiles`: busca combinando dodge/block e hit bonus (ainda com dano 0) e escreve a melhor combinação.
- `LevelScalingKeepsBalance`: aplica perfis por tier/nível (com dano por tier) e valida dominâncias + razão mínima em níveis 1–20.
- `HitPointsNeededForFourRounds`: para cada nível e armadura, calcula o pior DPS médio (maior entre as armas) e registra o HP aproximado para aguentar ~4 turnos (ceil de 4× dano médio) no output.

## Observações de design
- Empate no teste (over=0) ainda conta como sucesso, mas pode gerar dano zero se o block for maior que o bônus de dano.
- Mantivemos dodge da light em 2 (não 3) e diferenciamos a medium com dodge 1 e block maior para evitar “igual porém melhor”.
- Escalonamento de dano/hit foi ajustado para manter equilíbrio com block da light subindo por tier.

## Caminho e execução
- Arquivo principal: `UnitTests/Attacks/Services/AttackServiceTests/RpgBalanceDesignTests.cs`.
- Rodar testes: `dotnet test --filter RpgBalanceDesignTests`.

## Próximos passos possíveis
- Rerodar busca automática se mudar premissas (dodge/block/dano por tier).
- Ajustar razão mínima se quiser estreitar a faixa de viabilidade.
