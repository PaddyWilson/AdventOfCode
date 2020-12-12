using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day12 : BaseDay
	{
		public Day12()
		{
			Day = "12";
			Answer1 = "521";
			Answer2 = "22848";
		}

		protected override string Solution1(string[] input)
		{
			int shipX = 0;
			int shipY = 0;

			char shipDirection = 'E';

			foreach (var direction in input)
			{
				int length = int.Parse(direction.Substring(1));
				char action = direction[0];

				if (action == 'F')
				{
					if (shipDirection == 'E')
						shipX += length;
					else if (shipDirection == 'W')
						shipX -= length;
					else if (shipDirection == 'N')
						shipY -= length;
					else if (shipDirection == 'S')
						shipY += length;
				}
				else if (action == 'E')
					shipX += length;
				else if (action == 'W')
					shipX -= length;
				else if (action == 'N')
					shipY -= length;
				else if (action == 'S')
					shipY += length;
				else if (action == 'L')
				{
					for (int i = length; i > 0; i -= 90)
					{
						if (shipDirection == 'E')
							shipDirection = 'N';
						else if (shipDirection == 'W')
							shipDirection = 'S';
						else if (shipDirection == 'N')
							shipDirection = 'W';
						else if (shipDirection == 'S')
							shipDirection = 'E';
					}
				}
				else if (action == 'R')
				{
					for (int i = length; i > 0; i -= 90)
					{
						if (shipDirection == 'E')
							shipDirection = 'S';
						else if (shipDirection == 'W')
							shipDirection = 'N';
						else if (shipDirection == 'N')
							shipDirection = 'E';
						else if (shipDirection == 'S')
							shipDirection = 'W';
					}
				}
			}

			if (shipX < 0)
				shipX *= -1;
			if (shipY < 0)
				shipY *= -1;

			return (shipX + shipY).ToString();
		}

		protected override string Solution2(string[] input)
		{
			int shipX = 0;
			int shipY = 0;

			int waypointX = 10;
			int waypointY = 1;

			foreach (var direction in input)
			{
				int length = int.Parse(direction.Substring(1));
				char action = direction[0];

				if (action == 'F')
				{
					shipX += waypointX * length;
					shipY += waypointY * length;
				}
				else if (action == 'E')
					waypointX += length;
				else if (action == 'W')
					waypointX -= length;
				else if (action == 'N')
					waypointY += length;
				else if (action == 'S')
					waypointY -= length;
				else if (action == 'L')
				{
					int temp;
					if (length == 90)
					{
						temp = waypointX;
						waypointX = waypointY * -1;
						waypointY = temp;
					}
					else if (length == 180)
					{
						waypointY *= -1;
						waypointX *= -1;
					}
					else if (length == 270)
					{
						temp = waypointX;
						waypointX = waypointY;
						waypointY = temp * -1;
					}
				}
				else if (action == 'R')
				{
					int temp;
					if (length == 90)
					{
						temp = waypointX;
						waypointX = waypointY;
						waypointY = temp * -1;
					}
					else if (length == 180)
					{
						waypointY *= -1;
						waypointX *= -1;
					}
					else if (length == 270)
					{
						temp = waypointX;
						waypointX = waypointY * -1;
						waypointY = temp;
					}
				}
			}

			if (shipX < 0)
				shipX *= -1;
			if (shipY < 0)
				shipY *= -1;

			return (shipX + shipY).ToString();
		}
	}
}
