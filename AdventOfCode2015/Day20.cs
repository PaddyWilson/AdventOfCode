using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day20 : BaseDay
	{
		public Day20()
		{
			Day = "20";
			Answer1 = "831600";
			Answer2 = "884520";
		}

		protected override string Solution1(string[] input)
		{
			int highest = int.Parse(input[0]);
			int houseNumber = 0;
			List<int> ints = new List<int>();
			while (true)
			{
				int pres = 0;

				ints.Add(houseNumber);

				for (int i = 0; i < ints.Count; i++)
				{
					if (ints[i] < houseNumber)
						ints[i] += i;

					if (ints[i] == houseNumber)
						pres += i * 10;
				}
				//if (houseNumber % 1000 == 0)
				//	Console.WriteLine(houseNumber + ":" + pres);// + "=" + string.Join(',', ints));

				if (pres >= highest)
					return houseNumber.ToString();
				houseNumber++;

			}

			//return 0.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int highest = int.Parse(input[0]);
			int houseNumber = 0;
			Dictionary<int, (int,int)> ints = new Dictionary<int, (int,int)>();
			//a  very slow solution but does it in about 1 hour 10 mins
			//maybe try removing ints that are not needed anymore to speed it up 
			while (true)
			{
				int pres = 0;

				ints.Add(houseNumber,(houseNumber,1));

				for (int i = 0; i < ints.Count; i++)
				{
					if (ints[i].Item1 < houseNumber)
					{ 
						ints[i] = (ints[i].Item1 + i, ints[i].Item2+1); 
					}

					if (ints[i].Item1 == houseNumber && ints[i].Item2 <= 50)
						pres += i * 11;
				}
				//if (houseNumber % 1000 == 0)
				//	Console.WriteLine(houseNumber + ":" + pres);// + "=" + string.Join(',', ints));

				if (pres >= highest)
					return houseNumber.ToString();
				houseNumber++;

			}

			//return 0.ToString();
		}
	}
}
