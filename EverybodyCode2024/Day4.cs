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
    public class Day4 : BaseDayEC
    {
        public Day4()
        {
            Day = "4";
            Answer1 = "84";
            Answer2 = "919880";
            Answer3 = "129441494";
        }

        protected override string Solution1(string[] input)
        {
            return LevelNails(input);
        }

        private static string LevelNails(string[] input)
        {
            int lowest = int.MaxValue;
            List<int> nails = new List<int>();
            foreach (var item in input)
            {
                nails.Add(int.Parse(item));
                if (lowest > nails[^1])
                    lowest = nails[^1];
            }

            int hits = 0;
            foreach (var item in nails)
            {
                hits += item - lowest;
            }

            return hits.ToString();
        }

        protected override string Solution2(string[] input)
        {
            return LevelNails(input);
        }

        protected override string Solution3(string[] input)
        {
            List<int> nails = new List<int>();
            foreach (var item in input)
            {
                nails.Add(int.Parse(item));
            }

            int lowest = int.MaxValue;
            foreach (var target in nails){
                int hits = 0;
                foreach (var nail in nails)
                {
                    if(nail >= target)                    
                        hits += nail - target;
                    else
                        hits += target - nail;
                }
                if (lowest > hits)
                    lowest = hits;
            }

            return lowest.ToString();
        }
    }
}
