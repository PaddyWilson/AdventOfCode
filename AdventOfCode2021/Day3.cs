using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
	public class Day3 : BaseDay
	{
		public Day3()
		{
			Day = "3";
			Answer1 = "2724524";
			Answer2 = "2775870";
		}

		protected override string Solution1(string[] input)
		{
			string gammaRate = "";
			string epsilonRate = "";

			for (int i = 0; i < input[0].Length; i++)
			{
				int zeroCount = 0;
				int oneCount = 0;

				for (int j = 0; j < input.Length; j++)
				{
					if (input[j][i] == '1')
						oneCount++;
					else
						zeroCount++;
				}

				if (oneCount > zeroCount)
				{
					gammaRate += "1";
					epsilonRate += "0";
				}
				else
				{
					gammaRate += "0";
					epsilonRate += "1";
				}
			}

			int output = Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);

			return output.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<string> oxygenList = new List<string>(input);
			List<string> co2List = new List<string>(input);

			int bitIndex = 0;
			while (oxygenList.Count > 1 || co2List.Count > 1)
			{
				int zeroCount = 0;
				int oneCount = 0;
				for (int j = 0; j < oxygenList.Count; j++)
				{
					if (oxygenList[j][bitIndex] == '1')
						oneCount++;
					else
						zeroCount++;
				}

				if (oneCount > zeroCount || oneCount == zeroCount)
					RemoveItems(oxygenList, bitIndex, '0');
				else
					RemoveItems(oxygenList, bitIndex, '1');

				zeroCount = 0;
				oneCount = 0;
				for (int j = 0; j < co2List.Count; j++)
				{
					if (co2List[j][bitIndex] == '1')
						oneCount++;
					else
						zeroCount++;
				}

				if (zeroCount < oneCount || oneCount == zeroCount)
					RemoveItems(co2List, bitIndex, '1');
				else
					RemoveItems(co2List, bitIndex, '0');

				bitIndex++;
			}

			int output = Convert.ToInt32(oxygenList[0], 2) * Convert.ToInt32(co2List[0], 2);

			return output.ToString();
		}

		private void RemoveItems(List<string> input, int binaryIndex, char number)
		{
			if (input.Count > 1)
				for (int j = input.Count - 1; j >= 0; j--)
				{
					if (input[j][binaryIndex] == number)
						input.RemoveAt(j);
				}
		}
	}
}
