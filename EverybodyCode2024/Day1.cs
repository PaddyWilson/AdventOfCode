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
    public class Day1 : BaseDayEC
    {
        public Day1()
        {
            Day = "1";
            Answer1 = "1437";
            Answer2 = "5669";
            Answer3 = "28073";
        }

        protected override string Solution1(string[] input)
        {
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

        protected override string Solution3(string[] input)
        {
            int count = 0;
            for (int i = 0; i < input[0].Length; i += 3) 
            {
                char[] mons = new char[3];
                mons[0] = input[0][i];
                mons[1] = input[0][i + 1];
                mons[2] = input[0][i + 2];

                int tcount = GetPotionCount(mons[0]);
                tcount += GetPotionCount(mons[1]);
                tcount += GetPotionCount(mons[2]);

                int x = mons.Count(m => m == 'x');
                if(x == 0)
                {
                    tcount+=6;
                }
                else if (x == 1)
                {
                    tcount+=2;
                }
                else if (x == 2)
                {
                    tcount+=0;
                }
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
