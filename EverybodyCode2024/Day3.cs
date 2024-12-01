using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EverybodyCode2024
{
    //the basic layout of for a new Day
    public class Day3 : BaseDayEC
    {
        public Day3()
        {
            Day = "3";
            Answer1 = "124";
            Answer2 = "2668";
            Answer3 = "10190";
        }

        protected override string Solution1(string[] input)
        {
            return FindDepths(input);
        }
        protected override string Solution2(string[] input)
        {
            return FindDepths(input);
        }

        protected override string Solution3(string[] input)
        {
            return FindDepths(input, false);
        }

        private static string FindDepths(string[] input, bool part1 = true)
        {
            int xSize = input.Length;
            int ySize = input[0].Length;
            int[,] hole = new int[xSize, ySize];
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (input[x][y] == '.')
                        hole[x, y] = 0;
                    else
                        hole[x, y] = 1;
                }
            }

            int depth = 1;
            int dug = 0;
            do
            {
                dug = 0;
                for (int x = 1; x < xSize - 1; x++)
                {
                    for (int y = 1; y < ySize - 1; y++)
                    {
                        if (hole[x, y] != depth)
                            continue;

                        int countDepths = 0;

                        if (part1)//part 1/2
                        {
                            //top
                            if (hole[x, y - 1] >= depth)
                                countDepths++;
                            //bottom
                            if (hole[x, y + 1] >= depth)
                                countDepths++;
                        }
                        else//part 3
                        {
                            //top
                            for (int i = -1; i < 2; i++)
                            {
                                if (hole[x + i, y - 1] >= depth)
                                    countDepths++;
                            }
                            //bottom
                            for (int i = -1; i < 2; i++)
                            {
                                if (hole[x + i, y + 1] >= depth)
                                    countDepths++;
                            }
                        }
                        //center
                        for (int i = -1; i < 2; i++)
                        {
                            if (hole[x + i, y] >= depth)
                                countDepths++;
                        }

                        int n = 5;
                        if (!part1)
                            n = 9;

                        if (countDepths == n)
                        {
                            hole[x, y] += 1;
                            dug++;
                        }
                    }
                }
                depth++;
            } while (dug != 0);

            int count = 0;
            for (int x = 0; x < xSize; x++)
                for (int y = 0; y < ySize; y++)
                    count += hole[x, y];
            return count.ToString();
        }
    }
}
