namespace AdventOfCode2021
{
    using System;

    public class Day2
    {
        /**
        * SOLUTION :  1815044
        **/
        private static string inputFile = @"./resources/day2_input.txt";

        public int solve()
        {
            ReadInputFile f = new ReadInputFile(FileConstants.INPUT_DAY_2);            
            int horizontal = 0;
            int depth = 0;

            foreach (string row in f.lines)
            {
                string[] command = row.Split(' ', StringSplitOptions.None);
                
                switch(command[0])
                {
                    case "forward" :
                        horizontal += Int32.Parse(command[1]);
                        break;
                    case "up" :
                        depth -= Int32.Parse(command[1]);
                        break;
                    case "down" : 
                        depth += Int32.Parse(command[1]);
                        break;
                    default: 
                        throw new ArgumentException("This should not happen");
                }
            }

            return horizontal*depth;
        }
    }
}