# Land of Heroes — Livro de Campanha

## Como usar este livro

Land of Heroes é uma campanha do RoleRolls. Este livro define suas escolhas
oficiais: criação de personagem, atributos, perícias, opções, equipamentos e
conjurações. As regras de resolução — testes, ataque, Evasão, dano, ações e
vitalidades — estão no [Livro de Regras do RoleRolls](../livro-de-regras.md) e
não são repetidas aqui.

Quando este livro informa um nome, valor, lista ou fórmula, ele é regra de Land
of Heroes. Quando remete ao livro-base, aplica-se a regra geral do RoleRolls.

## 1. Criação de personagem

Uma personagem de nível 1 distribui exatamente `12` pontos entre os seis
atributos da campanha. Cada atributo recebe no mínimo `1` ponto e no máximo
`3`. A distribuição equilibrada é `2/2/2/2/2/2`; uma distribuição como
`2/3/2/2/2/1` também é válida.

O teto por nível vem do RoleRolls:

| Nível | Teto por atributo |
|---|---:|
| 1–5 | 3 |
| 6–11 | 4 |
| 12–17 | 5 |
| 18–20 | 6 |

Os bônus de raça são bônus inatos de propriedade. Eles não consomem os 12
pontos distribuídos na criação.

### Atributos

| Nome usado no livro | Nome da configuração |
|---|---|
| Agilidade | `Agility` |
| Carisma | `Charisma` |
| Inteligência | `Intelligence` |
| Intuição | `Intuition` |
| Força | `Strength` |
| Vigor | `Vigor` |

## 2. Perícias e especialidades

Use a regra de perícias e especialidades do [Livro de Regras do
RoleRolls](../livro-de-regras.md). Esta é a árvore oficial do Land of Heroes.

| Atributo ligado | Perícia | Especialidades |
|---|---|---|
| Intuição | Percepção (`Awareness`) | Observar, Ouvir, Procurar, Sentir |
| Carisma | Empatia (`Empathy`) | Diplomacia, Blefe |
| Inteligência | Conhecimento (`Knowledge`) | História, Arcano, Religião, Natureza, Masmorras |
| Agilidade | Destreza (`Nimbleness`) | Acrobacia, Esconder-se, Furtividade |
| Vigor | Sobrevivência (`Survival`) | Fome, Clima Frio, Clima Quente |
| Inteligência | Tratamento (`Treatment`) | Ferimento, Veneno, Maldição, Doença |
| Força | Atletismo (`Athletics`) | Natação, Corrida, Escalada, Salto |
| Variável | Combate (`Combat`) | Armas corpo a corpo leves, médias e pesadas; armas à distância leves, médias e pesadas; Evasão; Concentração; Causar Maldição, Ferimento e Veneno ou Doença |
| Variável | Resistência (`Resistance`) | Resistir Ferimento, Maldição e Veneno ou Doença |

Combate e Resistência não têm um único atributo ligado. Cada especialidade
dessas perícias usa o seu atributo configurado. Em especial: Evasão e armas
leves usam Agilidade; armas médias e pesadas usam Força; resistências usam
Vigor; Concentração e as três especialidades de causar efeito não têm atributo
ligado.

## 3. Defesas, vitalidades e condições

### Evasão e bloqueio

A defesa da campanha é **Evasão**. Sua fórmula é:

```text
10 + Evasão + bônus de defesa do peitoral + bônus de nível do peitoral
```

Evasão usa a especialidade Evasão, ligada a Agilidade. O bloqueio usa Vigor
como propriedade da criatura. Veja resolução, armaduras e empunhaduras nas
seções de combate e Evasão do [Livro do RoleRolls](../livro-de-regras.md).

### Vitalidades

| Vitalidade | Fórmula máxima | Ordem no dano básico |
|---|---|---:|
| Vida (`Life`) | `4 × Vigor + 2 × nível + Growth` | 2 |
| Moral (`Moral`) | `4 × Intuição + 2 × nível + Growth + 2 × Tier` | 1 |
| Mana (`Mana`) | `2 + nível / 6` | — |

Moral recebe dano antes de Vida. Mana não participa da ordem padrão de dano
básico.

| Estado | Gatilho |
|---|---|
| Abalado (`Shaken`) | Moral em 30% ou menos |
| Sangrando (`Bleeding`) | Moral em 0; Abalado continua aplicado pela faixa de 30% |
| Debilitado (`Debilitated`) | Vida em 30% ou menos |

Essas condições estão cadastradas sem bônus automático próprio. A condição
marca o estado; efeitos adicionais dependem da regra que a aplicar.

### Tipos de dano

Marcial, Arcano, Fogo, Gelo, Eletricidade, Ácido, Necrótico e Radiante.

## 4. Combate e equipamento

As regras de ataque, Evasão, dano, bloqueio, sorte por arma e armadura, área de
ameaça e ações estão no [Livro do RoleRolls](../livro-de-regras.md). Land of
Heroes usa as opções a seguir.

### Iniciativa

Land of Heroes configura `IniciativePropertyId` como **Agilidade**. Cada
criatura rola `max(1, 1 + Agilidade)` d20 e conserva apenas o maior resultado
como score de iniciativa. Empates usam maior Agilidade; persistindo empate,
somente as criaturas empatadas rolam novamente. O score inicial permanece
registrado.

### Manobras disponíveis

Tiro Livre, Ataque Completo, Ataque Parcial, Ataque Cauteloso, Ataque Auxiliar,
Defesa Total, Cobrir Aliado e Cobertura Total de Aliado. Seus efeitos estão na
seção **Manobras de combate** do livro-base.

### Armas do catálogo inicial

| Categoria | Armas |
|---|---|
| Leves, corpo a corpo | Clava, Adaga, Machado de Mão, Espada Curta, Cimitarra |
| Médias, corpo a corpo | Florete, Espada Longa, Machado de Batalha, Martelo de Guerra, Lança, Bordão |
| Pesadas, corpo a corpo | Espada Grande, Machado Grande, Malho |
| Leves, à distância | Arco Curto |
| Médias, à distância | Besta Leve |
| Pesadas, à distância | Arco Longo, Besta Pesada |
| Escudos | Broquel, Escudo, Escudo Torre |

Lança e bordão têm alcance de haste; as demais armas corpo a corpo do catálogo
têm alcance corpo a corpo. Arco Curto, Arco Longo e bestas são armas à
distância. A categoria da arma e a empunhadura efetiva determinam sua resolução
no livro-base.

### Armaduras do catálogo inicial

| Categoria | Armaduras |
|---|---|
| Leve | Acolchoada, Couro, Couro Batido |
| Média | Camisa de Cota, Cota de Escamas, Peitoral, Meia-Armadura |
| Pesada | Cota de Malha, Cota de Talas, Placas |

## 5. Raças e tipos de criatura

As raças disponíveis para personagens são:

| Raça | Bônus inato |
|---|---|
| Elfo | Agilidade +2 |
| Humano | Inteligência +1 |
| Anão | Força +2 |
| Halfling | Agilidade +2 |
| Orc | Força +3 |
| Goblin | Agilidade +1 |

Orc e Goblin podem ser aliados ou inimigos. Elfo, Humano, Anão e Halfling estão
cadastrados como aliados.

O bestiário inicial contém:

| Tipo | Descrição de uso |
|---|---|
| Esqueleto | Ossos animados por força necromântica. |
| Zumbi | Cadáver que continua avançando mesmo após grande castigo. |
| Lobo | Caçador veloz que depende de instinto e matilha. |
| Aranha Gigante | Emboscadora que usa veneno e teias. |
| Ogro | Força bruta para sobrepujar adversários. |

Todos os tipos do bestiário inicial estão cadastrados como inimigos.

## 6. Arquétipos e poderes

Os arquétipos disponíveis são Guerreiro, Bárbaro, Bardo, Cruzado, Druida,
Caçador, Artista Marcial, Ladino, Conjurador, Espiritualista e Bruxo. A campanha
carrega as progressões de poder por nível para todos eles.

Alguns arquétipos possuem material de referência detalhado já escrito. Estes
links fazem parte deste livro e evitam inventar efeitos onde a configuração só
registra o nome do poder:

- [Guerreiro](../../DefaultUniverses/LandOfHeroes/Archetypes/Warrior/Glossario.md)
- [Bárbaro](../../DefaultUniverses/LandOfHeroes/Archetypes/Barbarian/Glossario.md)
- [Bardo](../../DefaultUniverses/LandOfHeroes/Archetypes/Bard/Glossario.md)
- [Cruzado](../../DefaultUniverses/LandOfHeroes/Archetypes/Crusader/Glossario.md)
- [Druida](../../DefaultUniverses/LandOfHeroes/Archetypes/Druid/Glossario.md)
- [Caçador](../../DefaultUniverses/LandOfHeroes/Archetypes/Hunter/Glossario.md)
- [Artista Marcial](../../DefaultUniverses/LandOfHeroes/Archetypes/Martialist/Glossario.md)
- [Ladino](../../DefaultUniverses/LandOfHeroes/Archetypes/Rogue/Glossario.md)
- [Conjurador](../../DefaultUniverses/LandOfHeroes/Archetypes/Spellcaster/Glossario.md)
- [Espiritualista](../../DefaultUniverses/LandOfHeroes/Archetypes/Spiritualist/Glossario.md)
- [Bruxo](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Glossario.md)

## 7. Feitiços e magias

As regras do RoleRolls para resolver um ataque especial estão no livro-base.
Land of Heroes classifica as conjurações em Menores, Intermediárias e Maiores.

| Categoria | Círculos sobem nos níveis |
|---|---|
| Menor | 3, 5 e 7 |
| Intermediária | 4, 8 e 12 |
| Maior | 6, 12 e 18 |

[Feitiços](../../DefaultUniverses/LandOfHeroes/Conjurations/Spells/README.md)
e [magias](../../DefaultUniverses/LandOfHeroes/Conjurations/Magics/README.md)
usam essa mesma progressão. O [glossário de magia](../../DefaultUniverses/LandOfHeroes/Conjurations/Magics/Glossario.md)
define deslocamentos e classes de armadura usados pelos efeitos.

### Catálogo de feitiços do Bruxo

- Menores: [Sonho](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Minor/Sonho.md), [Preservar Cadáver](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Minor/PreservarCadaver.md), [Detectar Venenos](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Minor/DetectarVenenos.md), [Comunhão com Espíritos](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Minor/ComunhaoComEspiritos.md) e [Apodrecer](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Minor/Apodrecer.md).
- Intermediários: [Teia](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Medium/Teia.md), [Som Ilusório](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Medium/SomIlusorio.md), [Poder da Escuridão](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Medium/PoderDaEscuridao.md), [Máscara](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Medium/Mascara.md), [Manto das Sombras](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Medium/MantoDasSombras.md), [Controlar os Mortos](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Medium/ControlarOsMortos.md), [Clarividência](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Medium/Clarividencia.md), [Armadura de Ossos](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Medium/ArmaduraDeOssos.md), [Armadura das Sombras](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Medium/ArmaduraDasSombras.md) e [Arma das Sombras](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Spells/Medium/ArmaDasSombras.md).

### Catálogo de magias do Bruxo

- Menores: [Toque Vampírico](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Minor/ToqueVampirico.md), [Toque Macabro](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Minor/ToqueMacabro.md) e [Toque do Lívido](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Minor/ToqueDoLivido.md).
- Intermediárias: [Visão da Morte](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Medium/VisaoDaMorte.md), [Tentáculos Negros](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Medium/TentaculosNegros.md), [Recuo das Sombras](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Medium/RecuoDasSombras.md) e [Magia Negra](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Medium/MagiaNegra.md).
- Maiores: [Totem do Desespero](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Major/TotemDoDesespero.md), [Risada Macabra](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Major/RisadaMacabra.md), [Doença Plena](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Major/DoencaPlena.md) e [Correntes Sanguinárias](../../DefaultUniverses/LandOfHeroes/Archetypes/Warlock/Conjurations/Magics/Major/CorrentesSanguinarias.md).

Os detalhes de cada conjuração ficam na página vinculada. Poderes que não têm
texto próprio ainda são apresentados somente pela progressão de seu arquétipo;
este livro não atribui efeitos que a campanha não definiu.
