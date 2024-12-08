using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day8 : BaseDay
	{
		public Day8()
		{
			Day = "8";
			Answer1 = "361";
			Answer2 = "1249";
		}

		protected override string Solution1(string[] input)
		{
			Point min = new Point(0, 0);
			Point max = Helpers.ParseInputToArray(input, out char[,] map);
			char[,] visited = new char[max.X, max.Y];

			for (int baseY = 0; baseY < max.Y; baseY++)
			{
				for (int baseX = 0; baseX < max.X; baseX++)
				{
					if (map[baseX, baseY] == '.')
						continue;

					for (int searchY = 0; searchY < max.Y; searchY++)
					{
						for (int searchX = 0; searchX < max.X; searchX++)
						{
							if (baseX == searchX && baseY == searchY)
								continue;

							if (map[searchX, searchY] != map[baseX, baseY])
								continue;

							Point diff = new Point(searchX - baseX, searchY - baseY);
							Point anti1 = new Point(baseX, baseY) - diff;
							Point anti2 = new Point(searchX, searchY) + diff;

							if (anti1.InBounds(min, max))
								visited[anti1.X, anti1.Y] = '#';
							if (anti2.InBounds(min, max))
								visited[anti2.X, anti2.Y] = '#';
						}
					}
				}
			}

			return Helpers.GetCountOfMatchs(visited)['#'].ToString();
		}

		protected override string Solution2(string[] input)
		{            
            Point min = new Point(0, 0);
			Point max = Helpers.ParseInputToArray(input, out char[,] map);
			char[,] visited = new char[max.X, max.Y];

			for (int baseY = 0; baseY < max.Y; baseY++)
			{
				for (int baseX = 0; baseX < max.X; baseX++)
				{
					if (map[baseX, baseY] == '.')
						continue;

					for (int searchY = 0; searchY < max.Y; searchY++)
					{
						for (int searchX = 0; searchX < max.X; searchX++)
						{
							if (baseX == searchX && baseY == searchY)
								continue;

							if (map[searchX, searchY] != map[baseX, baseY])
								continue;

							Point diff = new Point(searchX - baseX, searchY - baseY);
							Point anti1 = new Point(baseX, baseY);
							Point anti2 = new Point(searchX, searchY);

							while (anti1.InBounds(min, max))
							{
								visited[anti1.X, anti1.Y] = '#';
								anti1 -= diff;
							}
							while (anti2.InBounds(min, max))
							{
								visited[anti2.X, anti2.Y] = '#';
								anti2 += diff;
							}
						}
					}
				}
			}
			
			return Helpers.GetCountOfMatchs(visited)['#'].ToString();
		}
	}
}
