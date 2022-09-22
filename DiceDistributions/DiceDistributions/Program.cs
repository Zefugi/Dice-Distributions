using System.Text;

var str = new StringBuilder();

var distributions = SumChanceDistribution(new int[] { 4, 4 });
OutputDistributionGraph(distributions);

void OutputDistribution(int[] distributions)
{
    for (int i = 1; i < distributions.Length; i++)
        Console.WriteLine($"{string.Format("{0:00}", i)}  {string.Format("{0:00}", distributions[i])}");
}

void OutputDistributionGraph(int[] distributions)
{
    var str = new StringBuilder();
    int maxValue = MaxValue(distributions);
    for(int value = maxValue; value > 0; value--)
    {
        str.AppendFormat("{0:00}  ", value);
        for (int i = 1; i < distributions.Length; i++)
            str.Append((distributions[i] >= value) ? @"/\  " : "--  ");
        str.AppendLine();
    }
    str.Append("    ");
    for (int i = 1; i < distributions.Length; i++)
        str.AppendFormat("{0:00}  ", i);
    Console.WriteLine(str);
}

int[] SumChanceDistribution(int[] dice, int diceOffset = 0)
{
    var maxSum = MaxSum(dice);
    var distributions = new int[maxSum + 1];
    for (int i = 1; i <= maxSum; i++)
        distributions[i] = SumChance(i, dice);
    return distributions;
}

int SumChance(int sum, int[] dice, int diceOffset = 0)
{
    if (sum == 0)
        return 0;
    if (diceOffset + 1 == dice.Length)
    {
        for (int i = 1; i <= dice[diceOffset]; i++)
            if (i == sum)
                return 1;
        return 0;
    }
    else
    {
        var sumChance = 0;
        for (int i = 1; i <= dice[diceOffset]; i++)
            sumChance += SumChance(sum - i, dice, diceOffset + 1);
        return sumChance;
    }
}

int MaxSum(int[] dice)
{
    var sum = 0;
    for (int i = 0; i < dice.Length; i++)
        sum += dice[i];
    return sum;
}

int MaxValue(int[] values)
{
    int max = values[0];
    for (int i = 1; i < values.Length; i++)
        if (values[i] > max)
            max = values[i];
    return max;
}