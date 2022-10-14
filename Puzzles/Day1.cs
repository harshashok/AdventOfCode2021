namespace AdventOfCode2021.Puzzles
{
    public class Day1 : Solution
    {
        /**
        * SOLUTION : 1477 
        **/
        private static string inputFile = @"./Resources/day1_input.txt";


        [Solution("Day1", "1")]
        public int solve()
        {
            int counter = 0; 
            ReadInputFile f = new ReadInputFile(FileConstants.INPUT_DAY_1);
            int[] arr = f.ReadInputToIntArray();
            for(int i = 1; i < arr.Length; i++)
            {
                if(arr[i] > arr[i-1])
                    counter++;
            }

            return counter;  //solution : 1477
        }

        /**
        * SOLUTION : 1523 
        **/
        [Solution("Day1", "2")]
        public int solve2()
        {
            int counter = 0;
            ReadInputFile f = new ReadInputFile(FileConstants.INPUT_DAY_1);
            int[] arr = f.ReadInputToIntArray();

            int prev = arr[0] + arr[1] + arr[2];
            int curr = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                if (i + 2 > arr.Length - 1)
                    break;

                curr = arr[i] + arr[i + 1] + arr[i + 2];
                if (curr > prev)
                {
                    counter++;
                }
                prev = curr;
            }

            return counter;
        }
    }
}