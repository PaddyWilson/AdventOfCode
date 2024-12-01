using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day5 : BaseDayEC
	{
		public Day5()
		{
			Day = "5";
			Answer1 = "2252";
			Answer2 = "0";
			Answer3 = "0";
		}

		protected override string Solution1(string[] input)
		{
			int row = input.Length;
			int coloum = input[0].Split(" ").Length;

			List<List<int>> numbers = new List<List<int>>();
			for (int y = 0; y < coloum; y++)
				numbers.Add(new List<int>());

			for (int x = 0; x < row; x++)
			{
				var num = input[x].Split(" ");
				for (int y = 0; y < num.Length; y++)
				{
					numbers[y].Add(int.Parse(num[y]));
				}
			}

			int maxRounds = 10;
			int currentPersonIndex = 0;

			for (int rounds = 0; rounds < maxRounds; rounds++)
			{
				currentPersonIndex = Dance(coloum, numbers, currentPersonIndex);
			}

			string output = "";
			for (int i = 0; i < coloum; i++)
				output += numbers[i][0];
			return output;
		}

		private static int Dance(int coloum, List<List<int>> numbers, int currentPersonIndex)
		{
			int nextIndex = currentPersonIndex + 1;

			if (nextIndex >= coloum) //loop around it needed
				nextIndex = 0;

			int currentPerson = numbers[currentPersonIndex][0];
			numbers[currentPersonIndex].RemoveAt(0);

			// if (numbers[nextIndex].Count > currentPerson)
			// {
			// 	numbers[nextIndex].Insert(currentPerson-1, currentPerson);
			// }
			// else
			{
				bool down = true;
				int index = 0;
				for (int i = 0; i < currentPerson; i++)
				{
					if (down)
					{
						if (i < numbers[nextIndex].Count)
							index++;
						else
							down = false;
					}
					else
					{
						if (i > 0)
							index--;
						else
							down = true;
					}
				}
				if (down)
					numbers[nextIndex].Insert(index - 1, currentPerson);
				else
					numbers[nextIndex].Insert(index, currentPerson);
			}
			currentPersonIndex = nextIndex;
			return currentPersonIndex;
		}

		protected override string Solution2(string[] input)
		{
			int row = input.Length;
			int coloum = input[0].Split(" ").Length;

			List<List<int>> numbers = new List<List<int>>();
			for (int y = 0; y < coloum; y++)
				numbers.Add(new List<int>());

			for (int x = 0; x < row; x++)
			{
				var num = input[x].Split(" ");
				for (int y = 0; y < num.Length; y++)
				{
					numbers[y].Add(int.Parse(num[y]));
				}
			}

			Dictionary<string, int> counts = new Dictionary<string, int>();

			int currentPersonIndex = 0;
			int rounds = 0;
			while(true)
			{
				rounds++;
				currentPersonIndex = Dance(coloum, numbers, currentPersonIndex);
				
				string shout = "";
				for (int i = 0; i < coloum; i++)
					shout += numbers[i][0];

				if(!counts.ContainsKey(shout))
					counts.Add(shout, 0);
				counts[shout]+=1;

				if(counts[shout] == 2024)
					return (int.Parse(shout) * rounds).ToString();
			}
		}

		protected override string Solution3(string[] input)
		{
			return "-1";
		}
	}
}
