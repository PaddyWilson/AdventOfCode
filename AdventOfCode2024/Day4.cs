using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    //the basic layout of for a new Day
    public class Day4 : BaseDay
    {
        public Day4()
        {
            Day = "4";
            Answer1 = "2569";
            Answer2 = "1998";
        }

        protected override string Solution1(string[] input)
        {
            int padding = 6;
            int xSize = input.Length + padding;
            int ySize = input[0].Length + padding;

            char[,] wordsearch = new char[xSize, ySize];

            for (int x = padding / 2; x < xSize - (padding / 2); x++)
            {
                for (int y = padding / 2; y < ySize - (padding / 2); y++)
                {
                    wordsearch[x, y] = input[x - (padding / 2)][y - (padding / 2)];
                }
            }

            //Helpers.PrintMatrix(wordsearch);

            int output = 0;

            for (int Rotate = 0; Rotate < 4; Rotate++)
            {
                for (int x = padding / 2; x < xSize - (padding / 2); x++)
                {
                    for (int y = padding / 2; y < ySize - (padding / 2); y++)
                    {
                        if (wordsearch[x, y] == 'X' && wordsearch[x + 1, y] == 'M' && wordsearch[x + 2, y] == 'A' && wordsearch[x + 3, y] == 'S')
                        {
                            output++;
                        }
                        if (wordsearch[x, y] == 'X' && wordsearch[x + 1, y + 1] == 'M' && wordsearch[x + 2, y + 2] == 'A' && wordsearch[x + 3, y + 3] == 'S')
                        {
                            output++;
                        }
                    }
                }
                wordsearch = Helpers.Rotate(wordsearch);

                //Helpers.PrintMatrix(wordsearch);
            }
            return output.ToString();
        }

        protected override string Solution2(string[] input)
        {
            int padding = 2;
            int xSize = input.Length + padding;
            int ySize = input[0].Length + padding;

            char[,] wordsearch = new char[xSize, ySize];

            for (int x = padding / 2; x < xSize - (padding / 2); x++)
            {
                for (int y = padding / 2; y < ySize - (padding / 2); y++)
                {
                    wordsearch[x, y] = input[x - (padding / 2)][y - (padding / 2)];
                }
            }

            int output = 0;
            for (int x = padding / 2; x < xSize - (padding / 2); x++)
            {
                for (int y = padding / 2; y < ySize - (padding / 2); y++)
                {
                    if (wordsearch[x - 1, y - 1] == 'M' && wordsearch[x, y] == 'A' && wordsearch[x + 1, y + 1] == 'S')
                    {
                        if (wordsearch[x - 1, y + 1] == 'M' && wordsearch[x, y] == 'A' && wordsearch[x + 1, y - 1] == 'S')
                        {
                            output++;
                        }
                        else if (wordsearch[x + 1, y - 1] == 'M' && wordsearch[x, y] == 'A' && wordsearch[x - 1, y + 1] == 'S')
                        {
                            output++;
                        }
                    }

                    if (wordsearch[x + 1, y + 1] == 'M' && wordsearch[x, y] == 'A' && wordsearch[x - 1, y - 1] == 'S')
                    {
                        if (wordsearch[x - 1, y + 1] == 'M' && wordsearch[x, y] == 'A' && wordsearch[x + 1, y - 1] == 'S')
                        {
                            output++;
                        }
                        else if (wordsearch[x + 1, y - 1] == 'M' && wordsearch[x, y] == 'A' && wordsearch[x - 1, y + 1] == 'S')
                        {
                            output++;
                        }
                    }
                }
            }
            return output.ToString();
        }
    }
}
