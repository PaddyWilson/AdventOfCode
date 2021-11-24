using AOC;
using System;
using System.IO;

namespace AdventOfCode2019
{
	public class Day5 : BaseDay
	{
		public Day5()
		{
			Day = "5";
			Answer1 = "3122865";
			Answer2 = "773660";
		}
		protected override string Solution1(string[] inputFile)
		{
			inputFile = inputFile[0].Split(',');//only one line


			long[] input = new long[inputFile.Length];
			for (long i = 0; i < inputFile.Length; i++)
			{
				input[i] = long.Parse(inputFile[i]);
			}

			//special instructions

			IntCode pc = new IntCode(input);
			long[] arr = {1};
			pc.SetInput(arr);
			pc.Run();

			return pc.output[pc.output.Count - 1].ToString();
		}

		protected override string Solution2(string[] inputFile)
		{
			inputFile = inputFile[0].Split(',');//only one line

			//inputFile = "3,9,8,9,10,9,4,9,99,-1,8".Split(',');//works
			//inputFile = "3,9,7,9,10,9,4,9,99,-1,8".Split(',');
			//inputFile = "3,3,1108,-1,8,3,4,3,99".Split(',');
			//inputFile = "3,3,1107,-1,8,3,4,3,99".Split(',');//works

			//inputFile = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9".Split(',');//works
			//inputFile = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1".Split(',');//works

			//inputFile = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99".Split(',');//works

			long[] input = new long[inputFile.Length];
			for (long i = 0; i < inputFile.Length; i++)
			{
				input[i] = long.Parse(inputFile[i]);
			}

			//special instructions

			IntCode pc = new IntCode(input);
			long[] arr = { 5 };
			pc.SetInput(arr);
			pc.Run();

			return pc.output[0].ToString();
		}
	}
}