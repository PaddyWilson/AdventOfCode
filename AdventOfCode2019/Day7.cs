using System;
using System.IO;
using AOC;

namespace AdventOfCode2019
{
	internal class Day7 : BaseDay
	{
		public Day7()
		{
			Day = "7";
			Answer1 = "77500";
			Answer2 = "22476942";
		}
		protected override string Solution1(string[] inputFile)
		{
			inputFile = inputFile[0].Split(',');//only one line

			//inputFile = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0".Split(',');

			long[] inputCommands = new long[inputFile.Length];
			for (long i = 0; i < inputFile.Length; i++)
			{
				inputCommands[i] = long.Parse(inputFile[i]);
			}

			IntCode cpu = new IntCode(inputCommands);

			// this is so dum but works great :)
			// should probably have brackets
			long highest = 0;
			for (long one = 0; one < 5; one++)
				for (long two = 0; two < 5; two++)
					if (one != two)
						for (long three = 0; three < 5; three++)
							if (one != two && one != three && two != three)
								for (long four = 0; four < 5; four++)
									if (one != two && one != three && one != four && two != three && two != four && three != four)
										for (long five = 0; five < 5; five++)
											if (one != two && one != three && one != four && one != five
												&& two != three && two != four && two != five
												&& three != four && three != five && four != five)
											{
												long temp = 0;
												cpu.Reset();
												cpu.SetInput(new long[] { one, temp });
												cpu.Run();
												temp = cpu.output[cpu.output.Count - 1];

												cpu.Reset();
												cpu.SetInput(new long[] { two, temp });
												cpu.Run();
												temp = cpu.output[cpu.output.Count - 1];

												cpu.Reset();
												cpu.SetInput(new long[] { three, temp });
												cpu.Run();
												temp = cpu.output[cpu.output.Count - 1];

												cpu.Reset();
												cpu.SetInput(new long[] { four, temp });
												cpu.Run();
												temp = cpu.output[cpu.output.Count - 1];

												cpu.Reset();
												cpu.SetInput(new long[] { five, temp });
												cpu.Run();
												temp = cpu.output[cpu.output.Count - 1];

												if (temp > highest)
													highest = temp;
											}
			return highest.ToString();
		}

		protected override string Solution2(string[] inputFile)
		{
			inputFile = inputFile[0].Split(',');//only one line

			//inputFile = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5".Split(',');

			long[] inputCommands = new long[inputFile.Length];
			for (long i = 0; i < inputFile.Length; i++)
				inputCommands[i] = long.Parse(inputFile[i]);
			long highest = 0;

			for (long one = 5; one < 10; one++)
				for (long two = 5; two < 10; two++)
					if (one != two)
						for (long three = 5; three < 10; three++)
							if (one != two && one != three && two != three)
								for (long four = 5; four < 10; four++)
									if (one != two && one != three && one != four && two != three && two != four && three != four)
										for (long five = 5; five < 10; five++)
											if (one != two && one != three && one != four && one != five
												&& two != three && two != four && two != five
												&& three != four && three != five && four != five)
											{
												IntCode cpu1 = new IntCode(inputCommands);
												IntCode cpu2 = new IntCode(inputCommands);
												IntCode cpu3 = new IntCode(inputCommands);
												IntCode cpu4 = new IntCode(inputCommands);
												IntCode cpu5 = new IntCode(inputCommands);

												cpu1.SetInput(new long[] { one });
												cpu2.SetInput(new long[] { two });
												cpu3.SetInput(new long[] { three });
												cpu4.SetInput(new long[] { four });
												cpu5.SetInput(new long[] { five });

												long temp = 0;
												do
												{
													cpu1.AddToInput(temp);
													cpu1.Run(true);

													cpu2.AddToInput(cpu1.output[cpu1.output.Count - 1]);
													cpu2.Run(true);

													cpu3.AddToInput(cpu2.output[cpu2.output.Count - 1]);
													cpu3.Run(true);

													cpu4.AddToInput(cpu3.output[cpu3.output.Count - 1]);
													cpu4.Run(true);

													cpu5.AddToInput(cpu4.output[cpu4.output.Count - 1]);
													cpu5.Run(true);

													temp = cpu5.output[cpu5.output.Count - 1];
												} while (cpu1.IsRunning());

												if (temp > highest)
													highest = temp;
											}
			return highest.ToString();
		}
	}
}