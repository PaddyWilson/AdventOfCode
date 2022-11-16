using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day18 : BaseDay
	{
		public Day18()
		{
			Day = "18";
			Answer1 = "768";
			Answer2 = "781";
		}

		protected override string Solution1(string[] input)
		{
			//over size array by 1 radius so out of bounds are easy to handle
			int gridSize = 100 + 2;
			int stepCount = 100;

			//TESTNG
			if (input.Length < gridSize - 20)
			{
				gridSize = input.Length + 2;
				stepCount = 4;
			}

			bool[,] lights = new bool[gridSize, gridSize];
			bool[,] lightsTemp = new bool[gridSize, gridSize];

			int x = 1;
			int y = 1;
			foreach (var item in input)
			{
				foreach (var c in item)
				{
					if (c == '#')
						lights[x, y] = true;
					else
						lights[x, y] = false;
					y++;
				}
				x++;
				y = 1;
			}


			for (int step = 0; step < stepCount; step++)
			{
				for (x = 1; x < gridSize - 1; x++)
				{
					for (y = 1; y < gridSize - 1; y++)
					{
						int on = 0;

						//top
						for (int i = -1; i < 2; i++)
						{
							if (lights[x + i, y - 1])
								on++;
						}

						//center
						for (int i = -1; i < 2; i++)
						{
							if (x + i == x) continue;
							if (lights[x + i, y])
								on++;
						}

						//bottom
						for (int i = -1; i < 2; i++)
						{
							if (lights[x + i, y + 1])
								on++;
						}

						if (lights[x, y])
						{
							if (on == 2 || on == 3)
								lightsTemp[x, y] = true;//stay on
							else
								lightsTemp[x, y] = false; // turn off
						}
						else if (!lights[x, y] && on == 3)
						{
							lightsTemp[x, y] = true;
						}
					}
				}

				Array.Copy(lightsTemp, lights, gridSize * gridSize);
			}

			//count on lights
			int count = 0;
			for (x = 1; x < gridSize - 1; x++)
			{
				for (y = 1; y < gridSize - 1; y++)
				{
					if (lights[x, y])
						count++;
				}
			}

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			//over size array by 1 radius so out of bounds are easy to handle
			int gridSize = 100 + 2;
			int stepCount = 100;

			//TESTNG
			if (input.Length < gridSize - 20)
			{
				gridSize = input.Length + 2;
				stepCount = 5;
			}

			bool[,] lights = new bool[gridSize, gridSize];
			bool[,] lightsTemp = new bool[gridSize, gridSize];

			int x = 1;
			int y = 1;
			foreach (var item in input)
			{
				foreach (var c in item)
				{
					if (c == '#')
						lights[x, y] = true;
					else
						lights[x, y] = false;
					y++;
				}
				x++;
				y = 1;
			}

			SetCornerLights(ref lights, gridSize);

			for (int step = 0; step < stepCount; step++)
			{
				for (x = 1; x < gridSize - 1; x++)
				{
					for (y = 1; y < gridSize - 1; y++)
					{
						int on = 0;

						//top
						for (int i = -1; i < 2; i++)
						{
							if (lights[x + i, y - 1])
								on++;
						}

						//center
						for (int i = -1; i < 2; i++)
						{
							if (x + i == x) continue;
							if (lights[x + i, y])
								on++;
						}

						//bottom
						for (int i = -1; i < 2; i++)
						{
							if (lights[x + i, y + 1])
								on++;
						}

						if (lights[x, y] && on == 2)
							lightsTemp[x, y] = true;//stay on = on
						else if (lights[x, y] && on == 3)
							lightsTemp[x, y] = true;//stay on = on
						else if (!lights[x, y] && on == 3)
							lightsTemp[x, y] = true;//off = on
						else
							lightsTemp[x, y] = false;
					}
				}
				SetCornerLights(ref lightsTemp, gridSize);
				Array.Copy(lightsTemp, lights, gridSize * gridSize);

				int l = 0;
			}

			//count on lights
			int count = 0;
			for (x = 1; x < gridSize - 1; x++)
			{
				for (y = 1; y < gridSize - 1; y++)
				{
					if (lights[x, y])
						count++;
				}
			}

			return count.ToString();
		}

		private void SetCornerLights(ref bool[,] lights, int gridsize)
		{
			lights[1, 1] = true;
			lights[1, gridsize - 2] = true;
			lights[gridsize - 2, 1] = true;
			lights[gridsize - 2, gridsize - 2] = true;
		}
	}
}
