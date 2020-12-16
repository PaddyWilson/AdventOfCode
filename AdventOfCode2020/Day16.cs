using System.Collections.Generic;

namespace AdventOfCode2020
{
	public class Day16 : BaseDay
	{
		public Day16()
		{
			Day = "16";
			Answer1 = "21978";
			Answer2 = "1053686852011";
		}

		protected override string Solution1(string[] input)
		{
			List<(int, int)> ranges = new List<(int, int)>();

			int[] myTicket;

			List<int[]> otherTickets = new List<int[]>();

			int asdfj = 0;
			foreach (var item in input)
			{
				if (item == "")
				{
					asdfj++;
				}
				//read ranges
				else if (asdfj == 0)
				{
					string[] temp1 = item.Split(':');
					string[] temp2 = temp1[1].Split(' ');

					string[] numbers = temp2[1].Split("-");
					ranges.Add((int.Parse(numbers[0]), int.Parse(numbers[1])));
					numbers = temp2[3].Split("-");
					ranges.Add((int.Parse(numbers[0]), int.Parse(numbers[1])));
				}
				else if (asdfj == 1)
				{
					if (item != "your ticket:")
					{
						string[] temp = item.Split(',');

						myTicket = new int[temp.Length];
						for (int i = 0; i < temp.Length; i++)
							myTicket[i] = int.Parse(temp[i]);
					}
				}
				else if (asdfj == 2)
				{
					if (item != "nearby tickets:")
					{
						string[] temp = item.Split(',');

						int[] ticket = new int[temp.Length];
						for (int i = 0; i < temp.Length; i++)
							ticket[i] = int.Parse(temp[i]);

						otherTickets.Add(ticket);
					}
				}
			}

			long output = 0;

			foreach (var ticket in otherTickets)
			{
				foreach (var number in ticket)
				{
					bool works = false;
					for (int i = 0; i < ranges.Count; i++)
					{
						if (number >= ranges[i].Item1 && number <= ranges[i].Item2)
						{
							works = true;
							break;
						}
					}

					if (!works)
					{
						output += number;
					}
				}
			}

			return output.ToString();
		}

		protected override string Solution2(string[] input)
		{
			Dictionary<string, (int, int, int, int)> ranges = new Dictionary<string, (int, int, int, int)>();

			int[] myTicket = new int[20];

			List<int[]> otherTickets = new List<int[]>();

			int wayToParse = 0;
			foreach (var item in input)
			{
				if (item == "")
				{
					wayToParse++;
				}
				//read ranges
				else if (wayToParse == 0)
				{
					string[] temp1 = item.Split(':');
					string[] temp2 = temp1[1].Split(' ');

					string[] numbers1 = temp2[1].Split("-");
					string[] numbers2 = temp2[3].Split("-");
					ranges.Add(temp1[0],
						(int.Parse(numbers1[0]),
						int.Parse(numbers1[1]),
						int.Parse(numbers2[0]),
						int.Parse(numbers2[1])));
				}
				else if (wayToParse == 1)
				{
					if (item != "your ticket:")
					{
						string[] temp = item.Split(',');

						myTicket = new int[temp.Length];
						for (int i = 0; i < temp.Length; i++)
							myTicket[i] = int.Parse(temp[i]);
					}
				}
				else if (wayToParse == 2)
				{
					if (item != "nearby tickets:")
					{
						string[] temp = item.Split(',');

						int[] ticket = new int[temp.Length];
						for (int i = 0; i < temp.Length; i++)
							ticket[i] = int.Parse(temp[i]);

						otherTickets.Add(ticket);
					}
				}
			}

			//remove bad tickets
			for (int i = otherTickets.Count - 1; i >= 0; i--)
			{
				foreach (var number in otherTickets[i])
				{
					bool works = false;
					foreach (var range in ranges)
					{
						if ((number >= range.Value.Item1 && number <= range.Value.Item2)
							|| (number >= range.Value.Item3 && number <= range.Value.Item4))
						{
							works = true;
							break;
						}
					}

					if (!works)
					{
						otherTickets.RemoveAt(i);
						break;
					}
				}
			}

			Dictionary<string, int> ticketData = new Dictionary<string, int>();

			//loop ticket numbers
			while (ranges.Count > 0)
			{
				for (int i = 0; i < otherTickets[0].Length; i++)
				{
					Dictionary<string, int> possibleData = new Dictionary<string, int>();
					//loop tickets
					for (int t = 0; t < otherTickets.Count; t++)
					{
						foreach (var range in ranges)
						{
							if ((otherTickets[t][i] >= range.Value.Item1 && otherTickets[t][i] <= range.Value.Item2)
								|| (otherTickets[t][i] >= range.Value.Item3 && otherTickets[t][i] <= range.Value.Item4))
							{
								if (!possibleData.ContainsKey(range.Key))
									possibleData[range.Key] = 0;
								possibleData[range.Key] += 1;
							}
						}
					}

					int count = 0;
					string key = "";
					foreach (var item in possibleData)
					{
						if (item.Value == otherTickets.Count)
						{
							key = item.Key;
							count++;
						}
					}

					if (count == 1)
					{
						ticketData[key] = i;
						ranges.Remove(key);
					}
				}//for (int i = 0; i < otherTickets[0].Length; i++)
			}//while (ranges.Count > 0)

			long output = 1;
			foreach (var item in ticketData)
				if (item.Key.Contains("departure"))
					output *= myTicket[item.Value];

			return output.ToString();
		}
	}
}
