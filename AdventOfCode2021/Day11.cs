using AOC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2021
{
	public class Day11 : BaseDay
	{
		public Day11()
		{
			Day = "11";
			Answer1 = "1691";
			Answer2 = "216";
		}

		protected override string Solution1(string[] input)
		{
			//using oversized array so i don't have handle array bounds
			int[,] octopus = new int[input[0].Length + 2, input.Length + 2];

			for (int y = 0; y < input.Length + 2; y++)
				for (int x = 0; x < input[0].Length + 2; x++)
					octopus[x, y] = int.MinValue;

			for (int y = 1; y <= input.Length; y++)
				for (int x = 1; x <= input[0].Length; x++)
					octopus[x, y] = int.Parse(input[y - 1][x - 1].ToString());

			int flashes = 0;

			int simSteps = 0;
			while (simSteps < 100)
			{
				int[,] octopusTemp = new int[input[0].Length + 2, input.Length + 2];
				//the energy level of each octopus increases by 1
				for (int y = 1; y <= input.Length; y++)
					for (int x = 1; x <= input[0].Length; x++)
						octopus[x, y] = octopus[x, y] + 1;

				octopusTemp = octopus;

				int flashCount = 0;
				int flashCountLast = -1;
				while (flashCount != flashCountLast)
				{
					flashCountLast = flashCount;
					flashCount = 0;

					for (int y = 1; y <= input.Length; y++)
					{
						for (int x = 1; x <= input[0].Length; x++)
						{
							if (octopus[x, y] > 9)
							{
								flashCount++;
								octopusTemp[x, y] = 0;
								//top row
								for (int i = x - 1; i < x + 2; i++)
									if (octopus[i, y - 1] != 0)
										octopusTemp[i, y - 1]++;
								//middle
								for (int i = x - 1; i < x + 2; i++)
									if (octopus[i, y] != 0)
										octopusTemp[i, y]++;
								//bottom
								for (int i = x - 1; i < x + 2; i++)
									if (octopus[i, y + 1] != 0)
										octopusTemp[i, y + 1]++;
							}
							else if (octopus[x, y] == 0)
								flashCount++;
						}
					}
					octopus = octopusTemp;
				}

				flashCount = 0;
				//count flashes
				for (int y = 1; y <= input.Length; y++)
					for (int x = 1; x <= input[0].Length; x++)
						if (octopus[x, y] == 0)
							flashCount++;

				flashes += flashCount;

				octopus = octopusTemp;
				simSteps += 1;
			}

			return flashes.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int[,] octopus = new int[input[0].Length + 2, input.Length + 2];

			for (int y = 0; y < input.Length + 2; y++)
				for (int x = 0; x < input[0].Length + 2; x++)
					octopus[x, y] = int.MinValue;

			for (int y = 1; y <= input.Length; y++)
				for (int x = 1; x <= input[0].Length; x++)
					octopus[x, y] = int.Parse(input[y - 1][x - 1].ToString());

			int simSteps = 0;
			while (true)
			{
				int[,] octopusTemp = new int[input[0].Length + 2, input.Length + 2];
				//the energy level of each octopus increases by 1
				for (int y = 1; y <= input.Length; y++)
					for (int x = 1; x <= input[0].Length; x++)
						octopus[x, y] = octopus[x, y] + 1;

				octopusTemp = octopus;

				int flashCount = 0;
				int flashCountLast = -1;
				while (flashCount != flashCountLast)
				{
					flashCountLast = flashCount;
					flashCount = 0;

					for (int y = 1; y <= input.Length; y++)
					{
						for (int x = 1; x <= input[0].Length; x++)
						{
							if (octopus[x, y] > 9)
							{
								flashCount++;
								octopusTemp[x, y] = 0;
								//top row
								for (int i = x - 1; i < x + 2; i++)
									if (octopus[i, y - 1] != 0)
										octopusTemp[i, y - 1]++;
								//middle
								for (int i = x - 1; i < x + 2; i++)
									if (octopus[i, y] != 0)
										octopusTemp[i, y]++;
								//bottom
								for (int i = x - 1; i < x + 2; i++)
									if (octopus[i, y + 1] != 0)
										octopusTemp[i, y + 1]++;
							}
							else if (octopus[x, y] == 0)
								flashCount++;
						}
					}
					octopus = octopusTemp;
				}

				flashCount = 0;
				//count flashes
				for (int y = 1; y <= input.Length; y++)
					for (int x = 1; x <= input[0].Length; x++)
						if (octopus[x, y] == 0)
							flashCount++;

				octopus = octopusTemp;

				simSteps += 1;

				if (flashCount == (input.Length * input[0].Length))
					break;

				//flashes += flashCount;
			}

			return simSteps.ToString();
		}
	}
}
