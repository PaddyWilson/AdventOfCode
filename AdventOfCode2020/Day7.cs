using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day7 : BaseDay
	{
		private class Bag
		{
			public string BagColor = "";
			public Dictionary<string, Bag> SubBagColor = new Dictionary<string, Bag>();
			public Dictionary<string, int> SubBagCount = new Dictionary<string, int>();

			public Bag(string bagColor)
			{
				BagColor = BagColor;
			}

			public void AddBag(string subBagColor, int count)
			{
				SubBagColor.Add(subBagColor, new Bag(subBagColor));
				SubBagCount.Add(subBagColor, count);
			}
		}

		public Day7()
		{
			Day = "7";
			Answer1 = "272";
			Answer2 = "172246";
		}

		private Dictionary<string, Bag> ParseInput(string[] input)
		{
			Dictionary<string, Bag> bags = new Dictionary<string, Bag>();
			foreach (var item in input)
			{
				string currentBag = item.Split("contain")[0].Replace("bags", "").Trim();
				bags.Add(currentBag, new Bag(currentBag));

				string otherBags = item.Split("contain")[1].Trim();

				//no sub bags
				if (otherBags == " no other bags.".Trim())
					continue;

				string[] subBags = otherBags.Split(',');

				foreach (var subBag in subBags)
				{
					string subBagAmount = subBag.Trim().Split(" ")[0];
					string subBagColors = subBag.Trim().Split(" ")[1] + " " + subBag.Trim().Split(" ")[2];

					bags[currentBag].AddBag(subBagColors, int.Parse(subBagAmount));
				}
			}
			return bags;
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<string, Bag> bags = ParseInput(input);

			string bagBeingLookedFor = "shiny gold";
			int outputCount = 0;
			foreach (var bag in bags)
			{
				if (SubBagSearch(bags, bagBeingLookedFor, bag.Key))
					outputCount++;
			}

			return outputCount.ToString();
		}

		private bool SubBagSearch(Dictionary<string, Bag> bags, string bagBeingLookedFor, string currentBag)
		{
			foreach (var item in bags[currentBag].SubBagColor.Keys)
			{
				if (item == bagBeingLookedFor)
					return true;

				bool yes = SubBagSearch(bags, bagBeingLookedFor, item);
				if (yes)
					return true;
			}

			return false;
		}

		protected override string Solution2(string[] input)
		{
			Dictionary<string, Bag> bags = ParseInput(input);

			string bagBeingLookedFor = "shiny gold";
			int outputCount = CountSubBags(bags, bagBeingLookedFor);

			return outputCount.ToString();
		}

		private int CountSubBags(Dictionary<string, Bag> bags, string currentBag)
		{
			int count = 0;
			foreach (var item in bags[currentBag].SubBagColor)
				count += bags[currentBag].SubBagCount[item.Key];

			foreach (var item in bags[currentBag].SubBagColor)
				count += CountSubBags(bags, item.Key) * bags[currentBag].SubBagCount[item.Key];
			return count;
		}
	}
}
