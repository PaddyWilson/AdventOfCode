using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Deer
	{
		public string Name { get; private set; }
		public int KMs { get; private set; }
		public int MoveSec { get; private set; }
		public int RestSec { get; private set; }
		public int Points { get; private set; }
		public int Distance { get; private set; }

		int moveCur = 0;
		int restCur = 0;

		bool moveing = true;

		public Deer(string name, int kms, int moveSec, int restSec)
		{
			this.Name = name;
			this.KMs = kms;
			this.MoveSec = moveSec;
			this.RestSec = restSec;
		}

		public void Step()
		{
			if (moveing)
			{
				Distance += KMs;
				moveCur++;
				if (moveCur == MoveSec)
				{
					moveing = false;
					moveCur = 0;
				}
			}
			else
			{
				restCur++;
				if (restCur == RestSec)
				{
					moveing = true;
					restCur = 0;
				}
			}
		}

		public void AddPoint()
		{
			Points++;
		}
	}

	public class Day14 : BaseDay
	{
		public Day14()
		{
			Day = "14";
			Answer1 = "2640";
			Answer2 = "-1";
		}

		protected override string Solution1(string[] input)
		{
			List<Deer> deer = ParseInput(input);

			int maxSteps = 2503;
			//for test data
			if (input.Length < 4)
				maxSteps = 1000;


			for (int i = 0; i < maxSteps; i++)
			{
				foreach (var item in deer)
				{
					item.Step();
				}
			}			

			return CalcFarthest(deer).Distance.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<Deer> deer = ParseInput(input);

			int maxSteps = 2503;
			//for test data
			if (input.Length < 4)
				maxSteps = 1000;

			for (int i = 0; i < maxSteps; i++)
			{
				foreach (var item in deer)
				{
					item.Step();
				}

				int far = CalcFarthest(deer).Distance;

				foreach (var item in deer)
				{
					if (item.Distance == far)
						item.AddPoint();
				}
			}

			int maxPoints = 0;
			foreach (var item in deer)
			{
				if (item.Points > maxPoints)
				{
					maxPoints = item.Points;
				}
			}
			return maxPoints.ToString();
		}

		private List<Deer> ParseInput(string[] input)
		{
			List<Deer> deer = new List<Deer>();
			foreach (var item in input)
			{
				string[] splitIn = item.Split(' ');

				string name = splitIn[0];
				int kms = int.Parse(splitIn[3]);
				int moveSec = int.Parse(splitIn[6]);
				int restSec = int.Parse(splitIn[13]);

				deer.Add(new Deer(name, kms, moveSec, restSec));
			}
			return deer;
		}

		private Deer CalcFarthest(List<Deer> deer)
		{
			int maxDis = 0;
			Deer farthest = new Deer("Null", 0, 0, 0);

			foreach (var item in deer)
			{
				if (item.Distance > maxDis)
				{
					maxDis = item.Distance;
					farthest = item;
				}
			}

			return farthest;
		}
	}
}
