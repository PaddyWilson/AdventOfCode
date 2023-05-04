﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
	public class Point
	{
		public int X { get; set; } 
		public int Y { get; set; }

		public Point()
		{
			X = 0;
			Y = 0;
		}

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Point(Point p)
		{
			X = p.X;
			Y = p.Y;
		}

		public bool InBounds(Point size)
		{
			return InBounds(new Point(0, 0), size);
		}

		public bool InBounds(Point start, Point end)
		{
			return (X >= start.X && Y >= start.Y && X < end.X && Y < end.Y);
		}

		public override string ToString()
		{
			return $"{X} {Y}";
		}
	}
}
