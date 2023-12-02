using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day2 : BaseDay
    {
        public Day2()
        {
            Day = "2";
            Answer1 = "2176";
            Answer2 = "63700";
        }

        protected override string Solution1(string[] input)
        {

            Dictionary<string, int> test = new Dictionary<string, int>
            {
                { "red", 12 },
                { "green", 13 },
                { "blue", 14 }
            };


            int output = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int gameNumber = i + 1;

                string gameData = input[i].Split(": ")[1];
                string[] pulls = gameData.Split("; ");

                bool validGame = true;

                foreach (var item in pulls)
                {
                    var data = item.Split(", ");

                    foreach (var item2 in data)
                    {
                        int count = int.Parse(item2.Split(" ")[0]);
                        string color = item2.Split(" ")[1];

                        int temp;
                        if(test.TryGetValue(color, out temp))
                        {
                            if(count > temp)
                                validGame = false;
                        }
                    }
                }

                if (validGame)
                    output += gameNumber;
            }


            return output.ToString();
        }

        protected override string Solution2(string[] input)
        {
            Dictionary<string, int> test = new Dictionary<string, int>
            {
                { "red", 12 },
                { "green", 13 },
                { "blue", 14 }
            };

            int output = 0;
            for (int i = 0; i < input.Length; i++)
            {
                test["red"] = 0;
                test["green"] = 0;
                test["blue"] = 0;

                string gameData = input[i].Split(": ")[1];
                string[] pulls = gameData.Split("; ");

                bool validGame = true;

                foreach (var item in pulls)
                {
                    var data = item.Split(", ");

                    foreach (var item2 in data)
                    {
                        int count = int.Parse(item2.Split(" ")[0]);
                        string color = item2.Split(" ")[1];

                        if (test[color] < count)
                            test[color] = count;
                    }
                }                

                if (validGame)
                    output += (test["red"] * test["green"] * test["blue"]);
            }
            return output.ToString();
        }
    }
}
