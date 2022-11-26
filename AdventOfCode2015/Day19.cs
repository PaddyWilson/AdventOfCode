using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day19 : BaseDay
	{
		public Day19()
		{
			Day = "19";
			Answer1 = "535";
			Answer2 = "212";
		}

		protected override string Solution1(string[] input)
		{
			List<(string, string)> subs = new List<(string, string)>();
			string molecule = input[^1];

			for (int i = 0; i < input.Length - 2; i++)
			{
				string[] temp = input[i].Split(" ");
				subs.Add((temp[0], temp[2]));
			}

			HashSet<string> hashes = new HashSet<string>();

			char[] mol = molecule.ToCharArray();
			foreach (var item in subs)
			{
				char[] match = item.Item1.ToCharArray();
				for (int i = 0; i < molecule.Length; i++)
				{
					if (mol[i] != match[0])
						continue;

					if (match.Length == 2 && mol[i + 1] != match[1])
						continue;

					char[] tempChars = new char[mol.Length - match.Length + item.Item2.Length];

					//Console.WriteLine(tempChars.Length);
					Array.Copy(mol, 0, tempChars, 0, i);
					//Console.WriteLine(item.Item1 + ">" + item.Item2 + ": " + string.Join(",", tempChars));

					Array.Copy(item.Item2.ToCharArray(), 0, tempChars, i, item.Item2.Length);
					//Console.WriteLine(item.Item1 + ">" + item.Item2 + ": " + string.Join(",", tempChars));

					Array.Copy(mol, i + item.Item1.Length, tempChars, i + item.Item2.Length, (mol.Length - i) - item.Item1.Length);
					//Console.WriteLine(item.Item1 + ">" + item.Item2 + ": " + string.Join(",", tempChars));

					string t = string.Join(" ", tempChars).Replace(" ", "");
					//Console.WriteLine("final: " + t);

					if (!hashes.Contains(t))
						hashes.Add(t);

				}
			}

			return hashes.Count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<(char[], char[])> subs = new List<(char[], char[])>();
			string molecule = input[^1];
			//parse
			for (int i = 0; i < input.Length - 2; i++)
			{
				string[] temp = input[i].Split(" ");
				subs.Add((temp[0].ToCharArray(), temp[2].ToCharArray()));
			}

			List<(int, string)> hashes = new List<(int, string)>();
			//start with molecule and remove components until e is left
			hashes.Add((0, molecule));


			List<(int, string)> hashesTemp = new List<(int, string)>();
			int hashesCount = hashes.Count;
			//bool breakOut = false;
			for (int k = 0; k < hashes.Count; k++)
			{
				var hash = hashes[k];

				char[] mol = hash.Item2.ToCharArray();
				foreach (var item in subs)
				{
					//find matching ocurance of sub

					for (int i = 0; i < mol.Length; i++)
					{
						if (mol[i] != item.Item2[0])
							continue;

						int count = 0;
						if (i + item.Item2.Length <= mol.Length)
						{
							for (int j = 0; j < item.Item2.Length; j++)
							{
								if (mol[i + j] == item.Item2[j])
									count++;
							}
						}

						//does not match
						if (count != item.Item2.Length)
							continue;

						//does match
						char[] tempChars = new char[mol.Length - item.Item2.Length + item.Item1.Length];

						Array.Copy(mol, 0, tempChars, 0, i);
						Array.Copy(item.Item1, 0, tempChars, i, item.Item1.Length);
						Array.Copy(mol, i + item.Item2.Length, tempChars, i + item.Item1.Length, (mol.Length - i) - item.Item2.Length);

						string t = string.Join("", tempChars);

						//found the minimum number of operations to get molecule
						//return number
						if (t.Length == 1 && t == "e")
						{

							return (hash.Item1 + 1).ToString();
						}

						//remove longest items to keep speed
						if (hashesTemp.Count > 5)
						{
							hashesTemp.Sort();
							hashesTemp.RemoveRange(0, hashesTemp.Count / 2);
						}

						if (t.Contains("e"))
							continue;

						//if (!hashesTemp.Contains((hash.Item1 + 1, t)))
						hashesTemp.Add((hash.Item1 + 1, t));
					}
				}

				//if (k % 50 == 0)
				//{
				//	Console.WriteLine(k + " " + hashes.Count + " " + hashesTemp.Count + " " + hash.Item2.Length + " " + hash);
				//}
				//hashesTemp.Sort();

				//swap list and start search again
				if (k == hashesCount - 1)
				{
					k = -1;
					hashes = hashesTemp;
					hashesTemp = new List<(int, string)>();

					hashes.Sort();

					hashesCount = hashes.Count;
				}
			}

			//start with e and works its way to the molucule
			//slow, very very slow
			//foreach (var item in electron)
			//{
			//	Solve(subs, molecule, item.Item2, ref depths, 1);
			//}
			//dont need
			return int.MaxValue.ToString();
		}

		//very slow
		// tests every possible combo of subs
		//private void Solve(List<(char[], char[])> subs, string molecule, char[] currentMol, ref int lowestDepth, int currentDepth = 1)
		//{
		//	if (currentMol.Length == molecule.Length)
		//	{
		//		if (string.Join(" ", currentMol).Replace(" ", "") == molecule && currentDepth < lowestDepth)
		//		{
		//			lowestDepth = currentDepth;
		//			//return;
		//		}
		//	}

		//	//to long
		//	if (currentMol.Length > molecule.Length)
		//		return;


		//	char[] mol = new char[currentMol.Length];
		//	Array.Copy(currentMol, mol, mol.Length);
		//	//currentMol.CopyTo(mol, 0);
		//	foreach (var item in subs)
		//	{
		//		for (int i = 0; i < currentMol.Length; i++)
		//		{
		//			if (mol[i] != item.Item1[0])
		//				continue;

		//			if (item.Item1.Length == 2 && mol[i + 1] != item.Item1[1])
		//				continue;

		//			char[] tempChars = new char[mol.Length - item.Item1.Length + item.Item2.Length];

		//			//Console.WriteLine(tempChars.Length);
		//			Array.Copy(mol, 0, tempChars, 0, i);
		//			//Console.WriteLine(item.Item1 + ">" + item.Item2 + ": " + string.Join(",", tempChars));

		//			Array.Copy(item.Item2, 0, tempChars, i, item.Item2.Length);
		//			//Console.WriteLine(item.Item1 + ">" + item.Item2 + ": " + string.Join(",", tempChars));

		//			Array.Copy(mol, i + item.Item1.Length, tempChars, i + item.Item2.Length, (mol.Length - i) - item.Item1.Length);
		//			//Console.WriteLine(item.Item1 + ">" + item.Item2 + ": " + string.Join(",", tempChars));

		//			//string t = string.Join(" ", tempChars).Replace(" ", "");
		//			//Console.WriteLine("final: " + t);

		//			Solve(subs, molecule, tempChars, ref lowestDepth, currentDepth + 1);

		//		}
		//	}
		//}



	}
}
