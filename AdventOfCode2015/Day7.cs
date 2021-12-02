using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Day7 : BaseDay
	{
		public Day7()
		{
			Day = "7";
			Answer1 = "956";
			Answer2 = "40149";
		}

		protected override string Solution1(string[] input)
		{
			/*
			 * operators
			 * ->
			 * 
			 * AND
			 * NOT
			 * RSHIFT
			 * LSHIFT
			 * OR 
			 */

			List<string> operations = new List<string>(input);

			Dictionary<string, ushort> wireValues = new Dictionary<string, ushort>();

			int operationsIndex = 0;
			while (operations.Count != 0)
			{
				string[] codes = operations[operationsIndex].Split(' ');
				
				bool remove = false;

				if (codes.Length == 3)
				{

					ushort number = 0;
					bool parsed = ushort.TryParse(codes[0], out number);
					if (parsed)
					{
						wireValues[codes[2]] = number;
						remove = true;
					}
					else
					{
						if (wireValues.ContainsKey(codes[0]))
						{
							wireValues[codes[2]] = wireValues[codes[0]];
							remove = true;
						}
					}
				}
				else if (codes.Length == 4)
				{
					if (wireValues.ContainsKey(codes[1]))
						if (codes[0] == "NOT")
						{
							wireValues[codes[3]] = (ushort)~(wireValues[codes[1]]);
							remove = true;
						}
				}
				else if (codes.Length == 5)
				{
					ushort value1;
					ushort value2;

					bool val1 = ushort.TryParse(codes[0], out value1);
					bool val2 = ushort.TryParse(codes[2], out value2);

					if (!val1 && wireValues.ContainsKey(codes[0]))
						value1 = wireValues[codes[0]];
					if (!val2 && wireValues.ContainsKey(codes[2]))
						value2 = wireValues[codes[2]];

					if ((wireValues.ContainsKey(codes[0]) && wireValues.ContainsKey(codes[2]))
						|| (val1 && wireValues.ContainsKey(codes[2]))
						|| (val2 && wireValues.ContainsKey(codes[0])))
					{
						if (codes[1] == "AND")
						{
							wireValues[codes[4]] = (ushort)(value1 & value2);
							remove = true;
						}
						else if (codes[1] == "RSHIFT")
						{
							wireValues[codes[4]] = (ushort)(value1 >> value2);
							remove = true;
						}
						else if (codes[1] == "LSHIFT")
						{
							wireValues[codes[4]] = (ushort)(value1 << value2);
							remove = true;
						}
						else if (codes[1] == "OR")
						{
							wireValues[codes[4]] = (ushort)(value1 ^ value2);
							remove = true;
						}
					}
				}

				if (remove)
					operations.RemoveAt(operationsIndex);

				if (!remove)
					operationsIndex++;
				if (operationsIndex >= operations.Count)
					operationsIndex = 0;
			}

			return wireValues["a"].ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<string> operations = new List<string>(input);

			Dictionary<string, ushort> wireValues = new Dictionary<string, ushort>();

			int operationsIndex = 0;
			while (operations.Count != 0)
			{
				string[] codes = operations[operationsIndex].Split(' ');

				bool remove = false;

				if (codes.Length == 3)
				{

					ushort number = 0;
					bool parsed = ushort.TryParse(codes[0], out number);
					if (parsed)
					{
						wireValues[codes[2]] = number;
						remove = true;
					}
					else
					{
						if (wireValues.ContainsKey(codes[0]))
						{
							wireValues[codes[2]] = wireValues[codes[0]];
							remove = true;
						}
					}
				}
				else if (codes.Length == 4)
				{
					if (wireValues.ContainsKey(codes[1]))
						if (codes[0] == "NOT")
						{
							wireValues[codes[3]] = (ushort)~(wireValues[codes[1]]);
							remove = true;
						}
				}
				else if (codes.Length == 5)
				{
					ushort value1;
					ushort value2;

					bool val1 = ushort.TryParse(codes[0], out value1);
					bool val2 = ushort.TryParse(codes[2], out value2);

					if (!val1 && wireValues.ContainsKey(codes[0]))
						value1 = wireValues[codes[0]];
					if (!val2 && wireValues.ContainsKey(codes[2]))
						value2 = wireValues[codes[2]];

					//override "b" wire with output from "a" wire from Solution1()
					if (codes[0] == "b")
						value1 = 956;
					if (codes[2] == "b")
						value2 = 956;

					if ((wireValues.ContainsKey(codes[0]) && wireValues.ContainsKey(codes[2]))
						|| (val1 && wireValues.ContainsKey(codes[2]))
						|| (val2 && wireValues.ContainsKey(codes[0])))
					{
						if (codes[1] == "AND")
						{
							wireValues[codes[4]] = (ushort)(value1 & value2);
							remove = true;
						}
						else if (codes[1] == "RSHIFT")
						{
							wireValues[codes[4]] = (ushort)(value1 >> value2);
							remove = true;
						}
						else if (codes[1] == "LSHIFT")
						{
							wireValues[codes[4]] = (ushort)(value1 << value2);
							remove = true;
						}
						else if (codes[1] == "OR")
						{
							wireValues[codes[4]] = (ushort)(value1 ^ value2);
							remove = true;
						}
					}
				}

				if (remove)
					operations.RemoveAt(operationsIndex);

				if (!remove)
					operationsIndex++;
				if (operationsIndex >= operations.Count)
					operationsIndex = 0;
			}

			return wireValues["a"].ToString();
		}
	}
}
