using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day4
    {

        IEnumerable<int> bingoNumbers;
        public Day4()
        {
        }

        [Solution("Day4", "1")]
        public int solve1()
        {

            ReadBoardData();
            //(int, bool) t1 = (1, false);
            //var coords = new[] { (50, false), (50, true), (450, false) };

            //(int, bool)[,] result = new (int, bool)[3,2];
            //(int, bool)[][] result2 = new (int, bool)[3][];
            return 1;
        }

        private void ReadBoardData()
        {
            ReadInputFile f = new ReadInputFile(FileConstants.SAMPLE);

            List<string> lines = f.lines;
            bingoNumbers = lines.First().Split(',').Select(x => Int32.Parse(x)); //TODO : use yeild to get items
            lines.RemoveAt(0);

            List<Board> boards = new List<Board>();
            Board b = new Board(5);
            int i = 0;

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && i != 5)
                {
                    var cNumbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(x => (Int32.Parse(x.Trim()), false))
                                       .ToArray();

                    b.board[i++] = cNumbers;

                    if(i == 5)
                    {
                        boards.Add(b);
                        b = new Board(5);
                        i = 0;
                    }
                }
            }
            
            Console.WriteLine("Number of boards : {0}", boards.Count);
        }

        internal class Board
        {
            public (int, bool)[][] board;
            public int[] col_counter;
            public int[] row_counter;

            internal Board(int size)
            {
                board = new (int, bool)[size][];
                col_counter = new int[size];
                row_counter = new int[size];
            }
        }
    }
}

