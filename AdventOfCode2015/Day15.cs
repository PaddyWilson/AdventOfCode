using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Ingredent
	{
		public string Name { get; private set; }
		public int Capacity { get; private set; }
		public int Durability { get; private set; }
		public int Flavor { get; private set; }
		public int Texture { get; private set; }
		public int Calories { get; private set; }

		public int Amount { get; set; }

		public Ingredent(string name, int capacity, int durability, int flavor, int texture, int calories)
		{
			Name = name;
			Capacity = capacity;
			Durability = durability;
			Flavor = flavor;
			Texture = texture;
			Calories = calories;
		}

		public int CalcCapacity()
		{
			return Capacity * Amount;
		}
		public int CalcDurability()
		{
			return Durability * Amount;
		}
		public int CalcFlavor()
		{
			return Flavor * Amount;
		}
		public int CalcTexture()
		{
			return Texture * Amount;
		}
		public int CalcCalories()
		{
			return Calories * Amount;
		}

	}

	public class Day15 : BaseDay
	{
		public Day15()
		{
			Day = "15";
			Answer1 = "222870";
			Answer2 = "117936";
		}

		protected override string Solution1(string[] input)
		{
			List<Ingredent> ingredents = ParseInput(input);

			int maxTeaspoons = 100;
			CalcMaxCookieScore(ingredents, maxTeaspoons, new int[ingredents.Count]);

			return maxScore.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<Ingredent> ingredents = ParseInput(input);

			int maxTeaspoons = 100;
			int maxCaloies = 500;
			CalcMaxCookieCalories(ingredents, maxTeaspoons, new int[ingredents.Count], maxCaloies);

			return maxScore.ToString();
		}

		int maxScore = int.MinValue;
		Dictionary<int[], int> cookieScoreCache = new Dictionary<int[], int>(new MyEqualityComparer());
		private int CalcMaxCookieScore(List<Ingredent> ingredents, int maxTeaspoons, int[] amounts, int currentTeaspoons = 0)
		{
			if (cookieScoreCache.ContainsKey(amounts))
				return cookieScoreCache[amounts];

			//create new dict on new run
			if (currentTeaspoons == 0)
			{
				maxScore = 0;
				cookieScoreCache = new Dictionary<int[], int>(new MyEqualityComparer());
			}
			int capacity = 0;
			int durability = 0;
			int flavor = 0;
			int texture = 0;

			for (int i = 0; i < ingredents.Count; i++)
			{
				ingredents[i].Amount = amounts[i];
				capacity += ingredents[i].CalcCapacity();
				durability += ingredents[i].CalcDurability();
				flavor += ingredents[i].CalcFlavor();
				texture += ingredents[i].CalcTexture();
			}

			if (capacity < 0) capacity = 0;
			if (durability < 0) durability = 0;
			if (flavor < 0) flavor = 0;
			if (texture < 0) texture = 0;

			int score = capacity * durability * flavor * texture;

			if (score > maxScore)
				maxScore = score;

			int[] tem = new int[amounts.Length];
			Array.Copy(amounts, tem, amounts.Length);
			cookieScoreCache.Add(tem, score);

			if (currentTeaspoons == maxTeaspoons)
				return score;

			for (int i = 0; i < amounts.Length; i++)
			{
				amounts[i]++;
				CalcMaxCookieScore(ingredents, maxTeaspoons, amounts, currentTeaspoons + 1);
				amounts[i]--;
			}

			return int.MinValue;
		}

		//Dictionary<int[], int> cookieCaloriesCache = new Dictionary<int[], int>(new MyEqualityComparer());
		private int CalcMaxCookieCalories(List<Ingredent> ingredents, int maxTeaspoons, int[] amounts, int maxCalories, int currentTeaspoons = 0)
		{
			if (cookieScoreCache.ContainsKey(amounts))
				return cookieScoreCache[amounts];

			//create new dict on new run
			if (currentTeaspoons == 0)
			{
				maxScore = 0;
				cookieScoreCache = new Dictionary<int[], int>(new MyEqualityComparer());
			}

			int capacity = 0;
			int durability = 0;
			int flavor = 0;
			int texture = 0;
			int calories = 0;

			for (int i = 0; i < ingredents.Count; i++)
			{
				ingredents[i].Amount = amounts[i];
				capacity += ingredents[i].CalcCapacity();
				durability += ingredents[i].CalcDurability();
				flavor += ingredents[i].CalcFlavor();
				texture += ingredents[i].CalcTexture();
				calories += ingredents[i].CalcCalories();
			}

			if (capacity < 0) capacity = 0;
			if (durability < 0) durability = 0;
			if (flavor < 0) flavor = 0;
			if (texture < 0) texture = 0;

			int score = capacity * durability * flavor * texture;

			if (calories == maxCalories && score > maxScore)
				maxScore = score;

			int[] tem = new int[amounts.Length];
			Array.Copy(amounts, tem, amounts.Length);
			cookieScoreCache.Add(tem, score);

			if (currentTeaspoons == maxTeaspoons)
				return score;

			for (int i = 0; i < amounts.Length; i++)
			{
				amounts[i]++;
				CalcMaxCookieCalories(ingredents, maxTeaspoons, amounts, maxCalories,currentTeaspoons + 1);
				amounts[i]--;
			}

			return int.MinValue;
		}

		private List<Ingredent> ParseInput(string[] input)
		{
			List<Ingredent> ingredents = new List<Ingredent>();
			foreach (var item in input)
			{
				string[] splitIn = item.Replace(",", "").Split(' ');

				string name = splitIn[0].Replace(":", "");
				int capacity = int.Parse(splitIn[2]);
				int durability = int.Parse(splitIn[4]);
				int flavor = int.Parse(splitIn[6]);
				int texture = int.Parse(splitIn[8]);
				int calories = int.Parse(splitIn[10]);
				ingredents.Add(new Ingredent(name, capacity, durability, flavor, texture, calories));
			}
			return ingredents;
		}

	}


}
