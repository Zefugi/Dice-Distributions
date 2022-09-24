using System.Text;

var distribution = SumChanceDistribution(new int[] { 4, 4 });
OutputDistributionGraph(distribution);

void OutputDistribution(int[] distribution)
{
    for (int i = 1; i < distribution.Length; i++)
        Console.WriteLine($"{string.Format("{0:00}", i)}  {string.Format("{0:00}", distribution[i])}");
}

void OutputDistributionGraph(int[] distribution)
{
    var str = new StringBuilder();
    int maxValue = MaxValue(distribution);
    for(int value = maxValue; value > 0; value--)
    {
        str.AppendFormat("{0:00}  ", value);
        for (int i = 1; i < distribution.Length; i++)
            str.Append((distribution[i] >= value) ? @"/\  " : "--  ");
        str.AppendLine();
    }
    str.Append("    ");
    for (int i = 1; i < distribution.Length; i++)
        str.AppendFormat("{0:00}  ", i);
    Console.WriteLine(str);
}

int[] SumChanceDistribution(int[] dice)
{
    var maxSum = MaxSum(dice);
    var distribution = new int[maxSum + 1];
    for (int i = 1; i <= maxSum; i++)
        distribution[i] = SumChance(i, dice);
    return distribution;
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