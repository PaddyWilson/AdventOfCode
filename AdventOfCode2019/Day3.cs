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
            string[] wire1 = input[0].Split(',');
            string[] wire2 = input[1].Split(',');

            //Dictionary<string, long> w1 = PlaceWires(wire1);
            //Dictionary<string, long> w2 = PlaceWires(wire2);

            int gridSize = 20000;
            long[,] w1 = PlaceWires(wire1, gridSize);
            long[,] w2 = PlaceWires(wire2, gridSize);

            //find intersections of wires add to list
            List<string> intersects = new List<string>();
            for (int y = 0; y < gridSize - 1; y++)
            {
                for (int x = 0; x < gridSize - 1; x++)
                {
                    if (y == (gridSize / 2) && x == (gridSize / 2))
                        continue;

                    if (w1[x, y] != -1 && w2[x, y] != -1)
                        intersects.Add(PosString(x, y));
                }
            }

            //foreach (var item1 in w1)
            //{
            //    foreach (var item2 in w2)
            //    {
            //        //don't include the starting point
            //        if (item1.Key == "0,0" && item2.Key == "0,0")
            //            continue;

            //        if (item1.Key == item2.Key)
            //            intersects.Add(item1.Key);
            //    }
            //}

            long lowest = long.MaxValue - 1;
            foreach (var item in intersects)
            {
                long x = long.Parse(item.Split(',')[0]) - (gridSize / 2);
                long y = long.Parse(item.Split(',')[1]) - (gridSize / 2);

                long temp = Math.Abs(x - 0) + Math.Abs(y - 0);
                if (temp < lowest)
                    lowest = temp;
            }

            return lowest.ToString();
        }

        protected override string Solution2(string[] input)
        {
            string[] wire1 = input[0].Split(',');
            string[] wire2 = input[1].Split(',');

            //Dictionary<string, long> w1 = PlaceWires(wire1);
            //Dictionary<string, long> w2 = PlaceWires(wire2);

            int gridSize = 20000;
            long[,] w1 = PlaceWires(wire1, gridSize);
            long[,] w2 = PlaceWires(wire2, gridSize);

            long lowest = long.MaxValue - 1;

            //find intersections of wires add to list
            List<long> intersects = new List<long>();
            for (int y = 0; y < gridSize - 1; y++)
            {
                for (int x = 0; x < gridSize - 1; x++)
                {
                    if (y == (gridSize / 2) && x == (gridSize / 2))
                        continue;

                    if (w1[x, y] != -1 && w2[x, y] != -1)
                        intersects.Add(w1[x, y] + w2[x, y]);
                }
            }

            intersects.Sort();

            return intersects[0].ToString();

            //foreach (var item1 in w1)
            //{
            //    foreach (var item2 in w2)
            //    {
            //        //don't include the starting point
            //        if (item1.Key == "0,0" && item2.Key == "0,0")
            //            continue;

            //        if (item1.Key == item2.Key)
            //        {
            //            //long x = long.Parse(item.Split(',')[0]);
            //            //long y = long.Parse(item.Split(',')[1]);

            //            //vis[offSetX + x, offSetY + y] = item;

            //            long temp = item1.Value + item2.Value;
            //            if (temp < lowest)
            //                lowest = temp;
            //        }
            //    }
            //}

            //return lowest.ToString();
        }

        static long[,] PlaceWires(string[] wire, int gridSize)
        {
            //starting position
            long x = gridSize / 2;
            long y = gridSize / 2;

            //Dictionary<string, long> wireView = new Dictionary<string, long>();

            long[,] wireView = new long[gridSize, gridSize];

            for (int x1 = 0; x1 < gridSize; x1++)
                for (int y1 = 0; y1 < gridSize; y1++)
                    wireView[x1, y1] = -1;

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

                    //if (!wireView.ContainsKey(PosString(x, y)))
                    //wireView.Add(PosString(x, y), length);
                    //else
                    //wireView.Add(PosString(x, y), length);

                    if (wireView[x, y] == -1)
                        wireView[x, y] = length;
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
