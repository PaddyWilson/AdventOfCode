using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day11 : BaseDay
    {
        public Day11()
        {
            Day = "11";
            Answer1 = "10173804";
            Answer2 = "634324905172";
        }
        protected override string Solution1(string[] input)
        {
            List<List<char>> map = MakeMap(input);
            List<(long, long)> galaxy = GetGalaxyCords(map,1);
            return GetDistances(galaxy).ToString();
        }

        protected override string Solution2(string[] input)
        {
            List<List<char>> map = MakeMap(input);
            List<(long, long)> galaxy = GetGalaxyCords(map,999999);            

            return GetDistances(galaxy).ToString();
        }

        long GetDistances(List<(long, long)> galaxy)
        {
            long outputSum = 0;
            for (int i = 0; i < galaxy.Count; i++)
            {
                for (int j = i + 1; j < galaxy.Count; j++)
                {
                    long x = galaxy[i].Item1 - galaxy[j].Item1;
                    long y = galaxy[i].Item2 - galaxy[j].Item2;

                    if (x < 0)
                        x *= -1;
                    if (y < 0)
                        y *= -1;

                    outputSum += x + y;
                }
            }
            return outputSum;
        }

        List<(long, long)> GetGalaxyCords(List<List<char>> map, long spaceing = 1)
        {
            List<(long, long)> points = new List<(long, long)>();

            long yOffset = 0;

            Console.WriteLine();

            for (int y = 0; y < map[0].Count; y++)
            {
                //x test
                long countX = 0;
                for (int j = 0; j < map.Count; j++)
                {
                    if (map[y][j] == '#')
                        countX++;
                }
                if (countX == 0)
                {
                    yOffset += spaceing;
                }
                long xOffset = 0;

                for (int x = 0; x < map.Count; x++)
                {
                    //y test
                    int countY = 0;
                    for (int j = 0; j < map.Count; j++)
                    {
                        if (map[j][x] == '#')
                            countY++;
                    }
                    if (countY == 0)
                        xOffset += spaceing;

                    if (map[y][x] == '#')
                        points.Add((x + xOffset, y + yOffset));

                }
            }
            return points;
        }

        List<List<char>> MakeMap(string[] input, int expandAmount = 1)
        {
            List<List<char>> map = new();

            foreach (string s in input)
            {
                map.Add(new List<char>());
                foreach (char b in s)
                    map[^1].Add(b);
            }
            return map;
        }


    }
}
