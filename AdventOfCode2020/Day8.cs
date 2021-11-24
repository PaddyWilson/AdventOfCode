using System;
using System.Collections.Generic;
using AOC;

namespace AdventOfCode2020
{
	public class Day8 : BaseDay
	{
		private struct Instruction
		{
			public string Instruc;
			public int Number;
		}

		public Day8()
		{
			Day = "8";
			Answer1 = "1262";
			Answer2 = "1643";
		}

		protected override string Solution1(string[] input)
		{
			Instruction[] Instructions = new Instruction[input.Length];
			for (int i = 0; i < input.Length; i++)
			{
				string[] temp = input[i].Split(" ");
				Instructions[i].Instruc = temp[0];
				Instructions[i].Number = int.Parse(temp[1]);
			}

			int InstIndex = 0;
			int Acc = 0;
			List<int> PastInstructions = new List<int>();

			while (!PastInstructions.Contains(InstIndex))
			{
				PastInstructions.Add(InstIndex);
				switch (Instructions[InstIndex].Instruc)
				{
					case "nop":
						InstIndex += 1;
						break;
					case "acc":
						Acc += Instructions[InstIndex].Number;
						InstIndex += 1;
						break;
					case "jmp":
						InstIndex += Instructions[InstIndex].Number;
						break;
					default:
						break;
				}
			}

			return Acc.ToString();
		}

		protected override string Solution2(string[] input)
		{
			Instruction[] InstuctionsOG = new Instruction[input.Length];
			for (int i = 0; i < input.Length; i++)
			{
				string[] temp = input[i].Split(" ");
				InstuctionsOG[i].Instruc = temp[0];
				InstuctionsOG[i].Number = int.Parse(temp[1]);
			}

			int output = 0;
			bool found = false;
			for (int i = 0; i < InstuctionsOG.Length; i++)
			{
				if (found)
					break;

				if (InstuctionsOG[i].Instruc == "acc")
					continue;

				Instruction[] Instuctions = new Instruction[input.Length];
				Array.Copy(InstuctionsOG, Instuctions, InstuctionsOG.Length);

				//change instruction to other one
				if (Instuctions[i].Instruc == "nop")
					Instuctions[i].Instruc = "jmp";
				else if (Instuctions[i].Instruc == "jmp")
					Instuctions[i].Instruc = "nop";

				int InstIndex = 0;
				int Acc = 0;
				List<int> PastInstructions = new List<int>();

				while (!PastInstructions.Contains(InstIndex))
				{
					//hit last instruction. not in loop
					if (InstIndex == Instuctions.Length)
					{
						output = Acc;
						found = true;
						break;
					}

					PastInstructions.Add(InstIndex);
					switch (Instuctions[InstIndex].Instruc)
					{
						case "nop":
							InstIndex += 1;
							break;
						case "acc":
							Acc += Instuctions[InstIndex].Number;
							InstIndex += 1;
							break;
						case "jmp":
							InstIndex += Instuctions[InstIndex].Number;
							break;
						default:
							break;
					}
				}
			}
			return output.ToString();
		}
	}
}
