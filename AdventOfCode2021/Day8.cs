using AOC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2021
{
	public class Day8 : BaseDay
	{
		public Day8()
		{
			Day = "8";
			Answer1 = "369";
			Answer2 = "1031553";
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<int, int> output = new Dictionary<int, int>();
			output.Add(2, 0);
			output.Add(3, 0);
			output.Add(4, 0);
			output.Add(7, 0);

			for (int i = 0; i < input.Length; i++)
			{
				string[] items = input[i].Split('|')[1].Trim().Split(' ');

				foreach (var item in items)
				{
					if (output.ContainsKey(item.Length))
						output[item.Length]++;
				}
			}

			int count = 0;
			foreach (var item in output)
				count += item.Value;

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int count = 0;

			for (int i = 0; i < input.Length; i++)
			{
				//Console.Write("New");
				var combined = input[i].Replace("| ", "").Split(' ').ToList();
				var outputItems = input[i].Split('|')[1].Trim().Split(' ').ToList();

				string zero = "", one = "", two = "", three = "", four = "", five = "", six = "", seven = "", eight = "", nine = "";

				//one, four, seven, eight
				for (int j = combined.Count - 1; j >= 0; j--)
				{
					// one
					if (combined[j].Length == 2)
					{
						one = combined[j];
						combined.RemoveAt(j);
					}
					// four
					else if (combined[j].Length == 4)
					{
						four = combined[j];
						combined.RemoveAt(j);
					}
					// seven
					else if (combined[j].Length == 3)
					{
						seven = combined[j];
						combined.RemoveAt(j);
					}
					// eight
					else if (combined[j].Length == 7)
					{
						eight = combined[j];
						combined.RemoveAt(j);
					}
				}

				for (int j = combined.Count - 1; j >= 0; j--)
				{
					//zero, three, nine
					if (matching(combined[j], one) == 2)
					{
						// nine
						if (matching(combined[j], four) == 4 && matching(combined[j], seven) == 3)
						{
							nine = combined[j];
							combined.RemoveAt(j);
						}
						// zero
						else if (combined[j].Length == 6 && matching(combined[j], four) == 3 && matching(combined[j], seven) == 3)
						{
							zero = combined[j];
							combined.RemoveAt(j);
						}
						// three
						else if (combined[j].Length == 5 && matching(combined[j], four) == 3 && matching(combined[j], one) == 2)
						{
							three = combined[j];
							combined.RemoveAt(j);
						}
					}
				}

				for (int j = combined.Count - 1; j >= 0; j--)
				{
					// six
					if (combined[j].Length == 6 && matching(combined[j], four) == 3 && matching(combined[j], one) == 1)
					{
						six = combined[j];
						combined.RemoveAt(j);
					}
				}

				for (int j = combined.Count - 1; j >= 0; j--)
				{
					// two
					if (matching(combined[j], six) == 4)
					{
						two = combined[j];
						combined.RemoveAt(j);
					}
					// five
					else if (matching(combined[j], six) == 5) 
					{
						five = combined[j];
						combined.RemoveAt(j);
					}
				}

				string o = "";
				foreach (var item in outputItems)
				{
					if (item.Length == 6 && matching(item, zero) == 6)
						o += '0';
					else if (item.Length == 2 && matching(item, one) == 2)
						o += '1';
					else if (item.Length == 5 && matching(item, two) == 5)
						o += '2';
					else if (item.Length == 5 && matching(item, three) == 5)
						o += '3';
					else if (item.Length == 4 && matching(item, four) == 4)
						o += '4';
					else if (item.Length == 5 && matching(item, five) == 5)
						o += '5';
					else if (item.Length == 6 && matching(item, six) == 6)
						o += '6';
					else if (item.Length == 3 && matching(item, seven) == 3)
						o += '7';
					else if (item.Length == 7 && matching(item, eight) == 7)
						o += '8';
					else if (item.Length == 6 && matching(item, nine) == 6)
						o += '9';
				}

				count += int.Parse(o);
			}

			return count.ToString();
		}

		private int matching(string in1, string in2)
		{
			int count = 0;
			for (int i = 0; i < in1.Length; i++)
			{
				for (int j = 0; j < in2.Length; j++)
				{
					if (in1[i] == in2[j])
						count++;
				}
			}
			return count;
		}
	}
}
