using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AOC;

namespace AdventOfCode2020
{
	public class Day18 : BaseDay
	{
		public Day18()
		{
			Day = "18";
			Answer1 = "11004703763391";
			Answer2 = "290726428573651";
		}

		protected override string Solution1(string[] input)
		{
			List<List<string>> mathProblems = new List<List<string>>();

			foreach (var item in input)
			{
				var temp = new List<string>();
				foreach (var c in item)
				{
					if (c != ' ')
						temp.Add(c.ToString());
				}
				mathProblems.Add(temp);
			}

			long total = 0;
			foreach (var problem in mathProblems)
			{
				total += DoMath(problem.ToArray());
			}

			return total.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<List<string>> mathProblems = new List<List<string>>();

			foreach (var item in input)
			{
				var temp = new List<string>();
				foreach (var c in item)
				{
					if (c != ' ')
						temp.Add(c.ToString());
				}
				mathProblems.Add(temp);
			}

			long total = 0;
			foreach (var problem in mathProblems)
			{
				total += DoAdvancedMath(problem.ToArray());
			}

			return total.ToString();
		}

		private long DoMath(string[] input, List<int> ignoreIndex = null, int braceDepth = 0, int start = 0)
		{//braceDepth could be removed. used for debuging
			long output = 0;

			char method = ' ';

			if (ignoreIndex == null)
				ignoreIndex = new List<int>();

			for (int i = start; i < input.Length; i++)
			{
				if (ignoreIndex.Contains(i))
					continue;

				ignoreIndex.Add(i);

				//Console.WriteLine("DM OP:" + input[i] + " Index=" + i + " Out:" + output + " BD:" + braceDepth);

				switch (input[i][0])
				{
					case '(':
						long lastNumber = DoMath(input, ignoreIndex, braceDepth + 1, i + 1);

						if (method == '+' || method == ' ')
							output += lastNumber;
						else if (method == '*')
							output *= lastNumber;

						break;
					case ')':
						return output; //break;
					case '+':
						method = '+'; break;
					case '*':
						if (i > 0)
							method = '*'; break;
					default:

						if (i == start || method == ' ')
						{
							output += long.Parse(input[i]);
						}
						else if (method == '+')
						{
							output += long.Parse(input[i]);
							method = ' ';
						}
						else if (method == '*')
						{
							output *= long.Parse(input[i]);
							method = ' ';
						}
						break;
				}//end switch

			}//end for
			return output;
		}//end DoMath()

		private long DoAdvancedMath(string[] input, List<int> ignoreIndex = null, int braceDepth = 0, int start = 0)
		{//braceDepth could be removed. used for debuging
			long output = 0;

			char method = ' ';

			if (ignoreIndex == null)
				ignoreIndex = new List<int>();

			List<string> doLator = new List<string>();

			for (int i = start; i < input.Length; i++)
			{
				if (ignoreIndex.Contains(i))
					continue;

				ignoreIndex.Add(i);

				//Console.WriteLine("AD OP:" + input[i] + " Index=" + i + " Out:" + output + " BD:" + braceDepth + " lator:" + doLator.Count);

				switch (input[i][0])
				{
					case '(':
						if (method == '+' || method == ' ')
						{
							output += DoAdvancedMath(input, ignoreIndex, braceDepth + 1, i + 1);
						}
						else if (method == '*')
						{
							doLator.Add("*");
							doLator.Add(DoAdvancedMath(input, ignoreIndex, braceDepth + 1, i + 1).ToString());
							method = ' ';
							output = 0;
						}
						break;
					case ')':
						i = input.Length + 20;
						//return output; 
						break;
					case '+':
						method = '+';
						break;
					case '*':
						doLator.Add(output.ToString());
						doLator.Add("*");
						method = ' ';
						output = 0;
						break;
					default:

						if (i == start || method == ' ')
						{
							output += long.Parse(input[i]);
						}
						else if (method == '+')
						{
							output += long.Parse(input[i]);
							method = ' ';
						}
						break;
				}//end switch

			}//end for

			doLator.Add(output.ToString());

			//does the multiplication ofter addition

			return DoMath(doLator.ToArray());
		}//end DoMath()
	}
}
