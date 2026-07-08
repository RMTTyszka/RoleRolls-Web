# RoleRolls - Regra de Negócio do Sistema Base

## Propósito

Esta documentação descreve o que o `RoleRolls` é como motor genérico de RPG.

O foco aqui não é um universo específico, uma classe específica de personagem, uma lista específica de vitalidades, nem um conjunto fixo de nomes como se eles fossem obrigatórios.

O foco aqui é responder:

- o que o sistema base sempre oferece
- o que o criador da campanha pode configurar
- como testes, defesas, vitalidades e combate funcionam em termos genéricos

## 1. O Que É o RoleRolls

O `RoleRolls` é um motor de RPG orientado à configuração.

Ele não nasce fechado em uma única fantasia de mundo. Em vez disso, permite que cada campanha ou universo defina sua própria combinação de:

- atributos
- perícias
- especializações
- defesas
- vitalidades
- condições
- tipos de dano
- tipos de criatura
- arquétipos
- manobras de combate

Em linguagem simples:

o sistema base define a estrutura do jogo, e cada universo define o conteúdo dessa estrutura.

## 2. O Que É Fixo no Motor

Mesmo sendo configurável, o `RoleRolls` possui um núcleo próprio de regra.

Esse núcleo inclui:

- ficha baseada em capacidades e especializações
- testes resolvidos por sucessos
- defesas configuráveis
- vitalidades configuráveis com fórmulas
- condições ligadas ao estado das vitalidades
- combate com armas categorizadas
- mitigação por defesa e por block
- fórmulas que podem depender de propriedades, nível e equipamento

Ou seja: o sistema é genérico, mas não é vazio. Ele tem uma forma própria de pensar ficha, teste e combate.

## 3. Estrutura Básica da Ficha

Toda criatura no `RoleRolls` parte das mesmas camadas conceituais.

### Atributos

Os atributos representam capacidades amplas.

Eles servem como base para testes e também podem alimentar fórmulas de defesa, vitalidade ou qualquer outra regra configurada pela campanha.

O nome, a quantidade e o significado dos atributos não são travados pelo motor base.

### Perícias

As perícias representam grandes áreas de competência.

Elas organizam a ficha em grupos mais amplos de atuação e ajudam a estruturar as especializações.

### Especializações

As especializações representam o uso concreto da capacidade da criatura em ações mais específicas.

Em termos de regra de negócio, elas são o ponto em que a ficha deixa de falar apenas de competência ampla e passa a falar do tipo exato de ação que será resolvida.

### Defesas

As defesas representam as barreiras que uma ação hostil precisa superar.

O sistema não exige uma única defesa fixa. Cada campanha pode definir as defesas que fizerem sentido para o seu estilo de jogo e para os tipos de confronto que deseja representar.

### Vitalidades

As vitalidades representam os reservatórios que absorvem desgaste, dano ou consumo de recursos.

O ponto central aqui é:

- o motor permite criar as vitalidades que a campanha quiser
- o nome dessas vitalidades não é fixo
- a fórmula de cada vitalidade não é fixa

Então, em regra de negócio, o sistema base não diz que toda criatura precisa ter um conjunto único de vitalidades. Ele diz que a campanha pode definir esse conjunto.

### Condições

As condições representam estados da criatura.

Elas podem servir para marcar desgaste, comprometimento, enfraquecimento, colapso ou qualquer outro estado relevante que a campanha queira expressar.

## 4. O Que a Campanha Pode Configurar

O `RoleRolls` foi desenhado para que a campanha escolha não apenas nomes, mas a própria anatomia da ficha.

Em termos de regra, a campanha pode definir:

- quais atributos existem
- quais perícias existem
- quais especializações pertencem a cada perícia
- se uma especialização usa o mesmo atributo da perícia ou outro atributo
- quais defesas existem
- quais vitalidades existem
- qual fórmula calcula cada defesa
- qual fórmula calcula cada vitalidade
- quais condições existem
- quais condições aparecem em faixas críticas de cada vitalidade
- quais propriedades alimentam acerto, dano e block de cada categoria de arma

Esse é o ponto mais importante do sistema base:

`RoleRolls` não entrega um mundo pronto. Ele entrega um esqueleto configurável para mundos diferentes.

## 5. Testes

Toda ação relevante do sistema segue a mesma ideia geral:

1. escolher a capacidade usada
2. transformar essa capacidade em rolagem
3. comparar os resultados com uma barreira
4. contar sucessos
5. transformar esses sucessos em efeito

### Lógica de sucesso

O `RoleRolls` não trabalha apenas com passa ou falha.

O total de sucessos importa.

Isso significa que o sistema não quer apenas responder `deu certo?`. Ele também quer responder `deu certo com quanta força?`.

### Complexity

`Complexity` representa a barra que cada resultado individual precisa atingir.

Em linguagem de negócio:

- ela mede a dificuldade de cada acerto individual dentro do teste

### Difficulty

`Difficulty` representa quantos sucessos o teste inteiro precisa acumular.

Em linguagem de negócio:

- ela mede quantos acertos válidos a ação precisa ter para funcionar

### Sucesso por margem

O sistema atual valoriza não apenas o acerto, mas também a margem acima da barreira.

Essa margem é importante porque, em especial no combate, ela alimenta a intensidade do resultado.

### Vantagem e sorte

O motor base trabalha com modificadores que melhoram ou pioram a rolagem.

Em termos de regra de negócio:

- vantagem deve melhorar a chance ou a qualidade do resultado
- desvantagem deve piorar a chance ou a qualidade do resultado
- sorte positiva deve favorecer o teste
- sorte negativa deve atrapalhar o teste

## 6. Fórmulas

O `RoleRolls` não exige que defesa e vitalidade sejam valores estáticos.

O motor base trabalha com fórmulas.

Essas fórmulas podem depender de:

- propriedades da criatura
- valores ligados ao nível da criatura
- valores ligados ao equipamento
- valores manuais definidos pela campanha

Em linguagem de negócio, isso quer dizer que a campanha pode escrever sua própria lógica de escalonamento.

Ela pode decidir, por exemplo, que:

- uma defesa cresce com um atributo
- uma vitalidade cresce com nível
- uma proteção cresce com equipamento
- um recurso cresce com uma mistura dessas coisas

O sistema base não prende isso a uma única progressão universal.

## 7. Progressão

O motor base possui nível e permite crescimento da ficha, mas ainda não apresenta uma única regra de progressão totalmente fechada e universal como verdade de negócio final.

O que dá para afirmar em nível de sistema é isto:

- existe nível
- existe crescimento da capacidade da criatura ao longo do nível
- fórmulas podem depender de nível e de escalonadores derivados do nível
- os testes de balanceamento trabalham com uma curva própria de progressão para estudar chance e dano

### Ponto em aberto

A progressão oficial do sistema base ainda precisa ser descrita de forma única e definitiva, sem depender da curva usada apenas para balanceamento.

## 8. Combate

O `RoleRolls` não trata mais todo ataque como uma coisa só.

O sistema base precisa distinguir dois fluxos ofensivos diferentes:

- ataque básico
- ataque especial

### Ataque Básico

O `ataque básico` é o fluxo de combate armado.

Em regra de negócio:

- usa arma equipada
- usa categoria da arma
- usa configuração da campanha para decidir qual propriedade entra no hit
- usa configuração da campanha para decidir qual propriedade entra no dano
- pode usar a defesa padrão configurada pela campanha ou um override explícito
- transforma sucessos em hits
- calcula dano automaticamente
- aplica desgaste de vitalidades automaticamente
- pode gerar status automáticos a partir dos thresholds de vitalidade

### Ataque Especial

O `ataque especial` é um teste ofensivo sem arma.

Em regra de negócio:

- não usa arma
- não usa `WeaponSlot`
- não usa categoria de arma
- não usa configuração de item para escolher a ofensiva
- sempre resolve `MinorSkill x Defense`
- usa `difficulty = 1`
- usa `complexity = valor atual da defesa alvo`
- nunca calcula dano automaticamente
- nunca desgasta vitalidade automaticamente
- nunca aplica efeito automaticamente

### Diferença Estrutural

O `ataque básico` é um fluxo completo de combate armado, parametrizado pela campanha.

O `ataque especial` é apenas a resolução do teste ofensivo.

Depois do resultado do `ataque especial`, o mestre decide a consequência narrativa ou mecânica.

## 9. Categorias de Arma

O motor base já trabalha com categorias de arma.

Entre elas, o material atual mostra como categorias centrais:

- leve
- média
- pesada

Em termos de regra de negócio, elas não servem apenas para dar nome ao equipamento. Elas mudam a forma como o acerto vira hit.

Isso cria identidades diferentes de arma:

- uma arma pode favorecer frequência de hit
- outra pode exigir mais sucessos por hit
- outra pode concentrar mais peso em menos hits

Essa lógica pertence ao motor, não a um universo específico.

## 10. Defesa e Block

O sistema base distingue duas camadas de mitigação.

### Defesa

A defesa responde:

`o golpe conseguiu me acertar?`

### Block

O block responde:

`depois de acertar, quanto do impacto ainda sobra?`

Essa separação é importante porque permite que a campanha modele criaturas ou armaduras que:

- evitam melhor o acerto
- resistem melhor ao dano
- ou equilibram as duas coisas

O motor base suporta essa separação de papéis.

## 11. Dano e Mitigação

No desenho atual do sistema, o dano nasce do sucesso ofensivo que sobreviveu à defesa.

Depois disso, o dano ainda passa por mitigação.

Em linguagem de negócio, isso quer dizer que o sistema separa três perguntas:

1. o golpe passou?
2. com quanta força ele passou?
3. quanto dessa força sobreviveu à proteção do alvo?

### Ponto em aberto

O tema mais sensível aqui é o piso de dano.

O material atual do projeto ainda preserva uma tensão entre:

- uma leitura em que um hit válido sempre gera pelo menos algum dano
- e outra leitura em que um hit pequeno ainda pode terminar em dano zero

Esse ponto precisa de definição de regra antes de qualquer refatoração técnica mais profunda.

## 12. Vitalidades Configuráveis

Esse é um dos pilares mais importantes do motor.

O `RoleRolls` não depende de uma lista fixa de vitalidades.

Uma campanha pode:

- criar as vitalidades que quiser
- escolher o nome de cada uma
- escolher a fórmula de cada uma
- decidir quais entram no fluxo de desgaste de um ataque básico
- decidir em que ordem esse desgaste acontece

Isso significa que o sistema base trata vitalidade como categoria configurável, não como lista fechada.

## 13. Ordem de Desgaste

As vitalidades podem participar de uma ordem de consumo para ataque básico.

Em linguagem de negócio, isso quer dizer que o dano não precisa cair sempre em um único recurso.

O sistema permite que a campanha diga algo como:

- primeiro desgasta esta vitalidade
- depois, se sobrar dano, desgasta a próxima
- depois, se ainda sobrar, desgasta a seguinte

O motor base suporta esse modelo de desgaste em camadas.

## 14. Condições por Faixa de Vitalidade

O motor base permite ligar condições ao estado crítico de uma vitalidade.

Hoje, a estrutura genérica observada suporta pelo menos dois gatilhos de negócio por vitalidade:

- uma condição em faixa crítica de `30 porcento`
- uma condição em `0`

Em linguagem simples:

o sistema permite transformar desgaste numérico em estado narrativo e funcional.

## 15. Condições como Regra de Estado

Uma condição no `RoleRolls` é um estado nomeado da criatura.

Ela pode carregar:

- nome
- descrição
- bônus ou penalidades

Em termos de negócio, isso permite que a campanha use condições para representar:

- fadiga
- ferimento
- medo
- instabilidade
- enfraquecimento
- qualquer outro estado relevante para seu jogo

Novamente: o motor define a estrutura. O universo define o conteúdo.

## 16. Mapeamento entre Ficha e Combate

O sistema base separa dois mapeamentos diferentes.

### Ataque Básico

O `ataque básico` não assume que toda arma usa sempre a mesma capacidade para acertar ou causar dano.

Em vez disso, a campanha pode configurar:

- qual propriedade alimenta o acerto de cada categoria de arma
- qual propriedade alimenta o dano de cada categoria de arma
- qual propriedade entra como block
- qual defesa a armadura referencia

Esse é um ponto muito importante da regra de negócio do motor base:

o `ataque básico` é parametrizado pela campanha.

Ou seja, o sistema base define a estrutura do combate armado, mas deixa para a campanha decidir de onde esse combate puxa seus valores.

### Ataque Especial

O `ataque especial` não usa esse mapeamento por categoria de arma.

Nele, o ponto de entrada já precisa vir explícito como:

- `MinorSkill`
- `Defense`
- `Luck`
- `Advantage`

O motor resolve apenas o teste entre a `MinorSkill` escolhida e a `Defense` escolhida.

### Diferença Estrutural

No `ataque básico`, a campanha define de onde vêm os números ofensivos.

No `ataque especial`, a ofensiva já nasce explícita como `MinorSkill x Defense`.

## 17. O Que Não É Regra Base do RoleRolls

As seguintes coisas não devem ser tratadas como regra universal do sistema base:

- nomes específicos de vitalidade de um universo
- nomes específicos de atributo de um universo
- uma tabela única de progressão de um universo
- uma lista única de condições de um universo
- uma leitura única de mundo, magia, cultura ou fantasia

Tudo isso pertence a campanhas e universos construídos em cima do motor.

## 18. Resumo Curto

Em linguagem direta, o `RoleRolls` base é isto:

- um motor de RPG configurável por campanha
- com ficha formada por capacidades, especializações, defesas e vitalidades
- com testes resolvidos por sucessos
- com fórmulas configuráveis para escalar recursos e defesas
- com combate estruturado por acerto, hits, mitigação e desgaste
- com vitalidades configuráveis e ordem de consumo configurável
- com condições ligadas ao estado das vitalidades

Esse é o núcleo da regra de negócio do `RoleRolls` sem confundir o sistema base com um `DefaultUniverse` específico.
