using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day4 : BaseDay
    {
        public class CardGame
        {
            public List<int> winningNumber = new();
            public List<int> yourNumbers = new();

            int score = -1;
            int matching = -1;

            public int Score { 
                get {
                    if (score == -1)
                        Calculate();
                    return score; 
                } }

            public int MatchingCount { 
                get {
                    if (matching == -1)
                        Calculate();
                    return matching; 
                } }

            private void Calculate()
            {
                int score = 0;
                foreach (int i in winningNumber)
                {
                    if (yourNumbers.Contains(i))
                    {
                        matching++;
                        if (score == 0)
                            score++;
                        else
                            score *= 2;
                    }
                }
                this.score = score;
                matching++;
            }
        }

        public Day4()
        {
            Day = "4";
            Answer1 = "21959";
            Answer2 = "5132675";
        }

        protected override string Solution1(string[] input)
        {
            List<CardGame> games = Parse(input);

            int output = 0;
            foreach (CardGame game in games)
                output += game.Score;
            return output.ToString();
        }

        protected override string Solution2(string[] input)
        {
            List<CardGame> games = Parse(input);

            int output = 0;
            for (int i = 0; i < games.Count; i++)
            {
                output++;
                output +=Part2(games, i);
            }
            return output.ToString();
        }

        public int Part2(List<CardGame> games, int currentIndex)
        {
            int output = 0;
            for (int i = 0; i < games[currentIndex].MatchingCount; i++)
            {
                output += Part2(games, currentIndex + i + 1);
            }
            return output + games[currentIndex].MatchingCount;
        }

        private List<CardGame> Parse(string[] input)
        {
            List<CardGame> output = new List<CardGame>();
            foreach (var item in input)
            {
                var card = item.Replace("  ", " ").Split(": ")[1].Split(" | ");

                output.Add(new CardGame());

                foreach (var num in card[0].Split(" "))
                    output[^1].winningNumber.Add(int.Parse(num));
                foreach (var num in card[1].Split(" "))
                    output[^1].yourNumbers.Add(int.Parse(num));
            }

            return output;
        }
    }
}
