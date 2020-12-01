using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
	public abstract class BaseDay
	{
		protected virtual string Day { get; set; }
		public static string InputDir { get; set; }
		protected virtual string Answer1 { get; set; }
		protected virtual string Answer2 { get; set; }
		protected virtual string[] Input { get; set; }

		public void RunSolution1()
		{
			Stopwatch timer = new Stopwatch();
			ReadInput();
			timer.Start();
			string output = Solution1(Input);
			timer.Stop();
			Console.WriteLine("Day {0,2} Answer 1:{1,12} | Correct:{2,6} | Run Time:{3,9}", Day, output, (output == Answer1), timer.Elapsed);
		}

		public void RunTestSolution1(string[] input, string answer)
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();
			string output = Solution1(input);
			timer.Stop();
			Console.WriteLine("Day {0,2} Answer 1:{1,12} | Correct:{2,6} | Run Time:{3,9} | Test", Day, output, (output == answer), timer.Elapsed);
		}

		public void RunSolution2()
		{
			Stopwatch timer = new Stopwatch();
			ReadInput();
			timer.Start();
			string output = Solution2(Input);
			timer.Stop();
			Console.WriteLine("Day {0,2} Answer 2:{1,12} | Correct:{2,6} | Run Time:{3,9}", Day, output, (output == Answer2), timer.Elapsed);
		}

		public void RunTestSolution2(string[] input, string answer)
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();
			string output = Solution2(input);
			timer.Stop();
			Console.WriteLine("Day {0,2} Answer 2:{1,12} | Correct:{2,6} | Run Time:{3,9} | Test", Day, output, (output == answer), timer.Elapsed);
		}

		private string[] ReadInput()
		{
			string[] output = File.ReadAllLines(InputDir + "Day" + Day + ".txt");
			Input = output;
			return output;
		}

		protected abstract string Solution1(string[] input);// { return "BaseDay1"; }
		protected abstract string Solution2(string[] input);// { return "BaseDay2"; }
	}
}
