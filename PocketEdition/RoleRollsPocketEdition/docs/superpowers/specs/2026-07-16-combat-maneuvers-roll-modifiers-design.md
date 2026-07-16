# Manobras de Combate e Modificadores de Rolagem

## Objetivo

Completar o livro de regras do sistema base com os modificadores que já fazem
parte da linguagem de jogo e com as oito manobras de combate padrão do Land of
Heroes. O texto deve permitir que jogadores leiam uma manobra e saibam qual
ação ela exige, quem recebe cada efeito, por quanto tempo ele vale e como seus
valores alteram uma rolagem.

O escopo é editorial. Não altera a execução de poderes, o resolvedor de ataque,
nem os contratos da API.

## Estrutura do livro

Expandir a subseção atual `4. Testes > Vantagem e Sorte` para
`Modificadores de rolagem`. A regra geral vem antes de combate, pois testes,
ataques e Evasion usam os mesmos termos.

Adicionar `6. Manobras de combate` depois de `5. Ataque básico`. Renumerar as
seções atuais de Evasion até Sequência de jogo para `7` até `11`.

## Modificadores de rolagem

O livro definirá estes termos, todos com magnitude numérica `N`:

| Modificador | Efeito |
|---|---|
| Vantagem `+N` | Rola `N` dados adicionais. |
| Desvantagem `+N` | Rola `N` dados a menos, até o mínimo de zero dados. |
| Sorte `+N` | Rerrola os `N` menores resultados e conserva o maior de cada par. |
| Azar `-N` | Rerrola os `N` maiores resultados e conserva o menor de cada par. |
| Buff `+N` | Soma `N` ao valor estático da aplicação indicada. |
| Debuff `+N` | Subtrai `N` do valor estático da aplicação indicada. |

Uma aplicação identifica o valor afetado: `Acerto` modifica a ofensiva,
`Evasion` modifica a defesa e uma propriedade modifica a capacidade indicada.
Vantagem e Desvantagem se compensam antes da rolagem; o resultado líquido nunca
reduz a quantidade de dados abaixo de zero. Buffs e debuffs da mesma aplicação
somam seus valores.

Evasion preserva sua exceção já documentada: quando Vantagem cria dados extras,
o defensor conserva apenas os melhores resultados até completar a quantidade
base da defesa.

## Manobras de combate

Uma manobra instantânea de `Ação de Ataque` modifica o Ataque Básico que a
resolve. `Ação Completa` consome a ação completa da criatura. Cada linha informa
os modificadores em termos da regra geral e separa os efeitos do usuário dos
efeitos do alvo.

| Manobra | Ação e duração | Efeito |
|---|---|---|
| `Open Shot` | Ação de Ataque, instantânea, usuário | Vantagem Acerto `+2`. |
| `Full Attack` | Ação de Ataque, instantânea, usuário | Vantagem Acerto `+1`; Desvantagem Evasion `+1`; Debuff Evasion `+1`. |
| `Partial Attack` | Ação de Ataque, instantânea, usuário | Desvantagem Acerto `+1`. |
| `Cautious Attack` | Ação de Ataque, instantânea, usuário | Desvantagem Acerto `+1`; Vantagem Evasion `+1`. |
| `Auxiliar Attack` | Ação de Ataque, instantânea, usuário e alvo | Usuário: Desvantagem Acerto `+3`. Alvo: Vantagem Evasion `+2`. |
| `Full Defense` | Ação Completa, usuário, 1 turno | Desvantagem Acerto `+3`; Vantagem Evasion `+2`; Buff Evasion `+2`. |
| `Cover Ally` | Ação de Ataque, instantânea, usuário e alvo | Usuário: Desvantagem Acerto `+1`. Alvo: Vantagem Evasion `+1`. |
| `Full Cover Ally` | Ação de Ataque, instantânea, usuário e alvo | Usuário: Desvantagem Acerto `+3`. Alvo: Vantagem Evasion `+2`. |

`Full Attack` segue a intenção de jogo aprovada: seus dois termos de Evasion
afetam a Evasion do usuário, e não o Acerto, apesar de a configuração atual
registrá-los com a aplicação de Acerto.

## Exemplo de leitura

Uma personagem usa `Cautious Attack`. Nesta resolução, ela rola um dado de
Acerto a menos e ganha um dado de Evasion enquanto a manobra for aplicável. Se
ela recebe um Buff Evasion `+2`, esse valor soma dois ao bônus estático de sua
Evasion; ele não cria dados extras.

## Consistência e validação

A revisão editorial deve confirmar:

1. Vantagem, Desvantagem, Sorte, Azar, Buff e Debuff possuem efeito e magnitude
   explícitos;
2. Acerto, Evasion e propriedade são definidos como aplicações;
3. a tabela contém as oito manobras carregadas pelo Land of Heroes;
4. `Full Attack` documenta a intenção aprovada para Evasion;
5. duração, alvo e ação de cada manobra estão presentes;
6. a renumeração vai de `6. Manobras` até `11. Sequência de jogo` sem saltos.

Não há mudança de código; portanto, não são necessários testes automatizados.

## Fora de escopo

- aplicar no motor as Desvantagens e Debuffs cadastrados nas manobras;
- corrigir a aplicação técnica atual de Evasion em `Full Attack`;
- criar manobras, poderes de arquétipo ou ações de combate adicionais;
- definir economia geral de turnos além de Ação de Ataque e Ação Completa.
