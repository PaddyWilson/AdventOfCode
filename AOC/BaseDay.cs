using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

		protected virtual List<string[]> TestInput { get; private set; }
		protected virtual List<string> TestInputAnswers1 { get; private set; }
		protected virtual List<string> TestInputAnswers2 { get; private set; }

		public void RunSolution1()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    			Console.ForegroundColor = ConsoleColor.White;

			Console.Write("{1} Day {0,2} ", Day, Year);

			Stopwatch timer = new Stopwatch();
			ReadInput();

			timer.Start();
			string output = Solution1(Input);
			timer.Stop();
			
			if ((output == Answer1))
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			else
				Console.BackgroundColor = ConsoleColor.Red;

			Console.Write("Answer 1:{1,16} | Expected:{4,16} | Correct:{2,6} | Run Time:{3,9}", Day, output, (output == Answer1), timer.Elapsed, Answer1, Year);
			Console.ResetColor();
			Console.WriteLine();
		}

		public void RunTestSolution1(string[] input, string answer)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    			Console.ForegroundColor = ConsoleColor.White;
			
			Console.Write("{1} Day {0,2} ", Day, Year);

			Stopwatch timer = new Stopwatch();
			timer.Start();
			string output = Solution1(input);
			timer.Stop();
			
			if ((output == answer))
				Console.BackgroundColor = ConsoleColor.DarkBlue;
			else
				Console.BackgroundColor = ConsoleColor.DarkRed;

			Console.Write("Answer 1:{1,16} | Expected:{4,16} | Correct:{2,6} | Run Time:{3,9} | Test", Day, output, (output == answer), timer.Elapsed, answer, Year);
			Console.ResetColor();
			Console.WriteLine();
		}

		public void RunSolution2()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    			Console.ForegroundColor = ConsoleColor.White;
			
			Console.Write("{1} Day {0,2} ", Day, Year);

			Stopwatch timer = new Stopwatch();
			ReadInput();
			timer.Start();
			string output = Solution2(Input);
			timer.Stop();
			
			if ((output == Answer2))
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			else
				Console.BackgroundColor = ConsoleColor.Red;

            Console.Write("Answer 2:{1,16} | Expected:{4,16} | Correct:{2,6} | Run Time:{3,9}", Day, output, (output == Answer2), timer.Elapsed, Answer2, Year);
			Console.ResetColor();
			Console.WriteLine();
		}

		public void RunTestSolution2(string[] input, string answer)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    			Console.ForegroundColor = ConsoleColor.White;
			
			Console.Write("{1} Day {0,2} ", Day, Year);

			Stopwatch timer = new Stopwatch();
			timer.Start();
			string output = Solution2(input);
			timer.Stop();
			
			if ((output == answer))
				Console.BackgroundColor = ConsoleColor.DarkBlue;
			else
				Console.BackgroundColor = ConsoleColor.DarkRed;

			Console.Write("Answer 2:{1,16} | Expected:{4,16} | Correct:{2,6} | Run Time:{3,9} | Test", Day, output, (output == answer), timer.Elapsed, answer, Year);
			Console.ResetColor();
			Console.WriteLine();
		}

		private string[] ReadInput()
		{
			string[] output = null;
			//windows does not care about caps but linux does
			if (File.Exists(InputDir + "Day" + Day + ".txt"))
			{
				output = File.ReadAllLines(InputDir + "Day" + Day + ".txt");
			}
			else if (File.Exists(InputDir + "day" + Day + ".txt"))
			{
				output = File.ReadAllLines(InputDir + "day" + Day + ".txt");
			}
			else
			{
				throw new InvalidDataException("Cant find file \"" + InputDir + "Day" + Day + ".txt" + "\"");
			}
			Input = output;
			return output;
		}

        protected string[] ReadInput(string filename)
        {
            string[] output = null;
            //windows does not care about caps but linux does
            if (File.Exists(InputDir + filename))
            {
                output = File.ReadAllLines(InputDir + filename);
            }
            else if (File.Exists(InputDir + filename))
            {
                output = File.ReadAllLines(InputDir + filename);
            }
            else
            {
                throw new InvalidDataException("Cant find file \"" + InputDir + filename + "\"");
            }
            Input = output;
            return output;
        }

        public void AddTestInput(string[] input, string answer1, string answer2)
		{
			if (TestInput == null)
			{
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
			if (TestInput == null)
			{
				TestInput = new List<string[]>();
				TestInputAnswers1 = new List<string>();
				TestInputAnswers2 = new List<string>();
			}

			string[] temp = null;
			if (File.Exists(InputDir + input))
			{
				temp = File.ReadAllLines(InputDir + input);
			}
			else if (File.Exists(InputDir + input))
			{
				temp = File.ReadAllLines(InputDir + input);
			}
			else
			{
				throw new InvalidDataException("Cant find file \"" + InputDir + input + "\"");
			}

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
			if (TestInput != null)
				for (int i = 0; i < TestInput.Count; i++)
					if (TestInputAnswers1[i] != "")//no testing answer. don't run
						RunTestSolution1(TestInput[i], TestInputAnswers1[i]);
		}

		public void RunAllSolution2Tests()
		{
			if (TestInput != null)
				for (int i = 0; i < TestInput.Count; i++)
					if (TestInputAnswers2[i] != "")//no testing answer. don't run
						RunTestSolution2(TestInput[i], TestInputAnswers2[i]);
		}

		protected abstract string Solution1(string[] input);// { return "BaseDay1"; }
		protected abstract string Solution2(string[] input);// { return "BaseDay2"; }
	}
}
