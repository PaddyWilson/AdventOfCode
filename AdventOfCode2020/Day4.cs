using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day4 : BaseDay
	{
		public Day4()
		{
			Day = "4";
			Answer1 = "170";
			Answer2 = "103";
		}

		protected override string Solution1(string[] input)
		{
			List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();

			int currentPass = 0;
			foreach (var item in input)
			{
				if (passports.Count == currentPass)
					passports.Add(new Dictionary<string, string>());

				if (item == "")
					currentPass += 1;
				else
				{
					string[] items = item.Split(" ");
					foreach (var item2 in items)
					{
						string key = item2.Split(":")[0];
						string value = item2.Split(":")[0];
						passports[currentPass].Add(key, value);
					}
				}
			}

			int outputCount = 0;

			foreach (var item in passports)
			{
				if (item.ContainsKey("byr") & item.ContainsKey("iyr") & item.ContainsKey("eyr") & item.ContainsKey("hgt")
					&& item.ContainsKey("hcl") & item.ContainsKey("ecl") & item.ContainsKey("pid"))
				{
					outputCount++;
				}
			}
			return outputCount.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();

			int currentPass = 0;
			foreach (var item in input)
			{
				if (passports.Count == currentPass)
					passports.Add(new Dictionary<string, string>());

				if (item == "")
					currentPass += 1;
				else
				{
					string[] items = item.Split(" ");
					foreach (var item2 in items)
					{
						string key = item2.Split(":")[0];
						string value = item2.Split(":")[1];
						passports[currentPass].Add(key, value);
					}
				}
			}

			int i = 0;
			int outputCount = 0;
			foreach (var item in passports)
			{
				if (item.ContainsKey("byr") && item.ContainsKey("iyr") && item.ContainsKey("eyr") && item.ContainsKey("hgt")
					& item.ContainsKey("hcl") && item.ContainsKey("ecl") && item.ContainsKey("pid"))
				{
					bool byr = (int.Parse(item["byr"]) >= 1920 && int.Parse(item["byr"]) <= 2002);
					bool iry = (int.Parse(item["iyr"]) >= 2010 && int.Parse(item["iyr"]) <= 2020);
					bool eyr = (int.Parse(item["eyr"]) >= 2020 && int.Parse(item["eyr"]) <= 2030);

					bool hgt = false;
					string hgt_ext = item["hgt"].Substring(item["hgt"].Length - 2, 2);
					if (hgt_ext == "cm")
					{
						int hgt_size = int.Parse(item["hgt"].Substring(0, item["hgt"].Length - 2));
						if (hgt_size >= 150 && hgt_size <= 193)
						{
							hgt = true;
						}
					}
					else if (hgt_ext == "in")
					{
						int hgt_size = int.Parse(item["hgt"].Substring(0, item["hgt"].Length - 2));
						if (hgt_size >= 59 && hgt_size <= 76)
						{
							hgt = true;
						}
					}

					string pattern = @"^#[0-9a-fA-F]{6}$";
					Regex rg = new Regex(pattern);
					bool hcl = rg.IsMatch(item["hcl"]);

					bool ecl = (item["ecl"] == "amb" 
						|| item["ecl"] == "blu"
						|| item["ecl"] == "brn"
						|| item["ecl"] == "gry"
						|| item["ecl"] == "grn" 
						|| item["ecl"] == "hzl" 
						|| item["ecl"] == "oth");

					pattern = @"^[0-9]{9}$";
					Regex rg2 = new Regex(pattern);
					bool pid = rg2.IsMatch(item["pid"]);

					//bool pid = (item["pid"].Length == 9);

					if (byr && iry && eyr && hgt && hcl && ecl && pid)
						outputCount++;
				}
				i++;
			}

			return outputCount.ToString();
		}
	}
}
