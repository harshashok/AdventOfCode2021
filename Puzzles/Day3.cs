namespace AdventOfCode2021
{
    using System;
    using System.Collections;
    using System.Linq;

    public class Day3
    {
        /**
        * SOLUTION :  2583164
        **/
        private static string inputFile = @"./resources/day3_input.txt";
        private static string sampleFile = @"./resources/sample3.txt";

        [Solution("Day3", "1")]
        public int solvePart1()
        {
            ReadInputFile f = new ReadInputFile(FileConstants.INPUT_DAY_3);
            BitArray[] bitsArray = f.ReadInputToByteArray();

            int inputLength = f.counter;
            int arraySize = bitsArray[0].Length;

            int[] sum = new int[arraySize];

            //Find the sum of each column
            for (int i = 0; i < inputLength; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    sum[j] += bitsArray[i].Get(j) ? 1 : 0;
                }               
            }

            //Set the binary val for each sum
            for (int i = 0; i < sum.Length; i++)
            {
                if(sum[i] >= inputLength/2)
                {
                    sum[i] = 1;
                }
                else 
                {
                    sum[i] = 0;                
                }
            }

            string result = string.Join("", sum);
            int gammaRate = Convert.ToInt32(result, 2);

            for (int a = 0; a < sum.Length; a++)
            {
                if (sum[a] == 1) 
                    sum[a] = 0;
                else 
                    sum[a] = 1;
            }

            result = string.Join("", sum);
            int epsilonRate = Convert.ToInt32(result, 2);
            
            return gammaRate*epsilonRate;
        }

        /**
         * SOLUTION : 2784375
         */
        [Solution("Day3", "2")]
        public int solvePart2()
        {
            ReadInputFile f = new ReadInputFile(FileConstants.INPUT_DAY_3);
            BitArray[] bitsArray = f.ReadInputToByteArray();
            BitArray[] oxygenRatings = new BitArray[bitsArray.Length];
            BitArray[] carbonRatings = new BitArray[bitsArray.Length];
            bitsArray.CopyTo(oxygenRatings, 0);
            bitsArray.CopyTo(carbonRatings, 0);

            int j = 0;
            while(oxygenRatings.Length > 1)
            {
                int listLength = oxygenRatings.Length;

                //counters to calculate column sums of 0's and 1's
                int total_zeroes = 0;
                int total_ones = 0;

                ArrayList ones_array = new ArrayList();
                ArrayList zero_array = new ArrayList();

                //Find the sum of each column
                for (int i = 0; i < listLength; i++)
                {
                    if (oxygenRatings[i].Get(j))
                    {
                        ones_array.Add(oxygenRatings[i]);
                        total_ones++;
                    }
                    else
                    {
                        zero_array.Add(oxygenRatings[i]);
                        total_zeroes++;
                    }
                }

                if(oxygenRatings.Length > 1)
                {
                    if (total_ones >= total_zeroes)
                    {
                        oxygenRatings = ones_array.Cast<BitArray>().ToArray();
                    }
                    else
                    {
                        oxygenRatings = zero_array.Cast<BitArray>().ToArray();
                    }
                }
                j++;
            }

            j = 0;
            while (carbonRatings.Length > 1)
            {
                int listLength = carbonRatings.Length;

                //counters to calculate column sums of 0's and 1's
                int total_zeroes = 0;
                int total_ones = 0;

                ArrayList ones_array = new ArrayList();
                ArrayList zero_array = new ArrayList();

                //Find the sum of each column
                for (int i = 0; i < listLength; i++)
                {
                    if (carbonRatings[i].Get(j))
                    {
                        ones_array.Add(carbonRatings[i]);
                        total_ones++;
                    }
                    else
                    {
                        zero_array.Add(carbonRatings[i]);
                        total_zeroes++;
                    }
                }

                if(carbonRatings.Length > 1)
                {
                    if (total_zeroes <= total_ones)
                    {
                        carbonRatings = zero_array.Cast<BitArray>().ToArray();
                    }
                    else
                    {
                        carbonRatings = ones_array.Cast<BitArray>().ToArray();
                    }
                }

                j++;
            }

            //copying the result because converter does something funky on mac intel processors.
            int[] res = new int[oxygenRatings[0].Length];
            for (int a = 0; a < oxygenRatings[0].Length; a++)
            {
                if (oxygenRatings[0].Get(a))
                    res[a] = 1;
                else
                    res[a] = 0;
            }

            int[] res2 = new int[carbonRatings[0].Length];
            for (int a = 0; a < carbonRatings[0].Length; a++)
            {
                if (carbonRatings[0].Get(a))
                    res2[a] = 1;
                else
                    res2[a] = 0;
            }

            string result = string.Join("", res);
            //Console.WriteLine("resultString o2 : {0}", result);
            //Console.WriteLine("resultString co2 : {0}", string.Join("", res2));

            int o2 = Convert.ToInt32(string.Join("", res), 2);
            //Console.WriteLine("O2 converted value : "+o2);
            int co2 = Convert.ToInt32(string.Join("", res2), 2);
            //Console.WriteLine("CO2 converted value : " + co2);

            return o2*co2;
        }
    }
}