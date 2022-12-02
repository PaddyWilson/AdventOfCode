using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day2 : BaseDay
	{
		public Day2()
		{
			Day = "2";
			Answer1 = "11767";
			Answer2 = "13886";
		}

		protected override string Solution1(string[] input)
		{
			int totalScore = 0;

			foreach (var item in input)
			{
				//A for Rock
				//B for Paper
				//C for Scissors

				//X for Rock 1
				//Y for Paper 2
				//Z for Scissors 3

				string[] temp = item.Split(' ');

				string p1 = temp[0];
				string p2 = temp[1];

				int score = 0;

				if (p1 == "A")
				{
					if (p2 == "Y")//won
					{
						score += 6 + GetScore(p2);
					}
					else if (p2 == "Z")//lost
					{
						score += 0 + GetScore(p2);
					}
					else//draw AX
					{
						score += 3 + GetScore(p2);
					}
				}
				else if (p1 == "B")
				{
					if (p2 == "Z")//won
					{
						score += 6 + GetScore(p2);
					}
					else if (p2 == "X")//lost
					{
						score += 0 + GetScore(p2);
					}
					else//draw BY
					{
						score += 3 + GetScore(p2);
					}
				}
				else if (p1 == "C")
				{
					if (p2 == "X")//won
					{
						score += 6 + GetScore(p2);
					}
					else if (p2 == "Y")//lost
					{
						score += 0 + GetScore(p2);
					}
					else//draw CZ
					{
						score += 3 + GetScore(p2);
					}
				}
				totalScore += score;
			}

			return totalScore.ToString();
		}

		private int GetScore(string p1)
		{
			if (p1 == "X")
				return 1;
			else if (p1 == "Y")
				return 2;
			else if (p1 == "Z")
				return 3;
			else return 0;
		}

		protected override string Solution2(string[] input)
		{
			int totalScore = 0;

			foreach (var item in input)
			{
				//A for Rock
				//B for Paper
				//C for Scissors

				//X for Lose
				//Y for Draw
				//Z for Win

				string[] temp = item.Split(' ');

				string p1 = temp[0];
				string p2 = temp[1];

				int score = 0;

				if (p1 == "A")
				{
					if (p2 == "Z")//won
					{
						score += 6 + GetScore("Y");
					}
					else if (p2 == "X")//lost
					{
						score += 0 + GetScore("Z");
					}
					else//draw Y
					{
						score += 3 + GetScore("X");
					}
				}
				else if (p1 == "B")
				{
					if (p2 == "Z")//won
					{
						score += 6 + GetScore("Z");
					}
					else if (p2 == "X")//lost
					{
						score += 0 + GetScore("X");
					}
					else//draw Y
					{
						score += 3 + GetScore("Y");
					}
				}
				else if (p1 == "C")
				{
					if (p2 == "Z")//won
					{
						score += 6 + GetScore("X");
					}
					else if (p2 == "X")//lost
					{
						score += 0 + GetScore("Y");
					}
					else//draw Y
					{
						score += 3 + GetScore("Z");
					}
				}
				totalScore += score;
			}

			return totalScore.ToString();
		}
	}
}
