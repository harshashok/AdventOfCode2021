using System;
using System.Linq;

namespace AdventOfCode2021.Puzzles;

public class Day7
{
    readonly ReadInputFile f;
    int[] values;
    public Day7()
    {
        f = new ReadInputFile(FileConstants.INPUT_DAY_7);
        values = Array.ConvertAll(f.input.Split(','), n => Int32.Parse(n));
    }

    [Solution("Day7", "1")]
    public int solve()
    {
        values = values.OrderBy(n => n).ToArray();
        var median = (values.ElementAt(values.Length / 2) + values.ElementAt((values.Length - 1) / 2))/2;
        int sum = values.Sum(x => Math.Abs(median - x));

        return sum;
    }

    [Solution("Day7", "2")]
    public int solve2()
    {
        return Math.Min(values.Select(x => Math.Abs((int)Math.Floor(values.Average()) - x)).Sum(n => n * (n + 1) / 2),
                        values.Select(x => Math.Abs((int)Math.Ceiling(values.Average()) - x)).Sum(n => n * (n + 1) / 2));
    }
}

