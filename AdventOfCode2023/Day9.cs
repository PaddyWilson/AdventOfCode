using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day9 : BaseDay
    {
        public Day9()
        {
            Day = "9";
            Answer1 = "1702218515";
            Answer2 = "925";
        }

        class ScaneData
        {
            List<List<int>> data = new();

            public ScaneData(string input)
            {
                data.Add(new());
                input.Split(" ").ToList().ForEach(number => data[^1].Add(int.Parse(number)));
                CalculateDiffs();
            }

            void CalculateDiffs()
            {
                while (true)
                {
                    data.Add(new List<int>());

                    for (int i = 0; i < data[^2].Count - 1; i++)
                        data[^1].Add(data[^2][i + 1] - data[^2][i]);

                    //check if all numbers are zero
                    if (data[^1].FindAll(x => { return x == 0; }).Count == data[^1].Count)
                        return;
                }
            }

            public int CalculateForward()
            {
                //add zero to end array
                data[^1].Add(0);

                for (int dataIndex = data.Count - 1; dataIndex > 0; dataIndex--)
                    data[dataIndex - 1].Add(data[dataIndex - 1][^1] + data[dataIndex][^1]);

                return data[0][^1];
            }

            public int CalculateBackward()
            {
                //add zero to front array
                data[^1].Insert(0, 0);

                for (int dataIndex = data.Count - 1; dataIndex > 0; dataIndex--)
                    data[dataIndex - 1].Insert(0, data[dataIndex - 1][0] - data[dataIndex][0]);

                return data[0][0];
            }
        }

        protected override string Solution1(string[] input)
        {
            List<ScaneData> data = new();
            input.ToList().ForEach(line => { data.Add(new ScaneData(line)); });

            int output = 0;
            foreach (var item in data)
                output += item.CalculateForward();

            return output.ToString();
        }

        protected override string Solution2(string[] input)
        {
            List<ScaneData> data = new();
            input.ToList().ForEach(line => { data.Add(new ScaneData(line)); });

            int output = 0;
            foreach (var item in data)
                output += item.CalculateBackward();

            return output.ToString();
        }
    }
}
