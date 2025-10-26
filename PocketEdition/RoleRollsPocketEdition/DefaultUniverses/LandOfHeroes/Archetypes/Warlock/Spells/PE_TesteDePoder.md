## Teste de Poder (PE) — Referência Rápida

Este documento explica a notação e o uso de PE nos rituais e manobras.

### O que é PE

- PE é um teste composto por X dados de 20 (d20) definido pelo atributo/nível.
- Em geral, cada d20 “cobre” 1 alvo (criatura/objeto) quando o efeito declara que afeta múltiplos alvos.
- A margem acima de 10 em cada dado é convertida em bônus/efeito adicional conforme a notação.

### Notação PEk+v

- Forma: `PEk+v`.
- Leitura: “para cada k acima de 10 no d20, concede +v de bônus (ou unidades do efeito).”
- Exemplos:
  - `PE2+2`: para cada 2 pontos acima de 10, +2 de bônus. Ex.: rolar 16 (margem 6) concede 3 passos de k=2; bônus total +6.
  - `PE5+10`: para cada 5 acima de 10, +10 de bônus. Ex.: rolar 20 (margem 10) concede 2 passos; bônus total +20.

### Alvos por dado

- Regra comum: “normalmente, para cada d20 rolado, afeta uma criatura/objeto”.
- Se o ritual disser “Alvo: 1 criatura por dado do Teste de Poder (PE)”, cada dado bem-sucedido permite escolher 1 alvo adicional.

### Diretrizes de balanceamento

- Nível 1: use `PE2+1` ou `PE2+2` conforme a “apelação” da manobra (quanto mais forte, menor o ganho por passo).
- Escalonamento: o bônus pode crescer até cerca de `+5` nos círculos mais altos, mas evite crescer rápido demais, pois o personagem também ganha bônus com o avanço de nível/atributo.
- Subida entre círculos: nem sempre muda o efeito; pode apenas aumentar bônus, duração e/ou alcance.

### Outras formas

- Penalidade via passos: `PE2-2` — igual ao formato de passos, mas gerando penalidade. Para cada 2 acima de 10 no d20, aplica −2 (some entre os dados).
- Sucessos por quociente: `PE/10` — some todos os d20 do teste (e quaisquer bônus fixos do PE, se houver), divida por 10 e arredonde para baixo; o resultado são “sucessos”.
- Sucessos escalados: `PE/10*5` — calcule os sucessos como em `PE/10` e multiplique cada sucesso por 5 para obter o total do bônus/penalidade.
- Observação: substituir “10” e “5” por outros valores segue a mesma lógica (ex.: `PE/8*3`).

### Exemplos práticos

- Passos (bônus): “Bônus: `PE2+2` em testes de escalar” — cada 2 acima de 10 em um d20 rende +2; some entre os dados. Ex.: rolou 16 (margem 6) → 3 passos → +6.
- Passos (penalidade): “Penalidade: `PE2-2` em Percepção” — rolou 18 (margem 8) → 4 passos → −8 total.
- Alcance por sucesso “simples”: “5 m por sucesso” — conte os dados ≥ 11; cada sucesso concede 5 m.
- Alvo por dado: “Alvo: 1 criatura por dado” — com 3 d20 rolados, até 3 criaturas podem ser afetadas.
- Quociente de PE: `PE/10` — com 3d20 = 12, 17, 9 (soma 38) → 38/10 = 3 sucessos (resto ignora).
- Quociente escalado: `PE/10*5` — com a mesma rolagem (38), 3 sucessos × 5 = +15 (ou −15, se usado como penalidade).
