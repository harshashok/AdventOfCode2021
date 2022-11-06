using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2021.Puzzles
{
    public class Day5
    {
        int[,] grid;
        IEnumerable<Coordinate> coordinateList;
        List<Coordinate> diagonalCoord;

        public Day5()
        {
            grid = new int[1000, 1000];
            diagonalCoord = new List<Coordinate>();

            ReadInputFile f = new ReadInputFile(FileConstants.INPUT_DAY_5);

            coordinateList = f.lines.Select(x => x.Replace("->", ",")
                                            .Split(',', StringSplitOptions.TrimEntries))
                                        .Select(p => new Coordinate(Int32.Parse(p[0]), Int32.Parse(p[1]),
                                                                    Int32.Parse(p[2]), Int32.Parse(p[3])));
        }

        [Solution("Day5", "1")]
        public int solve()
        {
            //grid[9, 9] = 9;
            foreach(var point in coordinateList)
            {
                if(point.x1 == point.x2 || point.y1 == point.y2)
                {
                    MarkGridStraight(point);
                }
                else
                {
                    diagonalCoord.Add(point);
                }
            }

            return CalculateDangerZones();
        }

        [Solution("Day5", "2")]
        public int solve2()
        {
            solve();

            foreach(var point in diagonalCoord)
            {
                MarkGridDiagonal(point);
            }

            return CalculateDangerZones();
        }

        private void MarkGridStraight(Coordinate coord)
        {
            int startPos;
            int endPos;
            bool isRow = (coord.y1 == coord.y2);

            if (isRow)
            {
                startPos = coord.x1 > coord.x2 ? coord.x2 : coord.x1;
                endPos = coord.x1 > coord.x2 ? coord.x1 : coord.x2;

                for (int i = startPos; i <= endPos; i++)
                {
                    //grid[i, coord.y1] += 1;
                    grid[coord.y1, i] += 1;
                }

            }
            else
            {
                startPos = coord.y1 > coord.y2 ? coord.y2 : coord.y1;
                endPos = coord.y1 > coord.y2 ? coord.y1 : coord.y2;

                for (int i = startPos; i <= endPos; i++)
                {
                    //grid[coord.x1, i] += 1;
                    grid[i, coord.x1] += 1;
                }
            }
        }

        private void MarkGridDiagonal(Coordinate coord)
        {
            int startX = coord.x1;
            int endX = coord.x2;
            int startY = coord.y1;
            int endY = coord.y2;

            bool xIncreasing = coord.x2 > coord.x1;
            bool yIncreasing = coord.y2 > coord.y1;
            bool flag = true;

            while (flag)
            {
                if (startX == endX && startY == endY)
                {
                    flag = false;
                }

                grid[startY, startX] += 1;
                startX = xIncreasing ? startX+1 : startX-1;
                startY = yIncreasing ? startY+1 : startY-1;
            }
        }

        private int CalculateDangerZones()
        {
            int sum = 0;
            for(int i=0; i<grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] > 1)
                        sum += 1;
                }
            }
            return sum;
        }

        private int Debug_CalculateDangerZones()
        {
            int sum = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j]);
                    if (grid[i, j] > 1)
                        sum += 1;
                }
                Console.WriteLine();
            }
            return sum;
        }   
    }

    record Coordinate (int x1, int y1, int x2, int y2);
}

