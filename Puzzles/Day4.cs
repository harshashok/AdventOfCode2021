using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day4
    {

        IEnumerable<int> bingoNumbers;
        List<Board> boards;
        List<Soln> winningBoardsInOrder = new List<Soln>();
        public Day4()
        {
            boards = new List<Board>();
            winningBoardsInOrder = new List<Soln>();
        }

        [Solution("Day4", "1")]
        public int solve1()
        {
            ReadBoardData();
            PlayBingo();
            return winningBoardsInOrder.FirstOrDefault().finalScore();
        }

        [Solution("Day4", "2")]
        public int solve2()
        {

            ReadBoardData();
            PlayBingo();
            var t = winningBoardsInOrder.LastOrDefault().finalScore();
            return t;
        }

        private void ReadBoardData()
        {
            ReadInputFile f = new ReadInputFile(FileConstants.INPUT_DAY_4);

            List<string> lines = f.lines;
            bingoNumbers = lines.First().Split(',').Select(x => Int32.Parse(x));
            lines.RemoveAt(0);

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

                    if (i == 5)
                    {
                        boards.Add(b);
                        b = new Board(5);
                        i = 0;
                    }
                }
            }

            Console.WriteLine("Number of boards : {0}", boards.Count);
        }

        private List<Soln> PlayBingo()
        {
            var nextNumberGenerator = GetNextBingoNumber().GetEnumerator();
            var _ = nextNumberGenerator.MoveNext;
            //int number = nextNumberGenerator.Current;
            while (nextNumberGenerator.MoveNext())
            {
                int number = nextNumberGenerator.Current;
                foreach (Board b in boards.ToArray())
                {
                    //mark number in board and increment counters for each.
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (b.board[i][j].Item1 == number)
                            {
                                b.board[i][j].Item2 = true;
                                b.row_counter[i] += 1;
                                b.col_counter[j] += 1;

                                for (int x = 0; x < 5; x++)
                                {
                                    if (b.row_counter[x] >= 5 || b.col_counter[x] >= 5)
                                    {
                                        //return new Soln(b, number);
                                        winningBoardsInOrder.Add(new Soln(b, number));
                                        boards.Remove(b);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return winningBoardsInOrder;
        }

        private IEnumerable<int> GetNextBingoNumber()
        {
            foreach (int num in bingoNumbers)
            {
                yield return num;
            }
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

        internal class Soln
        {
            public Board winner { get; }
            public int winningNumber;

            internal Soln(Board board, int win)
            {
                this.winner = board;
                this.winningNumber = win;
            }

            public int calculateUnmarkedSum()
            {
                var board = winner.board;
                int sum = 0;
                for(int i = 0; i < 5; i++)
                {
                    for(int j = 0; j< 5; j++)
                    {
                        if(board[i][j].Item2 == false)
                        {
                            sum += board[i][j].Item1;
                        }
                    }
                }

                return sum;
            }

            public int finalScore()
            {
                return calculateUnmarkedSum() * winningNumber;
            }
        }
    }
}

