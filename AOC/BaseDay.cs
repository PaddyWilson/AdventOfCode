using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;

namespace AOC
{
	public abstract class BaseDay
	{
		public virtual string Day { get; set; }
		public static string Year { get; set; }
		public static string InputDir { get; set; }
		protected virtual string Answer1 { get; set; }
		protected virtual string Answer2 { get; set; }
		protected virtual string[] Input { get; set; }

		protected virtual List<string[]> TestInput { get; set; }
		protected virtual List<string> TestInputAnswers1 { get; set; }
		protected virtual List<string> TestInputAnswers2 { get; set; }

		public virtual void RunAll()
		{
			RunAllSolution1Tests();
            RunSolution1();
            RunAllSolution2Tests();
            RunSolution2();
		}

		protected void RunSolution(int solution, Func<string[], string> func, string answer, string[] input)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    			Console.ForegroundColor = ConsoleColor.White;

			Console.Write("{1} Day {0,2} ", Day, Year);

			Stopwatch timer = new Stopwatch();
			//ReadInput();

			string output = "";
			timer.Start();
			output = func(input);
			timer.Stop();

			if (output == answer)
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			else
				Console.BackgroundColor = ConsoleColor.Red;

			Console.Write("Answer {0,1}:{1,16} | Expected:{2,16} | Correct:{3,6} | Run Time:{4,9}", solution, output, answer, (output == answer), timer.Elapsed);
			Console.ResetColor();
			Console.WriteLine();
		}

		public virtual void RunSolution1()
		{
			RunSolution(1, Solution1, Answer1, ReadInput());
		}

		public virtual void RunSolution2()
		{
			RunSolution(2, Solution2, Answer2, ReadInput());
		}

		protected void RunSolutionTest(int solution,  Func<string[], string> func, string[] input, string answer)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    			Console.ForegroundColor = ConsoleColor.White;

			Console.Write("{1} Day {0,2} ", Day, Year);

			Stopwatch timer = new Stopwatch();

			string output = "";
			timer.Start();
			output = func(input);
			timer.Stop();

			if (output == answer)
				Console.BackgroundColor = ConsoleColor.DarkBlue;
			else
				Console.BackgroundColor = ConsoleColor.DarkRed;

			Console.Write("Answer {0,1}:{1,16} | Expected:{2,16} | Correct:{3,6} | Run Time:{4,9} TEST", solution, output, answer, (output == answer), timer.Elapsed);
			Console.ResetColor();
			Console.WriteLine();
		}

		public virtual void RunTestSolution1(string[] input, string answer)
		{
			RunSolutionTest(1, Solution1, input, answer);
		}

		public virtual void RunTestSolution2(string[] input, string answer)
		{
			RunSolutionTest(2, Solution2, input, answer);
		}

		protected virtual string[] ReadInput()
		{
			string[] output = null;
			//windows does not care about caps but linux does
			if (File.Exists(InputDir + "Day" + Day + ".txt"))
				output = File.ReadAllLines(InputDir + "Day" + Day + ".txt");
			else if (File.Exists(InputDir + "day" + Day + ".txt"))
				output = File.ReadAllLines(InputDir + "day" + Day + ".txt");
			else
				throw new InvalidDataException("Cant find file \"" + InputDir + "Day" + Day + ".txt" + "\"");
			Input = output;
			return output;
		}

        protected virtual string[] ReadInput(string filename)
        {
            string[] output = null;
            //windows does not care about caps but linux does
            if (File.Exists(InputDir + filename))
                output = File.ReadAllLines(InputDir + filename);
            else
                throw new InvalidDataException("Cant find file \"" + InputDir + filename + "\"");
            return output;
        }

        public virtual void AddTestInput(string[] input, params string[] answers)
		{
			if (TestInput == null)
			{
				TestInput = new List<string[]>();
				TestInputAnswers1 = new List<string>();
				TestInputAnswers2 = new List<string>();
			}

			TestInput.Add(input);
			TestInputAnswers1.Add(answers[0]);
			if(answers.Length > 1)
				TestInputAnswers2.Add(answers[1]);
			else
				TestInputAnswers2.Add("");
		}

		public virtual void AddTestInputFromFile(string filename, params string[] answers)
		{
			AddTestInput(ReadInput(filename), answers);
		}

		public virtual void RunAllTests()
		{
			RunAllSolution1Tests();
			RunAllSolution2Tests();
		}

		protected void RunAllSolutionTests(List<string[]> testInputs, List<string> testAnswers, 
			Action<string[], string> func)
		{
			if (testInputs != null)
				for (int i = 0; i < testInputs.Count; i++)
					if (testAnswers[i] != "")//no testing answer. don't run
						func(testInputs[i], testAnswers[i]);
		}

		public virtual void RunAllSolution1Tests()
		{
			RunAllSolutionTests(TestInput, TestInputAnswers1, RunTestSolution1);
		}

		public virtual void RunAllSolution2Tests()
		{
			RunAllSolutionTests(TestInput, TestInputAnswers2, RunTestSolution2);
		}
		protected abstract string Solution1(string[] input);
		protected abstract string Solution2(string[] input);
	}
}
