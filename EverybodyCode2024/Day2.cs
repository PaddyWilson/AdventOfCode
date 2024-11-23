using AOC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EverybodyCode2024
{
    //the basic layout of for a new Day
    public class Day2 : BaseDayEC
    {
        public Day2()
        {
            Day = "2";
            Answer1 = "34";
            Answer2 = "5165";
            Answer3 = "12076";
        }

        protected override string Solution1(string[] input)
        {
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
                        if (c)
                            tcount++;
                }
                count += tcount;
            }
            return count.ToString();
        }

        protected override string Solution3(string[] input)
        {
            List<string> runes = input[0].Split(':')[1].Split(',').ToList();

            int sizeX = input.Length - 2;
            int sizeY = input[2].Length;

            bool[,] visited = new bool[sizeX, sizeY];
            char[,] text = new char[sizeX, sizeY];
            for (int x = 2; x < input.Length; x++)
                for (int y = 0; y < sizeY; y++)
                    text[x - 2, y] = input[x][y];

            //loop 4 times for 4 rotations
            for (int rotation = 0; rotation < 4; rotation++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    for (int y = 0; y < sizeY; y++)
                    {
                        foreach (string rune in runes)
                        {
                            //rune to big to match with anything
                            if (rune.Length > sizeY)
                                continue;

                            int match = 0;
                            for (int i = 0; i < rune.Length; i++)
                            {
                                int tY = y + i;
                                //loop around only on the left and right of original rotation
                                if (tY >= sizeY && rotation % 2 == 0)
                                    tY -= sizeY;

                                //oob of array or letter does not match
                                if (tY >= sizeY || text[x, tY] != rune[i])
                                    break;
                                match++;
                            }
                            // found match
                            if (match == rune.Length)
                            {
                                for (int i = 0; i < rune.Length; i++)
                                {
                                    int tY = y + i;
                                    if (tY >= sizeY)
                                        tY -= sizeY;
                                    visited[x, tY] = true;
                                }
                            }
                        }
                    }
                }
                visited = Helpers.Rotate(visited, sizeX, sizeY);
                text = Helpers.Rotate(text, sizeX, sizeY);

                int tempX = sizeX;
                sizeX = sizeY;
                sizeY = tempX;
            }

            int count = 0;
            for (int x = 0; x < sizeX; x++)
                for (int y = 0; y < sizeY; y++)
                    if (visited[x, y])
                        count++;
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

                            for (int k = 0; k < rune.Length; k++)
                                count[i + k] = true;
                    }
                }
            }
        }
    }
}
