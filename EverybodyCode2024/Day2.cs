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
    public class Day2 : BaseDay
    {
        public Day2()
        {
            Day = "2";
            Answer1 = "34";
            Answer2 = "";
        }

        protected override string Solution1(string[] input)
        {
            //input = ReadInput("everybody_codes_e2024_q02_p1.txt");

            List<string> runes = input[0].Split(':')[1].Split(',').ToList();
            List<string> words = input[2].Replace(",", "").Replace(".", "").Split(' ').ToList();

            int count = 0;
            foreach (string rune in runes)
            {
                foreach (var word in words)
                {
                    count += CountRunes(word, rune);
                }
            }
            return count.ToString();
        }

        protected override string Solution2(string[] input)
        {
            //input = ReadInput("everybody_codes_e2024_q02_p3.txt");

            List<string> runes = input[0].Split(':')[1].Split(',').ToList();

            int count = 0;
            for (int i = 2; i < input.Length; i++)
            {
                int tcount = 0;
                List<string> words = input[i].Replace(",", "").Replace(".", "").Split(' ').ToList();
                foreach (var word in words)
                {
                    List<bool> letterCount = new List<bool>();
                    foreach (var c in word)
                        letterCount.Add(false);

                    foreach (string rune in runes)
                    {
                        CountSymbols(word, rune, letterCount);
                        var reverse = rune.ToCharArray();
                        Array.Reverse(reverse);
                        CountSymbols(word, new string(reverse), letterCount);
                    }

                    foreach (var c in letterCount)
                        if(c)
                            tcount++;
                }
                count += tcount;
            }
            return count.ToString();
        }

        private int CountRunes(string word, string rune)
        {
            int count = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == rune[0])
                {
                    for (int j = 0; j < rune.Length; j++)
                    {
                        if (i + j >= word.Length)
                            break;
                        if (word[i + j] != rune[j])
                            break;

                        if (j == rune.Length - 1)
                            count++;
                    }
                }
            }
            return count;
        }

        private void CountSymbols(string word, string rune, List<bool> count)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == rune[0])
                {
                    for (int j = 0; j < rune.Length; j++)
                    {
                        if (i + j >= word.Length)
                            break;
                        else if (word[i + j] != rune[j])
                            break;
                        else if (j == rune.Length - 1)
                            
                            for(int k = 0; k < rune.Length; k++)
                                count[i + k] = true;
                    }
                }
            }
        }
    }
}
