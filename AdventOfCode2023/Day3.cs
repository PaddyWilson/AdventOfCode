using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day3 : BaseDay
    {
        public Day3()
        {
            Day = "3";
            Answer1 = "530849";
            Answer2 = "84900879";
        }

        List<char> symbols = new List<char>();

        protected override string Solution1(string[] input)
        {
            List<List<char>> engineData = Parse(input);
            List<(string, int, int)> partNumbers = GetValidPartNumbers(engineData);

            int output = 0;
            foreach (var item in partNumbers)
                output += int.Parse(item.Item1);

            return output.ToString();
        }

        protected override string Solution2(string[] input)
        {
            List<List<char>> engineData = Parse(input);
            List<(string, int, int)> partNumbers = GetValidPartNumbers(engineData);

            int output = 0;

            for (int x = 0; x < engineData.Count; x++)
            {
                for (int y = 0; y < engineData[x].Count; y++)
                {
                    if (engineData[x][y] != '*')
                        continue;

                    List<int> nums = new();

                    foreach (var item in partNumbers)
                    {
                        if (item.Item2 != x || item.Item3 != y)
                            continue;

                        nums.Add(int.Parse(item.Item1));
                    }

                    if (nums.Count == 2)
                        output += (nums[0] * nums[1]);
                }
            }

            return output.ToString();
        }

        private List<List<char>> Parse(string[] input)
        {
            List<List<char>> engineData = new();

            foreach (var item in input)
            {
                engineData.Add(new List<char>());
                foreach (var c in item)
                {
                    engineData[^1].Add(c);

                    if ((c >= '0' && c <= '9') || c == '.')
                        continue;
                    else
                    {
                        if (!symbols.Contains(c))
                            symbols.Add(c);
                    }
                }
            }
            return engineData;
        }

        //gets list of valid parts and position of symbol
        public List<(string, int, int)> GetValidPartNumbers(List<List<char>> engineData)
        {
            List<(string, int, int)> allPartNumbers = new();

            string number = "";
            for (int x = 0; x < engineData.Count; x++)
            {
                for (int y = 0; y < engineData[x].Count; y++)
                {
                    //outside range
                    if (engineData[x][y] < '0' || engineData[x][y] > '9')
                        continue;

                    //in range
                    number += engineData[x][y].ToString();

                    //add possible part number
                    if (y == engineData[x].Count - 1 ||
                        symbols.Contains(engineData[x][y + 1]) ||
                        engineData[x][y + 1] == '.')
                    {
                        allPartNumbers.Add((number, x, y));
                        number = "";
                    }
                }
            }

            //test for valid part numbers
            List<(string, int, int)> partNumbers = new();
            foreach (var item in allPartNumbers)
            {
                var val = ValidPart(engineData, item.Item1, item.Item2, item.Item3 + 1);
                if (val.Item1)
                    partNumbers.Add((item.Item1, val.Item2, val.Item3));
            }

            return partNumbers;
        }

        //test part for symbols
        //returns position of symbol for part
        public (bool, int, int) ValidPart(List<List<char>> engineData, string partNumber, int x, int y)
        {
            int min = y - partNumber.Length - 1;
            int max = y;
            //top
            for (int i = min; i <= max; i++)
            {
                //oob
                if (i < 0 || x - 1 < 0 || i >= engineData[0].Count)
                    continue;

                if (symbols.Contains(engineData[x - 1][i]))
                    return (true, x - 1, i);
            }

            //center
            if (min >= 0 && symbols.Contains(engineData[x][min]))//left
                return (true, x, min);
            if (max < engineData[0].Count && symbols.Contains(engineData[x][max]))//right
                return (true, x, max);

            //bottom
            for (int i = min; i <= max; i++)
            {
                //oob
                if (i < 0 || x + 1 >= engineData.Count || i >= engineData[0].Count)
                    continue;

                if (symbols.Contains(engineData[x + 1][i]))
                    return (true, x + 1, i);
            }
            return (false, -1, -1);
        }
    }
}
