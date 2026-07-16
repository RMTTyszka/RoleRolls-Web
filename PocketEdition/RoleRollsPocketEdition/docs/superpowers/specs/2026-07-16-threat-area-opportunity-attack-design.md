# Área de ameaça e ataque de oportunidade

## Objetivo

Adicionar ao livro de regras uma regra de posicionamento que permita a uma
criatura armada corpo a corpo punir o deslocamento de outra criatura dentro de
sua área de ameaça. A regra protege o espaço imediato da criatura sem impedir
aproximação, nem ações feitas sem deslocamento.

O escopo desta alteração é editorial: documentar a regra do sistema base. Não
há alteração de código, contratos ou testes automatizados nesta entrega.

## Localização no livro

Adicionar a seção `8. Área de ameaça e ataque de oportunidade` após `7. Ataque
especial`. Renumerar as seções atuais `8. Vitalidades, desgaste e condições` e
`9. Sequência de jogo` para `9` e `10`, respectivamente.

## Regra de jogo

### Área de ameaça

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

Portanto, uma criatura pode entrar na área e continuar aproximando-se sem
provocar o ataque. Se, depois de entrar, ela mudar a direção para mover-se de
lado ou para longe, provoca o ataque nesse ponto do deslocamento.

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

## Exemplos

Uma combatente com espada ameaça `1,5 m`. Uma exploradora entra nessa área e
segue em direção à combatente: não provoca ataque de oportunidade. Ainda dentro
da área, a exploradora muda a direção e tenta atravessar ao lado da combatente:
ela provoca um ataque de oportunidade. Se a combatente formar ao menos um hit,
o movimento da exploradora termina; caso contrário, ela continua o movimento.

Em outro momento, uma criatura já adjacente à combatente usa um ataque contra
um inimigo ao seu lado, sem deslocar-se. Não há ataque de oportunidade, porque
o gatilho exige deslocamento dentro da área de ameaça.

## Consistência e validação

O texto remete a `Ataque Básico` para manter especialidade ofensiva, arma,
Evasion, hits, dano, bloqueio e vitalidades sob a resolução já documentada. A
regra nova só acrescenta gatilho, frequência e consequência sobre movimento.

A revisão editorial deve confirmar que o texto contém os valores padrão
(`1,5 m` e um ataque por rodada), as exceções por arma/habilidade/poder, os
dois casos de não gatilho e os dois resultados possíveis do ataque. Como não
há implementação de código, não são necessários testes automatizados.

## Fora de escopo

- sistema geral de turnos, ações e deslocamento;
- rastreamento de ataques de oportunidade no motor;
- novas armas, habilidades ou poderes que alterem área, limite ou interrupção;
- mudanças na resolução do Ataque Básico.
