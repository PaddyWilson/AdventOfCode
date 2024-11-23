using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace AOC
{
	public abstract class BaseDayEC : BaseDay
	{
		public virtual string Answer3 {get;set;}

		public List<string[]> Inputs = new List<string[]>();

		protected virtual List<string> TestInputAnswers3 { get; private set; }

		protected override abstract string Solution1(string[] input);
		protected override abstract string Solution2(string[] input);
		protected abstract string Solution3(string[] input);

		public override void RunAll()
		{
			base.RunAll();
			RunAllSolution3Tests();
			RunSolution3();
		}

		public override void RunSolution1()
		{
			ReadInput();
			if(Inputs.Count >= 1)
				RunSolution(1, Solution1, Answer1, Inputs[0]);
			else
				Console.WriteLine("No Solution 1 input");
		}

		public override void RunSolution2()
		{
			ReadInput();
			if(Inputs.Count >= 2)
				RunSolution(2, Solution2, Answer2, Inputs[1]);
			else
				Console.WriteLine("No Solution 2 input");
		}

		public void RunSolution3()
		{
			ReadInput();
			if(Inputs.Count >= 3)
				RunSolution(3, Solution3, Answer3, Inputs[2]);
			else
				Console.WriteLine("No Solution 3 input");
		}
		
		public void RunTestSolution3(string[] input, string answer)
		{
			RunSolutionTest(3, Solution3, input, answer);
		}

		protected override string[] ReadInput()
		{
			string[] output = null;

			string day = Day;
			if(Day.Length == 1)
			{
				day = "0"+day;
			}

			if(File.Exists(InputDir + "everybody_codes_e"+Year+"_q"+day+"_p1.txt")){
				Inputs.Add(File.ReadAllLines(InputDir + "everybody_codes_e"+Year+"_q"+day+"_p1.txt"));
			}
			if(File.Exists(InputDir + "everybody_codes_e"+Year+"_q"+day+"_p2.txt")){
				Inputs.Add(File.ReadAllLines(InputDir + "everybody_codes_e"+Year+"_q"+day+"_p2.txt"));
			}
			if(File.Exists(InputDir + "everybody_codes_e"+Year+"_q"+day+"_p3.txt")){
				Inputs.Add(File.ReadAllLines(InputDir + "everybody_codes_e"+Year+"_q"+day+"_p3.txt"));
			}
			return output;
		}

        public override void AddTestInput(string[] input, params string[] answers)
		{
			if (TestInput == null)
			{
				TestInput = new List<string[]>();
				TestInputAnswers1 = new List<string>();
				TestInputAnswers2 = new List<string>();
				TestInputAnswers3 = new List<string>();
			}

			TestInput.Add(input);
			TestInputAnswers1.Add(answers[0]);
			if(answers.Length > 1)
				TestInputAnswers2.Add(answers[1]);
			else
				TestInputAnswers2.Add("");
			if(answers.Length > 2)
				TestInputAnswers3.Add(answers[2]);
			else
				TestInputAnswers3.Add("");
		}

		public override void RunAllTests()
		{
			RunAllSolution1Tests();
			RunAllSolution2Tests();
			RunAllSolution3Tests();
		}

		public void RunAllSolution3Tests()
		{
			RunAllSolutionTests(TestInput, TestInputAnswers3, RunTestSolution3);
		}
	}
}
