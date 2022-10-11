using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AOC;

namespace AdventOfCode2020
{

	public class Day21 : BaseDay
	{
		public Day21()
		{
			Day = "21";
			Answer1 = "0";
			Answer2 = "0";
		}

		protected override string Solution1(string[] input)
		{
			List<List<string>> ingredients = new List<List<string>>();
			List<List<string>> allergens = new List<List<string>>();

			Dictionary<string, List<string>> possibleAllergens = new Dictionary<string, List<string>>();

			foreach (var item in input)
			{
				string[] data = item.Split(" (contains ");

				string[] tempIngredients = data[0].Split(" ");
				string[] tempAllergen = data[1].Split(" ");

				ingredients.Add(new List<string>(tempIngredients));

				for (int i = 0; i < tempAllergen.Length; i++)
					tempAllergen[i] = tempAllergen[i].Replace(",", "").Replace(")", "");

				allergens.Add(new List<string>(tempAllergen));


			}

			//add all ingredients
			foreach (var foodList in ingredients)
			{
				foreach (var food in foodList)
				{
					if (!possibleAllergens.ContainsKey(food))
						possibleAllergens.Add(food, new List<string>());
				}
			}

			for (int i = 0; i < ingredients.Count; i++)
			{

			}

			return 0.ToString();
		}

		protected override string Solution2(string[] input)
		{
			return "-1";
		}
	}
}
