using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AOC;

namespace AdventOfCode2020
{
	public class Day15 : BaseDay
	{
		public Day15()
		{
			Day = "15";
			Answer1 = "517";
			Answer2 = "1047739";
		}

		protected override string Solution1(string[] input)
		{
			return DoTurns(input, 2020);
		}

		protected override string Solution2(string[] input)
		{
			return DoTurns(input, 30000000);
		}
		private string DoTurns(string[] input, long maxTurns)
		{
			Dictionary<long, (long, long)> numbers = new Dictionary<long, (long, long)>();
			long turnCount = 0;
			long lastTurn = 0;
			foreach (var item in input[0].Split(','))
			{
				turnCount += 1;
				numbers.Add(long.Parse(item), (turnCount, -1));
				lastTurn = long.Parse(item);
			}

			long currentNumber;
			while (turnCount < maxTurns)
			{
				turnCount += 1;
				currentNumber = 0;
				if (numbers[lastTurn].Item2 == -1)
				{
					currentNumber = 0;
				}
				else if (numbers[lastTurn].Item2 != -1)
				{
					currentNumber = numbers[lastTurn].Item1 - numbers[lastTurn].Item2;
				}

				if (numbers.ContainsKey(currentNumber))
				{
					numbers[currentNumber] = (turnCount, numbers[currentNumber].Item1);
				}
				else
				{
					numbers[currentNumber] = (turnCount, -1);
				}
				lastTurn = currentNumber;
			}

			return lastTurn.ToString();
		}
	}
}
