using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day12 : BaseDay
	{
		public Day12()
		{
			Day = "12";
			Answer1 = "111754";
			Answer2 = "65402";
		}

		protected override string Solution1(string[] input)
		{
			return Solve(input[0]);
		}

		protected override string Solution2(string[] input)
		{
			//I manualy remove the uneeded parts of the file and saved it to a new file
			return Solve(File.ReadAllLines(InputDir + "Day12part2.txt")[0]); ;
		}

		private string Solve(string input)
		{
			var matches = Regex.Matches(input, "(-|)[0-9]{1,20}");

			int sum = 0;
			foreach (var item in matches)
			{
				sum += int.Parse(item.ToString());
			}

			return sum.ToString();
		}

	}
}
