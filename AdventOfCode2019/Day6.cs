using AOC;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019
{
	internal class Day6 : BaseDay
	{
		public Day6()
		{
			Day = "6";
			Answer1 = "140608";
			Answer2 = "337";
		}

		struct Ship
		{
			public string Name;
			public string Orbiting;
			public List<string> Orbiters;
			public bool Visited;
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<string, string> orbits = new Dictionary<string, string>();

			foreach (var item in input)
			{
				string[] split = item.Split(')');
				orbits.Add(split[1], split[0]);
			}

			long countOfOrbits = 0;
			foreach (var item in orbits)
			{
				long countTemp = 1;
				string currentShip = item.Value;
				while (currentShip != "COM")
				{
					currentShip = orbits[currentShip];
					countTemp++;
				}
				countOfOrbits += countTemp;
			}

			return countOfOrbits.ToString();
		}

		protected override string Solution2(string[] input)
		{
			Dictionary<string, Ship> orbits = new Dictionary<string, Ship>();

			{//add COM
				Ship ship = new Ship()
				{
					Name = "COM",
					Orbiting = "",
					Orbiters = new List<string>(),
					Visited = false
				};
				//ship.Orbiters.Add("YCD");
				orbits.Add("COM", ship);
			}

			foreach (var item in input)
			{
				string[] split = item.Split(')');

				Ship ship = new Ship()
				{
					Name = split[1],
					Orbiting = split[0],
					Orbiters = new List<string>(),
					Visited = false
				};
				ship.Orbiters.Add(split[0]);

				orbits.Add(split[1], ship);

				foreach (var item2 in input)
				{
					if (item == item2) continue;

					string[] split2 = item2.Split(')');

					if (split[1] == split2[0])
						if (!orbits[split[1]].Orbiters.Contains(split[1]))
							orbits[split[1]].Orbiters.Add(split2[1]);
				}//foreach
			}//foreach

			string currentShip = "YOU";

			Stack<Ship> path = new Stack<Ship>();

			FindPath(orbits, path, currentShip);

			return (path.Count - 2).ToString();
		}

		static long FindPath(Dictionary<string, Ship> orbits, Stack<Ship> path, string currentShip)
		{
			if (currentShip == "SAN") return -1;//found ship

			path.Push(orbits[currentShip]);
			Ship s = orbits[currentShip];
			s.Visited = true;
			orbits[currentShip] = s;

			foreach (var item in orbits[currentShip].Orbiters)
			{
				if (currentShip == item) continue;//don't loop back
				long result = 0;
				if (orbits[item].Visited != true)
					result = FindPath(orbits, path, item);

				if (result == -1)
					return -1;
			}

			path.Pop();
			return 0;
		}
	}
}