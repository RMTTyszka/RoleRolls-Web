namespace RoleRollsPocketEdition.Itens.Configurations;

public static class ArmorDefinition
{
    // AKA  evasion
    public static int EvasionBonus(ArmorCategory armorCategory)
    {
        return armorCategory switch
        {
            ArmorCategory.None => 3,
            ArmorCategory.Light => 2,
            ArmorCategory.Medium => 0,
            ArmorCategory.Heavy => -2,
            _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
        };
    }  
    // AKA defense
    public static int DamageReductionByLevel(ArmorCategory armorCategory)
    {
        return armorCategory switch
        {
            ArmorCategory.None => 0,
            ArmorCategory.Light => 1,
            ArmorCategory.Medium => 2,
            ArmorCategory.Heavy => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
        };
    }    
    // AKA defense
    public static int BaseDamageReduction(ArmorCategory armorCategory)
    {
        return armorCategory switch
        {
            ArmorCategory.None => 0,
            ArmorCategory.Light => 1,
            ArmorCategory.Medium => 2,
            ArmorCategory.Heavy => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
        };
    }  
    public static int BaseLuck(ArmorCategory armorCategory)
    {
        return armorCategory switch
        {
            ArmorCategory.None => 0,
            ArmorCategory.Light => 0,
            ArmorCategory.Medium => 0,
            ArmorCategory.Heavy => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
        };
    }
    
    
        static void Main()
    {
        int numDados = 5; // Número de dados rolados
        int bonus = 3;    // Bônus aplicado a cada dado
        double targetPercentual = 70; // Percentual alvo (exemplo: 70%)
        double margemErro = 5; // Margem de erro em % para cima/baixo

        var resultados = EncontrarCombinacoes(numDados, bonus, targetPercentual, margemErro);

        Console.WriteLine("Combinações de dificuldade e complexidade:");
        foreach (var resultado in resultados)
        {
            Console.WriteLine($"Dificuldade: {resultado.Item1}, Complexidade: {resultado.Item2}, Percentual: {resultado.Item3:F2}%");
        }
    }

    static List<(int, int, double)> EncontrarCombinacoes(int numDados, int bonus, double targetPercentual, double margemErro)
    {
        var combinacoes = new List<(int, int, double)>();

        for (int dificuldade = 1; dificuldade <= numDados; dificuldade++)
        {
            for (int complexidade = 1; complexidade <= 20; complexidade++)
            {
                double chance = CalcularChance(numDados, dificuldade, complexidade, bonus);
                if (Math.Abs(chance - targetPercentual) <= margemErro)
                {
                    combinacoes.Add((dificuldade, complexidade, chance));
                }
            }
        }

        return combinacoes;
    }

    static double CalcularChance(int numDados, int dificuldade, int complexidade, int bonus)
    {
        double probSucesso = Math.Max(0, 21 - (complexidade - bonus)) / 20.0;
        double probFalha = 1 - probSucesso;

        double probabilidade = 0;

        // Soma a probabilidade acumulada de obter pelo menos "dificuldade" sucessos
        for (int k = dificuldade; k <= numDados; k++)
        {
            probabilidade += Binomial(numDados, k) * Math.Pow(probSucesso, k) * Math.Pow(probFalha, numDados - k);
        }

        return probabilidade * 100; // Converte para porcentagem
    }

    static double Binomial(int n, int k)
    {
        if (k > n) return 0;
        double resultado = 1;
        for (int i = 1; i <= k; i++)
        {
            resultado *= (n - (k - i)) / (double)i;
        }
        return resultado;
    }
}