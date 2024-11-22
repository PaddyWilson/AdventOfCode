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
    public class Day1 : BaseDay
    {
        public Day1()
        {
            Day = "1";
            Answer1 = "1437";
            Answer2 = "5669";
        }

        protected override string Solution1(string[] input)
        {
            //input = ReadInput("everybody_codes_e2024_q01_p1.txt");

            int count = 0;
            foreach (var item in input[0]) 
            {
                if (item == 'A')
                    count += 0; 
                if (item == 'B')
                    count += 1; 
                if (item == 'C')
                    count += 3;
            }

            return count.ToString();
        }

        protected override string Solution2(string[] input)
        {
            //input = ReadInput("everybody_codes_e2024_q01_p2.txt");

            int count = 0;

            for (int i = 0; i < input[0].Length; i += 2) 
            {
                char mon1 = input[0][i];
                char mon2 = input[0][i + 1];

                int tcount = GetPotionCount(mon1);
                tcount += GetPotionCount(mon2);

                if(mon1 == 'x' || mon2 == 'x')
                    tcount += 0;
                else 
                    tcount += 2;
                count += tcount;
            }

            return count.ToString();
        }

        private int GetPotionCount(char mon)
        {
            if (mon == 'A')
                return 0;
            if (mon == 'B')
                return 1;
            if (mon == 'C')
                return 3;
            if (mon == 'D')
                return 5;
            return 0;
        }
    }
}
