using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;
using AOC;

namespace AdventOfCode2019
{
	public class Day3 : BaseDay
	{
		public Day3()
		{
			Day = "3";
			Answer1 = "709";
			Answer2 = "13836";
		}

		protected override string Solution1(string[] input)
		{
			//string[] wire1 = "R8,U5,L5,D3".Split(',');
			//string[] wire2 = "U7,R6,D4,L4".Split(',');

			//string[] wire1 = "R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(',');
			//string[] wire2 = "U62,R66,U55,R34,D71,R55,D58,R83".Split(',');

			//string[] wire1 = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51".Split(',');
			//string[] wire2 = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split(',');

			string[] wire1 = input[0].Split(',');
			string[] wire2 = input[1].Split(',');


			Dictionary<string, long> w1 = PlaceWires(wire1);
			Dictionary<string, long> w2 = PlaceWires(wire2);

			List<string> intersects = new List<string>();
			foreach (var item1 in w1)
			{
				foreach (var item2 in w2)
				{
					//don't include the starting point
					if (item1.Key == "0,0" && item2.Key == "0,0")
						continue;

					if (item1.Key == item2.Key)
						intersects.Add(item1.Key);
				}
			}

			//long size = 500;
			//char[,] vis = new char[size, size];
			//long offSetY = size / 2;
			//long offSetX = size / 2;
			long lowest = long.MaxValue - 1;

			foreach (var item in intersects)
			{
				long x = long.Parse(item.Split(',')[0]);
				long y = long.Parse(item.Split(',')[1]);

				//vis[offSetX + x, offSetY + y] = item;

				long temp = Math.Abs(x - 0) + Math.Abs(y - 0);
				if (temp < lowest)
					lowest = temp;
			}

			return lowest.ToString();
		}

		protected override string Solution2(string[] input)
		{

			//string[] wire1 = "R8,U5,L5,D3".Split(',');
			//string[] wire2 = "U7,R6,D4,L4".Split(',');

			//string[] wire1 = "R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(',');
			//string[] wire2 = "U62,R66,U55,R34,D71,R55,D58,R83".Split(',');

			//string[] wire1 = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51".Split(',');
			//string[] wire2 = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split(',');

			string[] wire1 = input[0].Split(',');
			string[] wire2 = input[1].Split(',');

			Dictionary<string, long> w1 = PlaceWires(wire1);
			Dictionary<string, long> w2 = PlaceWires(wire2);

			//List<string> intersects = new List<string>();

			long lowest = long.MaxValue - 1;
			foreach (var item1 in w1)
			{
				foreach (var item2 in w2)
				{
					//don't include the starting point
					if (item1.Key == "0,0" && item2.Key == "0,0")
						continue;

					if (item1.Key == item2.Key)
					{
						//long x = long.Parse(item.Split(',')[0]);
						//long y = long.Parse(item.Split(',')[1]);

						//vis[offSetX + x, offSetY + y] = item;

						long temp = item1.Value +item2.Value;
						if (temp < lowest)
							lowest = temp;
					}
				}
			}

			return lowest.ToString();
		}

		static Dictionary<string, long> PlaceWires(string[] wire)
		{
			//starting position
			long x = 0;
			long y = 0;

			Dictionary<string, long> wireView = new Dictionary<string, long>();

			//wireView.Add(PosString(x, y));

			long length = 0;
			foreach (var item in wire)
			{
				char direction = item[0];
				long amount = long.Parse(item.Substring(1, item.Length - 1));
				long tempX = x;
				long tempY = y;

				while (amount != 0)
				{
					amount--;
					length++;
					switch (direction)
					{
						case 'D': x++; break;
						case 'U': x--; break;
						case 'L': y--; break;
						case 'R': y++; break;
						default:
							break;
					}

					if (!wireView.ContainsKey(PosString(x, y)))
						wireView.Add(PosString(x, y), length);
					//else
						//wireView.Add(PosString(x, y), length);
				}
			}
			return wireView;
		}


		static string PosString(long x, long y)
		{
			return x + "," + y;
		}
	}
}
