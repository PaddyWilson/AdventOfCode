using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace AOC
{
	public class Monkey
	{
		public Queue<long> items = new Queue<long>();

		public Func<long, long> Operation;

		public long Test;
		public long TestTrue;
		public long TestFalse;

		public long InspectedCount;

		public Monkey(IEnumerable<long> items, Func<long, long> Operation, long test, long testTrue, long testFalse)
		{
			this.items = new Queue<long>(items);
			this.Operation = Operation;
			this.Test = test;
			this.TestTrue = testTrue;
			this.TestFalse = testFalse;
			InspectedCount = 0;
		}

		public void ThrowItems(Dictionary<long, Monkey> monkeys, long div, bool part2 = false)
		{
			while (items.Count != 0)
			{
				InspectedCount++;
				long worryLevel = items.Dequeue();

				worryLevel = Operation(worryLevel);

				if (!part2)
					worryLevel = worryLevel / div;
				else//its a magic number
					worryLevel = worryLevel % div;

				if (worryLevel % Test == 0)
					monkeys[TestTrue].items.Enqueue(worryLevel);
				else
					monkeys[TestFalse].items.Enqueue(worryLevel);
			}
		}
	}

	public class Day11 : BaseDay
	{
		public Day11()
		{
			Day = "11";
			Answer1 = "50830";
			Answer2 = "14399640002";
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<long, Monkey> monkeys = Parse(input);

			for (long i = 0; i < 20; i++)
				foreach (var m in monkeys)
					m.Value.ThrowItems(monkeys, 3);

			List<long> items = new List<long>();
			foreach (var item in monkeys.Values)
				items.Add(item.InspectedCount);

			items.Sort();
			items.Reverse();

			return (items[0] * items[1]).ToString();
		}

		protected override string Solution2(string[] input)
		{
			Dictionary<long, Monkey> monkeys = Parse(input);

			//find the Least Common Multiple of the divisible number of the monkey
			//all the divisible numbers are also prime numbers
			//so you can just multiply them all together
			//this number will be used with Modular arithmetic so the numbers don't become super large
			//HELP:https://www.calculatorsoup.com/calculators/math/lcm.php
			long LCM = 1;
			foreach (var item in monkeys.Values)
				LCM *= item.Test;

			for (long i = 1; i <= 10000; i++)
				foreach (var m in monkeys)
					m.Value.ThrowItems(monkeys, LCM, true);

			List<long> items = new List<long>();
			foreach (var item in monkeys.Values)
				items.Add(item.InspectedCount);

			items.Sort();
			items.Reverse();

			return (items[0] * items[1]).ToString();
		}

		private Dictionary<long, Monkey> Parse(string[] input)
		{
			Dictionary<long, Monkey> monkeys = new Dictionary<long, Monkey>();

			for (long i = 0; i < input.Length; i += 2)
			{
				//line one of monkey
				var temp = input[i].Trim().Replace(":", "").Split(' ');
				long currentMonkey = long.Parse(temp[1]);

				i++;
				temp = input[i].Trim().Split(':')[1].Trim().Replace(",", "").Split(' ');
				List<long> items = new List<long>();
				foreach (var item in temp)
					items.Add(long.Parse(item));

				i++;
				temp = input[i].Trim().Split(':')[1].Trim().Replace(",", "").Split(' ');
				Func<long, long> Operation = (long item) => { return 0; };
				if (temp[2] == "old")//a;; are old
				{
					if (temp[3] == "+")
					{
						long value = long.Parse(temp[4]);
						Operation = (item) => { return item + value; };
					}
					else if (temp[3] == "*")
					{
						if (temp[4] == "old")
						{
							Operation = (item) => { return item * item; };
						}
						else
						{
							long value = long.Parse(temp[4]);
							Operation = (item) => { return item * value; };
						}
					}
				}

				i++;
				temp = input[i].Trim().Split(' ');
				long test = long.Parse(temp[3]);

				i++;
				temp = input[i].Trim().Split(' ');
				long testTrue = long.Parse(temp[5]);

				i++;
				temp = input[i].Trim().Split(' ');
				long testFalse = long.Parse(temp[5]);

				monkeys.Add(currentMonkey, new Monkey(items, Operation, test, testTrue, testFalse));
			}
			return monkeys;
		}
	}
}
