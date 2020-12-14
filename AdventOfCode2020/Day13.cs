using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day13 : BaseDay
	{
		public Day13()
		{
			Day = "13";
			Answer1 = "4808";
			Answer2 = "0";
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

		protected override string Solution2(string[] input)
		{
			string[] tempInput = input[1].Split(',');

			List<(int, int)> buses = new List<(int, int)>();
			for (int i = 0; i < tempInput.Length; i++)
			{
				if (tempInput[i] != "x")
					buses.Add((int.Parse(tempInput[i]), i)); ;
			}

			ulong timeStamp = 0;
			ulong tStep = (ulong)buses[0].Item1;
			//ulong lastMatching = 0;

			//ulong spaceing = 0;
			//ulong spaceingIndex = 1;
			int[] previousDiff = new int[buses.Count];
			previousDiff[0] = buses[0].Item2;

			ulong[] times = new ulong[buses.Count];
			ulong[] oldTimes = new ulong[buses.Count];
			for (int i = 0; i < times.Length; i++)
				times[i] = 0;

			ulong tempTime = 0;
			int matchTo = 0;

			bool running = true;

			Thread t = new Thread(() =>
			{
				while (running)
				{
					ulong dif = timeStamp;
					Thread.Sleep(1000 * 10);
					if (running)
						Console.WriteLine("TimeStamp:" + timeStamp + " MatchTo:" + matchTo + " Diff:" + (timeStamp - dif));
				}
			});//end thread
			t.Start();

			while (running)
			{
				timeStamp += tStep;

				for (int i = 0; i < buses.Count; i++)
				{
					ulong c = 0;
					ulong count = 0;
					for (c = times[i]; c < timeStamp; c += (ulong)buses[i].Item1) { count++; }

					ulong diff = c - timeStamp;
					times[i] = c;//store time stoped at
								 //the diff does not match the required time offset
					if (diff != (ulong)buses[i].Item2)
						break;

					previousDiff[i] = buses[i].Item2;

					//lastMatching = timeStamp;
				}

				int match = 0;

				for (int i = 0; i < buses.Count; i++)
				{
					if (buses[i].Item2 == previousDiff[i])
					{
						if (i == buses.Count - 1)
							running = false;
						match++;
					}
					else
					{
						break;
					}
				}

				if (match > matchTo)
					//{
					matchTo = match;
				//	tempTime = times[0] - oldTimes[0];
				//	Array.Copy(times, oldTimes, 1);//times.Length);
				//	if (tempTime > 0 && match > 1 && (tempTime % (ulong)buses[0].Item1) == 0)
				//		tStep = tempTime;
				//}

			}

			return (timeStamp).ToString();
		}
	}
}
