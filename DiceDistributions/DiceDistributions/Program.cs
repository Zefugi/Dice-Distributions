using System.Data;
using System.IO.IsolatedStorage;
using System.Text;

var dice = new int[] { 4, 6 };
var distribution = SumChanceDistribution(dice);
var totalCombinations = TotalCombinations(dice);
Console.WriteLine($"Dice combinations: {totalCombinations}");
OutputDistributionGraph(distribution, totalCombinations);

void OutputDistribution(int[] distribution)
{
    for (int i = 1; i < distribution.Length; i++)
        Console.WriteLine($"{string.Format("{0:00}", i)}  {string.Format("{0:00}", distribution[i])}");
}

const string SPACER = "    ";
const string INT_FORMAT = " {0:00} ";
const string PERCENT_FORMAT = "{0:00.0} %";
const string COLUMN_SHAPE = @" /\ ";
const string ROW_SHAPE = " -- ";
void OutputDistributionGraph(int[] distribution, int totalCombinations)
{
    var str = new StringBuilder();
    int maxValue = MaxValue(distribution);
    for (int value = maxValue; value > 0; value--)
    {
        str.AppendFormat(INT_FORMAT, value);
        for (int i = 1; i < distribution.Length; i++)
            str.Append((distribution[i] >= value) ? COLUMN_SHAPE : ROW_SHAPE);
        str.AppendFormat(PERCENT_FORMAT, (float)value * 100f / totalCombinations);
        str.AppendLine();
    }
    str.Append(SPACER);
    for (int i = 1; i < distribution.Length; i++)
        str.AppendFormat(INT_FORMAT, i);
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

int TotalCombinations(int[] dice)
{
    int totalCombinations = dice[0];
    for (int d = 1; d < dice.Length; d++)
        totalCombinations *= dice[d];
    return totalCombinations;
}
