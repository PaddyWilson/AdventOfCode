using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2015
{
	public class Day9 : BaseDay
	{
		public Day9()
		{
			Day = "9";
			Answer1 = "117";
			Answer2 = "909";
		}

		protected override string Solution1(string[] input)
		{
			Solve(input);

			return lowestDistance.ToString();
		}

		protected override string Solution2(string[] input)
		{
			Solve(input);

			return highestDistance.ToString();
		}

		int lowestDistance = int.MaxValue;
		int highestDistance = int.MinValue;

		private void Solve(string[] input)
		{
			//reset min max values
			lowestDistance = int.MaxValue;
			highestDistance = int.MinValue;

			//list of the locations index
			List<string> locations = new List<string>();
			Dictionary<(string, string), int> distance = new Dictionary<(string, string), int>();

			foreach (var item in input)
			{
				string[] tempSplit = item.Split(' ');

				if (!locations.Contains(tempSplit[0]))
					locations.Add(tempSplit[0]);

				if (!locations.Contains(tempSplit[2]))
					locations.Add(tempSplit[2]);

				distance.Add((tempSplit[0], tempSplit[2]), int.Parse(tempSplit[4]));
				distance.Add((tempSplit[2], tempSplit[0]), int.Parse(tempSplit[4]));
			}

			foreach (var item in locations)
			{
				List<string> temp1 = new List<string>(locations);
				temp1.Remove(item);
				List<string> temp2 = new List<string>();
				temp2.Add(item);
				ShortestRoute(temp2, temp1, distance, 0);
			}
		}

		private void ShortestRoute(List<string> visited, List<string> notVisited, Dictionary<(string, string), int> distance, int distanceTraveled)
		{
			if (notVisited.Count == 1)
			{
				distanceTraveled += distance[(visited[visited.Count - 1], notVisited[0])];
				if(lowestDistance > distanceTraveled)
					lowestDistance = distanceTraveled;
				if (highestDistance < distanceTraveled)
					highestDistance = distanceTraveled;
				distanceTraveled -= distance[(visited[visited.Count - 1], notVisited[0])];
				return;
			}

			foreach (var item in notVisited)
			{
				List<string> tempNot = new List<string>(notVisited);

				tempNot.Remove(item);

				distanceTraveled += distance[(visited[visited.Count - 1], item)];
				visited.Add(item);
				ShortestRoute(visited, tempNot, distance, distanceTraveled);
				visited.Remove(item);				
				distanceTraveled -= distance[(visited[visited.Count - 1], item)];
			}
		}
	}
}
