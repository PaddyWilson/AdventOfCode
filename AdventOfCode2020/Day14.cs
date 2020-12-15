using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day14 : BaseDay
	{
		public Day14()
		{
			Day = "14";
			Answer1 = "14839536808842";
			Answer2 = "4215284199669";
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<string, string> memory = new Dictionary<string, string>();
			string currentMask = "";

			foreach (var item in input)
			{
				string[] splits = item.Split(" ");

				if (splits[0] == "mask")
				{
					currentMask = splits[2];
					continue;
				}

				string number = Convert.ToString(long.Parse(splits[2]), 2);

				while (number.Length < currentMask.Length)
					number = "0" + number;

				var g = number.ToCharArray();
				for (int i = currentMask.Length - 1; i >= 0; i--)
					if (currentMask[i] != 'X')
						g[i] = currentMask[i];
				memory[splits[0]] = new string(g);
			}

			ulong output = 0;
			foreach (var item in memory)
				output += Convert.ToUInt64(item.Value, 2);
			return output.ToString();
		}

		protected override string Solution2(string[] input)
		{
			Dictionary<string, long> memory = new Dictionary<string, long>();
			string currentMask = "";

			foreach (var item in input)
			{
				string[] splits = item.Split(" ");

				if (splits[0] == "mask")
				{
					currentMask = splits[2];
					continue;
				}

				splits[0] = splits[0].Replace("mem[", "").Replace("]", "");//.Replace("[", "").Replace("]", "");

				string sAddress = Convert.ToString(long.Parse(splits[0]), 2);

				//add extra 0
				while (sAddress.Length < currentMask.Length)
					sAddress = "0" + sAddress;

				var g = sAddress.ToCharArray();
				for (int i = currentMask.Length - 1; i >= 0; i--)
				{
					if (currentMask[i] == '1')
						g[i] = '1';
					else if (currentMask[i] == 'X')
						g[i] = 'X';
				}

				List<string> address = new List<string>();
				GetAddresses(new string(g), ref address);

				foreach (var add in address)
					memory[add] = long.Parse(splits[2]);
			}

			ulong output = 0;
			foreach (var item in memory)
				output += (ulong)item.Value;
			return output.ToString();
		}


		private void GetAddresses(string sAddress, ref List<string> address, int at = -2)
		{
			if (at == -2)
			{
				GetAddresses(sAddress, ref address, sAddress.Length - 1);
				return;
			}
			else if (at == -1)
			{
				address.Add(sAddress);
				return;
			}
			else if (sAddress[at] == 'X')
			{
				var ar = sAddress.ToCharArray();
				ar[at] = '0';
				GetAddresses(new string(ar), ref address, at - 1);
				ar[at] = '1';
				GetAddresses(new string(ar), ref address, at - 1);
			}
			else
			{
				GetAddresses(sAddress, ref address, at - 1);
			}
			return;
		}
	}
}
