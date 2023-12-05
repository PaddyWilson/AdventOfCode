using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day5 : BaseDay
    {
        public Day5()
        {
            Day = "5";
            Answer1 = "525792406";
            Answer2 = "79004094";
        }

        protected override string Solution1(string[] input)
        {
            List<long> seeds = new List<long>();
            List<List<(long, long, long)>> maps = new();

            foreach (var item in input)
            {
                if (item.Contains("seeds:"))
                {
                    item.Replace("seeds: ", "").Split(" ").ToList().ForEach((string s) => { seeds.Add(long.Parse(s)); });
                }
                else if (item.Contains("map:"))
                {
                    maps.Add(new());
                }
                else if (item.Length != 0)
                {
                    var temp = item.Split(" ").ToList();
                    maps[^1].Add((long.Parse(temp[0]), long.Parse(temp[1]), long.Parse(temp[2])));
                }
            }

            long output = long.MaxValue;
            foreach (var item in seeds)
            {
                long temp = GetLocation(maps, 0, item);
                //Console.WriteLine("Location " + temp);
                if (output > temp)
                    output = temp;
            }

            return output.ToString();
        }

        protected override string Solution2(string[] input)
        {
            List<long> seeds = new List<long>();
            List<List<(long, long, long)>> maps = new();

            foreach (var item in input)
            {
                if (item.Contains("seeds:"))
                {
                    item.Replace("seeds: ", "").Split(" ").ToList().ForEach((string s) => { seeds.Add(long.Parse(s)); });
                }
                else if (item.Contains("map:"))
                {
                    maps.Add(new());
                }
                else if (item.Length != 0)
                {
                    var temp = item.Split(" ").ToList();
                    maps[^1].Add((long.Parse(temp[0]), long.Parse(temp[1]), long.Parse(temp[2])));
                }
            }

            long lowest = long.MaxValue;

            //multi threaded about 1:45 mins
            //List<long> tOut = new();
            //List<Thread> threads = new List<Thread>();
            //for (int i = 0; i < seeds.Count; i += 2)
            //{
            //    int a = i;
            //    long seed = seeds[i];
            //    long range = seeds[i + 1];
            //    threads.Add(new Thread(() =>
            //    {
            //        long output = long.MaxValue;
            //        for (long j = 0; j < range; j++)
            //        {
            //            long temp = GetLocation(maps, 0, seed + j);
            //            if (output > temp)
            //                output = temp;
            //        }
            //        lock (tOut)
            //            tOut.Add(output);
            //    }));
            //}

            //foreach (var item in threads)
            //    item.Start();

            //foreach (var item in threads)
            //    item.Join();

            //foreach (var item in tOut)
            //{
            //    if (lowest > item)
            //        lowest = item;
            //}
            //end multi thread

            //not multi threaded
            //about 7 mins on one 9900k core
            for (int i = 0; i < seeds.Count; i += 2)
            {
                for (long j = 0; j < seeds[i + 1]; j++)
                {
                    long temp = GetLocation(maps, 0, seeds[i] + j);
                    if (lowest > temp)
                        lowest = temp;
                }
            }

            return lowest.ToString();
        }

        void MultiThread(ref List<List<(long, long, long)>> maps, ref List<long> tOut, long seed, long range)
        {
            long output = long.MaxValue;
            //Console.WriteLine("Range " + (seeds[i + 1]));
            for (long j = 0; j < range; j++)
            {
                long temp = GetLocation(maps, 0, seed + j);
                //Console.WriteLine("Location " + temp);
                if (output > temp)
                    output = temp;
            }
            lock (tOut)
                tOut.Add(output);
        }


        long GetLocation(List<List<(long, long, long)>> maps, int mapIndex, long sourceNumber)
        {
            if (maps.Count == mapIndex)
                return sourceNumber;

            for (int i = 0; i < maps[mapIndex].Count; i++)
            {
                long dest = maps[mapIndex][i].Item1;
                long source = maps[mapIndex][i].Item2;
                long range = maps[mapIndex][i].Item3;

                if (sourceNumber >= source && sourceNumber < source + range)
                {
                    long newSource = dest + ((source - sourceNumber) * -1);
                    return GetLocation(maps, mapIndex + 1, newSource);
                }
            }
            return GetLocation(maps, mapIndex + 1, sourceNumber);
        }

    }
}
