namespace AdventOfCode2021
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    public class ReadInputFile
    {

        string filePath; 
        public List<string> lines;
        public string input;

        public int counter;

        public ReadInputFile(string filepath)
        {
            this.filePath = filepath;
            this.counter = 0;
            input = File.ReadAllText(filepath);
            lines = new List<string> (File.ReadLines(filepath)); 
            this.counter = lines.Count;
        }

        public ReadInputFile(string filepath, int type)
        {
            if(type == 0)
            {

            }
        }

        public int[] ReadInputToIntArray()
        {
            int[] intArray = new int[this.counter];
            intArray = lines.Select(x => Int32.Parse(x)).ToArray();
            return intArray;
        }

        public BitArray[] ReadInputToByteArray()
        {            
            int x = this.counter;
            int y = lines.First().Length;
            string str = lines.First();
            Console.WriteLine("lines : "+x + " size : "+y );
            var bits = new BitArray(str.Select(s => s == '1').ToArray());

            BitArray[] bitsArray = new BitArray[x];
            int z = 0;

            foreach (string line in lines)
            {
                bitsArray[z] = new BitArray(line.Select(s => s == '1').ToArray());
                z++;
            }
            
            for(int i = 0; i < 10; i++)
            {
                bits = bitsArray[i];
                foreach(Object o in bits)
                {
                    Console.Write((bool)o ? '1' : '0');
                }
                Console.WriteLine();
            }
            return bitsArray;
        }
    }
}