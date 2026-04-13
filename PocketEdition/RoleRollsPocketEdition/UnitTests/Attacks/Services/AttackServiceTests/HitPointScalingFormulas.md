# Hit Point Scaling Formulas

Documento curto com duas formulas simples para escalar `Life` e `Moral` de forma parecida com a curva observada no teste `HitPointsNeededForFourRounds`.

## Objetivo

- manter o calculo simples o suficiente para um jogador fazer na mao
- manter `Moral > Life`
- aceitar pequenas diferencas para cima ou para baixo em alguns niveis

## Variavel Base

As duas formulas usam o mesmo valor:

```text
T = ceil(Level / 2)
```

Equivalencia por nivel:

- niveis 1-2: `T = 1`
- niveis 3-4: `T = 2`
- niveis 5-6: `T = 3`
- niveis 7-8: `T = 4`
- niveis 9-10: `T = 5`
- niveis 11-12: `T = 6`
- niveis 13-14: `T = 7`
- niveis 15-16: `T = 8`
- niveis 17-18: `T = 9`
- niveis 19-20: `T = 10`

## Formula 1

Opcao mais forte. Fica mais proxima da parte media e alta da curva.

```text
Life  = 7 + T + T²
Moral = 7 + 4T + T²
Total = 14 + 5T + 2T²
```

Totais por tier:

- `T1 = 21`
- `T2 = 32`
- `T3 = 47`
- `T4 = 66`
- `T5 = 89`
- `T6 = 116`
- `T7 = 147`
- `T8 = 182`
- `T9 = 221`
- `T10 = 264`

## Formula 2

Opcao mais contida. Sobe menos no fim da progressao.

```text
Life  = 7 + T + T²
Moral = 8 + 3T + T²
Total = 15 + 4T + 2T²
```

Totais por tier:

- `T1 = 21`
- `T2 = 31`
- `T3 = 45`
- `T4 = 63`
- `T5 = 85`
- `T6 = 111`
- `T7 = 141`
- `T8 = 175`
- `T9 = 213`
- `T10 = 255`

## Leitura Rapida

- As duas formulas comecam em `21` no nivel 1.
- As duas mantem `Moral > Life` em todos os tiers.
- A `Formula 1` acompanha melhor a curva de HP recomendado do teste.
- A `Formula 2` e mais conservadora para nao inflar tanto os niveis altos.

## Como Calcular na Mao

1. Descubra o `T` pelo nivel.
2. Calcule `T²`.
3. Some os valores de `Life`.
4. Some os valores de `Moral`.

Exemplo no nivel 9:

```text
Level = 9
T = ceil(9 / 2) = 5
T² = 25
```

Formula 1:

```text
Life  = 7 + 5 + 25 = 37
Moral = 7 + 20 + 25 = 52
Total = 89
```

Formula 2:

```text
Life  = 7 + 5 + 25 = 37
Moral = 8 + 15 + 25 = 48
Total = 85
```
