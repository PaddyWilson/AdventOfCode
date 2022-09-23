using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AOC
{
	public abstract class BaseDay
	{
		protected virtual string Day { get; set; }
		public static string Year { get; set; }
		public static string InputDir { get; set; }
		protected virtual string Answer1 { get; set; }
		protected virtual string Answer2 { get; set; }
		protected virtual string[] Input { get; set; }

		private bool TestInit = false;
		protected virtual List<string[]> TestInput { get; private set; }
		protected virtual List<string> TestInputAnswers1 { get; private set; }
		protected virtual List<string> TestInputAnswers2 { get; private set; }

		public void RunSolution1()
		{
			Stopwatch timer = new Stopwatch();
			ReadInput();
			timer.Start();
			string output = Solution1(Input);
			timer.Stop();

			if((output == Answer1))
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			else
				Console.BackgroundColor = ConsoleColor.Red;

			Console.Write("{5} Day {0,2} Answer 1:{1,16} | Expected:{4,16} | Correct:{2,6} | Run Time:{3,9}", Day, output, (output == Answer1), timer.Elapsed, Answer1, Year);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine();
		}

		public void RunTestSolution1(string[] input, string answer)
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();
			string output = Solution1(input);
			timer.Stop();

			if ((output == answer))
				Console.BackgroundColor =  ConsoleColor.DarkBlue;
			else
				Console.BackgroundColor = ConsoleColor.DarkRed;

			Console.Write("{5} Day {0,2} Answer 1:{1,16} | Expected:{4,16} | Correct:{2,6} | Run Time:{3,9} | Test", Day, output, (output == answer), timer.Elapsed, answer, Year);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine();
		}

		public void RunSolution2()
		{
			Stopwatch timer = new Stopwatch();
			ReadInput();
			timer.Start();
			string output = Solution2(Input);
			timer.Stop();

			if ((output == Answer2))
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			else
				Console.BackgroundColor = ConsoleColor.Red;

			Console.Write("{5} Day {0,2} Answer 2:{1,16} | Expected:{4,16} | Correct:{2,6} | Run Time:{3,9}", Day, output, (output == Answer2), timer.Elapsed, Answer2, Year);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine();
		}

		public void RunTestSolution2(string[] input, string answer)
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();
			string output = Solution2(input);
			timer.Stop(); 

			if ((output == answer))
				Console.BackgroundColor = ConsoleColor.DarkBlue;
			else
				Console.BackgroundColor = ConsoleColor.DarkRed;

			Console.Write("{5} Day {0,2} Answer 2:{1,16} | Expected:{4,16} | Correct:{2,6} | Run Time:{3,9} | Test", Day, output, (output == answer), timer.Elapsed, answer, Year);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine();
		}

		private string[] ReadInput()
		{
			string[] output = File.ReadAllLines(InputDir + "Day" + Day + ".txt");
			Input = output;
			return output;
		}

		public void AddTestInput(string[] input, string answer1, string answer2)
		{
			if (!TestInit)
			{
				TestInit = true;
				TestInput = new List<string[]>();
				TestInputAnswers1 = new List<string>();
				TestInputAnswers2 = new List<string>();
			}

			TestInput.Add(input);
			TestInputAnswers1.Add(answer1);
			TestInputAnswers2.Add(answer2);
		}

		public void AddTestInputFromFile(string input, string answer1, string answer2)
		{
			if (!TestInit)
			{
				TestInit = true;
				TestInput = new List<string[]>();
				TestInputAnswers1 = new List<string>();
				TestInputAnswers2 = new List<string>();
			}

			string[] temp = File.ReadAllLines(InputDir + input);

			TestInput.Add(temp);
			TestInputAnswers1.Add(answer1);
			TestInputAnswers2.Add(answer2);
		}

		public void RunAllTests()
		{
			RunAllSolution1Tests();
			RunAllSolution2Tests();
		}

		public void RunAllSolution1Tests()
		{
			if (TestInit)
				for (int i = 0; i < TestInput.Count; i++)
					if (TestInputAnswers1[i] != "")//no testing answer. don't run
						RunTestSolution1(TestInput[i], TestInputAnswers1[i]);
		}

		public void RunAllSolution2Tests()
		{
			if (TestInit)
				for (int i = 0; i < TestInput.Count; i++)
					if (TestInputAnswers2[i] != "")//no testing answer. don't run
						RunTestSolution2(TestInput[i], TestInputAnswers2[i]);
		}

		protected abstract string Solution1(string[] input);// { return "BaseDay1"; }
		protected abstract string Solution2(string[] input);// { return "BaseDay2"; }
	}
}
