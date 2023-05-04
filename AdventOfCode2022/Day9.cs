using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day9 : BaseDay
	{
		public Day9()
		{
			Day = "9";
			Answer1 = "5960";
			Answer2 = "2327";
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<(int, int), bool> tailTouched = new Dictionary<(int, int), bool>();

			(int, int) head = (0, 0);
			(int, int) tail = (0, 0);

			//need for the first move
			if (!tailTouched.ContainsKey(tail))
				tailTouched.Add(tail, true);

			foreach (var item in input)
			{
				char dir = item[0];
				int amount = int.Parse(item.Split(" ")[1]);

				(int, int) moveWay = (0, 0);
				if (dir == 'U')
					moveWay = (moveWay.Item1 - 1, moveWay.Item2);
				else if (dir == 'D')
					moveWay = (moveWay.Item1 + 1, moveWay.Item2);
				else if (dir == 'L')
					moveWay = (moveWay.Item1, moveWay.Item2 - 1);
				else if (dir == 'R')
					moveWay = (moveWay.Item1, moveWay.Item2 + 1);

				for (int i = 0; i < amount; i++)
				{
					//var headTemp = head;//pos tail will move it not in range
					head = (head.Item1 + moveWay.Item1, head.Item2 + moveWay.Item2);

					tail = MoveTail(head, tail);

					tailTouched.TryAdd(tail, true);
				}
			}

			return tailTouched.Count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			Dictionary<(int, int), bool> tailTouched = new Dictionary<(int, int), bool>();

			List<(int, int)> positions = new List<(int, int)>();

			for (int i = 0; i < 10; i++)
				positions.Add((0, 0));

			//need for the first move
			tailTouched.Add(positions[^1], true);

			foreach (var item in input)
			{
				char dir = item[0];
				int amount = int.Parse(item.Split(" ")[1]);

				(int, int) moveWay = (0, 0);
				if (dir == 'U')
					moveWay = (moveWay.Item1 - 1, moveWay.Item2);
				else if (dir == 'D')
					moveWay = (moveWay.Item1 + 1, moveWay.Item2);
				else if (dir == 'L')
					moveWay = (moveWay.Item1, moveWay.Item2 - 1);
				else if (dir == 'R')
					moveWay = (moveWay.Item1, moveWay.Item2 + 1);

				for (int i = 0; i < amount; i++)
				{
					//var headTemp = head;//pos tail will move it not in range
					positions[0] = (positions[0].Item1 + moveWay.Item1, positions[0].Item2 + moveWay.Item2);

					for (int j = 1; j < positions.Count; j++)
						positions[j] = MoveTail(positions[j - 1], positions[j]);

					tailTouched.TryAdd(positions[^1], true);
				}
			}

			return tailTouched.Count.ToString();
		}

		private (int, int) MoveTail((int, int) head, (int, int) tail)
		{
			if (head == tail)
				return tail;

			var x = head.Item1 - tail.Item1;
			var y = head.Item2 - tail.Item2;

			if (x <= 1 && x >= -1 && y <= 1 && y >= -1)
				return tail;

			x = tail.Item1;
			y = tail.Item2;

			if (head.Item1 > tail.Item1) x++;
			if (head.Item1 < tail.Item1) x--;

			if (head.Item2 > tail.Item2) y++;
			if (head.Item2 < tail.Item2) y--;

			return (x, y);
		}

	}
}
