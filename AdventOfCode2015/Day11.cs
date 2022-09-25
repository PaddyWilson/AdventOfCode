using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day11 : BaseDay
	{
		public Day11()
		{
			Day = "11";
			Answer1 = "hepxxyzz";
			Answer2 = "heqaabcc";
		}

		protected override string Solution1(string[] input)
		{
			return Solve(input[0]);
		}

		protected override string Solution2(string[] input)
		{
			return Solve(IncrementPassword(Solve(input[0])));
		}

		private string Solve(string input)
		{
			string password = input;

			List<string> abcVal = new List<string>();
			{
				char abcT = 'a';
				for (int i = 0; i < 24; i++)
				{
					abcVal.Add(abcT.ToString() + ((char)(abcT + 1)).ToString() + ((char)(abcT + 2)).ToString());
					abcT = (char)(abcT + 1);
				}
			}

			List<string> aaVal = new List<string>();
			{
				char abcT = 'a';
				for (int i = 0; i < 26; i++)
				{
					aaVal.Add(abcT.ToString() + abcT.ToString());
					abcT = (char)(abcT + 1);
				}
			}

			List<string> invalid = new List<string>();
			invalid.Add("i");
			invalid.Add("o");
			invalid.Add("l");

			bool valid = false;
			while (!valid)
			{
				bool invalidB = false;
				bool abcB = false;

				foreach (var item in invalid)
				{
					if (password.Contains(item))
					{
						invalidB = true;
						break;
					}
				}

				foreach (var item in abcVal)
				{
					if (password.Contains(item))
					{
						abcB = true;
						break;
					}
				}

				int aaCount = 0;
				foreach (var item in aaVal)
				{
					if (password.Contains(item))
					{
						aaCount++;
					}
				}

				if (aaCount >= 2 && !invalidB && abcB)
				{
					return password;
				}

				password = IncrementPassword(password);
			}

			return password;
		}

		private string IncrementPassword(string password)
		{
			//increment password
			char[] temp = password.ToCharArray();

			//rollover
			if (temp[temp.Length - 1] == 'z')
			{
				if (temp[temp.Length - 2] == 'z' && temp[temp.Length - 1] == 'z')
				{
					if (temp[temp.Length - 3] == 'z' && temp[temp.Length - 2] == 'z' && temp[temp.Length - 1] == 'z')
					{
						if (temp[temp.Length - 4] == 'z' && temp[temp.Length - 3] == 'z' && temp[temp.Length - 2] == 'z' && temp[temp.Length - 1] == 'z')
						{
							if (temp[temp.Length - 5] == 'z')
							{
								if (temp[temp.Length - 6] == 'z')
								{
									if (temp[temp.Length - 7] == 'z')
									{
										temp[temp.Length - 8] = (char)(temp[temp.Length - 8] + 1);
										temp[temp.Length - 7] = (char)('a' - 1);
									}
									temp[temp.Length - 7] = (char)(temp[temp.Length - 7] + 1);
									temp[temp.Length - 6] = (char)('a' - 5);
								}
								temp[temp.Length - 6] = (char)(temp[temp.Length - 6] + 1);
								temp[temp.Length - 5] = (char)('a' - 4);
							}
							temp[temp.Length - 5] = (char)(temp[temp.Length - 5] + 1);
							temp[temp.Length - 4] = (char)('a' - 3);
						}
						temp[temp.Length - 4] = (char)(temp[temp.Length - 4] + 1);
						temp[temp.Length - 3] = (char)('a' - 2);
					}
					temp[temp.Length - 3] = (char)(temp[temp.Length - 3] + 1);
					temp[temp.Length - 2] = (char)('a' - 1);
				}
				temp[temp.Length - 2] = (char)(temp[temp.Length - 2] + 1);
				temp[temp.Length - 1] = 'a';
			}
			else
			{
				temp[temp.Length - 1] = (char)(temp[temp.Length - 1] + 1);
			}

			password = "";
			foreach (var item in temp)
			{
				password += item.ToString();
			}

			return password;
		}
	}
}
