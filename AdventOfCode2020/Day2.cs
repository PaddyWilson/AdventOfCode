using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day2 : BaseDay
	{
		public Day2()
		{
			Day = "2";
			Answer1 = "636";
			Answer2 = "588";
		}

		protected override string Solution1(string[] input)
		{
			List<string> validPasswords = new List<string>();

			foreach (var item in input)
			{
				string pass = item.Split(' ')[2];
				char letter = item.Split(' ')[1][0];// (0, 1);
				int min = int.Parse(item.Split(' ')[0].Split('-')[0]);
				int max = int.Parse(item.Split(' ')[0].Split('-')[1]);

				int count = 0;
				foreach (var p in pass)
				{
					if (p == letter)
						count += 1;
				}

				if (count >= min && count <= max)
					validPasswords.Add(pass);
			}

			return validPasswords.Count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<string> validPasswords = new List<string>();

			foreach (var item in input)
			{
				string pass = item.Split(' ')[2];
				char letter = item.Split(' ')[1][0];// (0, 1);
				int min = int.Parse(item.Split(' ')[0].Split('-')[0]);
				int max = int.Parse(item.Split(' ')[0].Split('-')[1]);

				if (pass[min - 1] == letter && pass[max - 1] == letter)
					continue;
				else if (pass[min - 1] != letter && pass[max - 1] != letter)
					continue;
				else if (pass[min - 1] == letter && pass[max - 1] != letter)
					validPasswords.Add(pass);
				else if (pass[min - 1] != letter && pass[max - 1] == letter)
					validPasswords.Add(pass);
			}

			return validPasswords.Count.ToString();
		}
	}
}
