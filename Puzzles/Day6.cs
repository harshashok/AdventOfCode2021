using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles;

public class Day6
{
    List<int> fishes;
    List<long> fishSchool;
    readonly ReadInputFile f;
    public Day6()
    {
        fishes = new List<int>();
        f = new ReadInputFile(FileConstants.INPUT_DAY_6);
        fishSchool = Enumerable.Repeat(0L, 9).ToList();
        fishes.AddRange(f.lines.First()
                       .Split(',', StringSplitOptions.RemoveEmptyEntries)
                       .Select(a => Int32.Parse(a)));

        foreach (int fish in fishes)
        {
            fishSchool[fish] += 1;
        }
    }

    [Solution("Day6", "1")]
    public long solve()
    {
        Tick(80);
        return fishSchool.Sum(x => x);
    }


    [Solution("Day6", "2")]
    public long solve2()
    {
        Tick(256);
        return fishSchool.Sum(x => x);
    }

    private void Tick(int days)
    {
        for (int i = 1; i <= days; i++)
        {
            long futureSpawn = fishSchool.First();
            fishSchool.RemoveAt(0);
            fishSchool.Add(0);
            if (futureSpawn > 0)
            {
                fishSchool[8] = futureSpawn;
                fishSchool[6] += futureSpawn;
            }
        }     
    }

    private void Debug_PrintState(int day = 0)
    {
        Console.WriteLine($"After {day} Days : {string.Join(",", fishes)}");
    }
}

