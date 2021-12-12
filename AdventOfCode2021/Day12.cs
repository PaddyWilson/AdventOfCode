using AOC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2021
{
	public class Day12 : BaseDay
	{
		public Day12()
		{
			Day = "12";
			Answer1 = "3802";
			Answer2 = "99448";
		}

		private class Cave
		{
			public string name = "";
			public List<string> connectedTo = new List<string>();
			public bool isLargeCave = false;
			public int timesVisited = 0;
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<string, Cave> caves = new Dictionary<string, Cave>();

			foreach (var item in input)
			{
				string[] temp = item.Split('-');

				if (!caves.ContainsKey(temp[0]))
				{
					caves.Add(temp[0], new Cave());
					caves[temp[0]].name = temp[0];
					if (temp[0][0] >= 65 && temp[0][0] <= 90)
						caves[temp[0]].isLargeCave = true;
				}

				if (!caves[temp[0]].connectedTo.Contains(temp[1]))
					caves[temp[0]].connectedTo.Add(temp[1]);

				if (!caves.ContainsKey(temp[1]))
				{
					caves.Add(temp[1], new Cave());
					caves[temp[1]].name = temp[1];
					if (temp[1][0] >= 65 && temp[1][0] <= 90)
						caves[temp[1]].isLargeCave = true;
				}

				if (!caves[temp[1]].connectedTo.Contains(temp[0]))
					caves[temp[1]].connectedTo.Add(temp[0]);
			}

			//this count be an int, its just a count of paths found
			//List<Stack<string>> foundPaths = new List<Stack<string>>();
			int foundPaths = 0;

			TracePaths(caves, ref foundPaths, "start");

			return foundPaths.ToString();
		}

		protected override string Solution2(string[] input)
		{
			Dictionary<string, Cave> caves = new Dictionary<string, Cave>();

			foreach (var item in input)
			{
				string[] temp = item.Split('-');

				if (!caves.ContainsKey(temp[0]))
				{
					caves.Add(temp[0], new Cave());
					caves[temp[0]].name = temp[0];
					if (temp[0][0] >= 65 && temp[0][0] <= 90)
						caves[temp[0]].isLargeCave = true;
				}

				if (!caves[temp[0]].connectedTo.Contains(temp[1]))
					caves[temp[0]].connectedTo.Add(temp[1]);

				if (!caves.ContainsKey(temp[1]))
				{
					caves.Add(temp[1], new Cave());
					caves[temp[1]].name = temp[1];
					if (temp[1][0] >= 65 && temp[1][0] <= 90)
						caves[temp[1]].isLargeCave = true;
				}

				if (!caves[temp[1]].connectedTo.Contains(temp[0]))
					caves[temp[1]].connectedTo.Add(temp[0]);
			}

			//this count be an int, its just a count of paths found
			//List<Stack<string>> foundPaths = new List<Stack<string>>();
			int foundPaths = 0;

			TracePathsPart2(caves, ref foundPaths, "start");

			return foundPaths.ToString();
		}

		private void TracePaths(Dictionary<string, Cave> caves, ref int foundPaths, string nextCave, Stack<string> currentPath = null)
		{
			if (currentPath == null)
			{
				Stack<string> path = new Stack<string>();
				path.Push("start");
				TracePaths(caves, ref foundPaths, "start", path);
				return;
			}

			if (nextCave == "end")
			{
				foundPaths++;
				//foundPaths.Add(new Stack<string>(currentPath));
				return;
			}

			foreach (var cave in caves[nextCave].connectedTo)
			{
				//ignore the start cave
				if (cave == "start")
					continue;

				//small cave has been visited twice
				if (!caves[cave].isLargeCave && currentPath.Contains(cave))
					continue;

				currentPath.Push(cave);
				TracePaths(caves, ref foundPaths, cave, currentPath);
				currentPath.Pop();
			}
		}

		private void TracePathsPart2(Dictionary<string, Cave> caves, ref int foundPaths, string nextCave, Stack<string> currentPath = null)
		{
			if (currentPath == null)
			{
				Stack<string> path = new Stack<string>();
				path.Push("start");
				caves["start"].timesVisited++;
				TracePathsPart2(caves, ref foundPaths, "start", path);
				return;
			}

			if (nextCave == "end")
			{
				foundPaths++;
				//foundPaths.Add(new Stack<string>(currentPath));
				return;
			}

			foreach (var cave in caves[nextCave].connectedTo)
			{
				//ignore the start cave
				if (cave == "start")
					continue;

				int countTwos = 0;
				foreach (var item in caves.Values)
					if (item.timesVisited == 2 && !item.isLargeCave)
						countTwos++;

				//small cave has been visited twice
				if (!caves[cave].isLargeCave && currentPath.Contains(cave) && countTwos == 1)
					continue;

				currentPath.Push(cave);
				caves[cave].timesVisited++;
				TracePathsPart2(caves, ref foundPaths, cave, currentPath);
				caves[cave].timesVisited--;
				currentPath.Pop();
			}
		}
	}
}
