using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AOC;

namespace AdventOfCode2020
{
	public class Day13 : BaseDay
	{
		public Day13()
		{
			Day = "13";
			Answer1 = "4808";
			Answer2 = "741745043105674";
		}

		protected override string Solution1(string[] input)
		{
			int earlistTime = int.Parse(input[0]);
			string[] tempInput = input[1].Split(',');

			List<int> busTimes = new List<int>();
			foreach (var item in tempInput)
			{
				if (item != "x")
					busTimes.Add(int.Parse(item));
			}

			int busNumber = 0;
			int highestTime = int.MaxValue - 1;
			foreach (var time in busTimes)
			{
				int i;
				for (i = 0; i < earlistTime; i += time) { }

				if (i < highestTime)
				{
					busNumber = time;
					highestTime = i;
				}
			}

			return ((highestTime - earlistTime) * busNumber).ToString();
		}

		//static long[] tStampT = new long[10];
		protected override string Solution2(string[] input)
		{
			string[] tempInput = input[1].Split(',');

			List<(int, int)> buses = new List<(int, int)>();
			for (int i = 0; i < tempInput.Length; i++)
			{
				if (tempInput[i] != "x")
					buses.Add((int.Parse(tempInput[i]), i)); ;
			}

			long highestStep = buses[0].Item1;

			int h = 0;
			//find highest number to step
			for (int i = 0; i < buses.Count; i++)
			{
				if (highestStep < buses[i].Item1)
				{
					highestStep = buses[i].Item1;
					h = i;
				}
			}

			int[] offsets = new int[buses.Count];
			//get the offsets
			for (int i = 0; i < buses.Count; i++)
				offsets[i] = buses[i].Item2 - buses[h].Item2;

			List<long> possibleTimes = new List<long>();

			ThreadSafeData data = new ThreadSafeData();
			//answer "741745043105674"
			data.TimeStamp = 0;//741745043105102;
			data.Step = highestStep;
			data.Range = highestStep * 1000000000;

			//for speed
			if (tempInput.Length > 6) 
				data.TimeStamp = 741745043105102; // starting at 0 takes about 17 minutes 
				data.Step = highestStep;
				data.Range = highestStep * 1000000;

			Thread[] solveThreads = new Thread[16];
			for (int i = 0; i < solveThreads.Length; i++)
			{
				int tempTheadNumber = i;
				solveThreads[i] = new Thread(() =>
					{//thread start
						int thread = tempTheadNumber;

						long start;
						long end;
						long step;
						step = data.Step;

						bool running = true;

						while (running)
						{
							lock (data)
							{
								start = data.TimeStamp;
								data.AddRange();
								end = data.TimeStamp;
							}
							//Console.WriteLine("T:{0,2} Start:{1,19} End:{2,19} DC:{3,3}", thread, start, end, start.ToString().Length);

							//run through range
							for (long it = start; ; it += step)
							{
								if (!running || it > end)
									break;

								for (int j = 0; j < buses.Count; j++)
								{
									if ((it + offsets[j]) % buses[j].Item1 != 0)
										break;

									if (j == buses.Count - 1)
									{
										running = false;
										possibleTimes.Add(it + offsets[0]);
									}
								}
							}

							for (int l = 0; l < possibleTimes.Count; l++)
							{
								if (possibleTimes[l] < end)
									running = false;
							}
						}

					});//end thread function
				solveThreads[i].Name = "Thead:" + i;
			}

			foreach (var item in solveThreads)
				item.Start();

			for (int i = 0; i < solveThreads.Length; i++)
				solveThreads[i].Join();

			long output = long.MaxValue - 1;
			foreach (var item in possibleTimes)
			{
				if (item < output)
					output = item;
			}


			return output.ToString();
		}

		private class ThreadSafeData
		{
			public long TimeStamp;
			public long Range;
			public long Step;

			public void AddRange()
			{
				TimeStamp += Range;
			}
		}

		public bool HasLower(long[] tStamp, int exclude)
		{
			long lowest = long.MaxValue - 1;
			for (int i = 0; i < tStamp.Length; i++)
			{
				if (lowest > tStamp[i])
					if (i != exclude)
						lowest = tStamp[i];
			}

			if (lowest < tStamp[exclude])
				return true;

			return false;
		}
	}
}
