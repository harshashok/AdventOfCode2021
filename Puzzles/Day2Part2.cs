namespace AdventOfCode2021
{
    using System;

    public class Day2Part2
    {
        /**
        * SOLUTION :  1739283308
        **/
        private static string inputFile = @"./resources/day2_input.txt";

        public int solve()
        {
            ReadInputFile f = new ReadInputFile(inputFile);            
            int horizontal = 0;
            int depth = 0;
            int aim = 0;
            foreach (string row in f.lines)
            {
                string[] command = row.Split(' ', StringSplitOptions.None);
                
                switch(command[0])
                {
                    case "forward" :
                        horizontal += Int32.Parse(command[1]);
                        depth += aim*Int32.Parse(command[1]);
                        break;
                    case "up" :
                        //depth -= Int32.Parse(command[1]);
                        aim -= Int32.Parse(command[1]);
                        break;
                    case "down" : 
                        //depth += Int32.Parse(command[1]);
                        aim += Int32.Parse(command[1]);
                        break;
                    default: 
                        throw new ArgumentException("This should not happen");
                }
            }

            return horizontal*depth;
        }
    }
}