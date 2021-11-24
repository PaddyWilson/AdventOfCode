using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Xml;

namespace AdventOfCode2019
{
	public class IntCode
	{
		public List<long> input;
		long inputCount = 0;
		public List<long> output;
		long outputCount = 0;

		bool running = false;
		bool isPaused = false;

		long[] memory;
		long[] backupMemory;
		long instructionAddress = 0;
		long relativeBase = 0;

		public IntCode(long[] code, int additionalMemorySize = 0)
		{
			memory = new long[code.Length];
			backupMemory = new long[code.Length];
			Array.Copy(code, memory, code.Length);
			Array.Copy(code, backupMemory, code.Length);

			Array.Resize(ref memory, memory.Length + additionalMemorySize);
			Array.Resize(ref backupMemory, backupMemory.Length + additionalMemorySize);

			input = new List<long>();
			output = new List<long>();
		}

		public void Reset()
		{
			Array.Copy(backupMemory, memory, backupMemory.Length);
			running = false;
			isPaused = false;
			instructionAddress = 0;
			inputCount = 0;
			relativeBase = 0;
		}

		public void SetInput(long[] input)
		{
			this.input = new List<long>(input);
			inputCount = 0;
		}

		public void AddToInput(long input)
		{
			this.input.Add(input);
		}

		public long GetLastOutput()
		{
			return output[output.Count - 1];
		}

		public bool IsRunning() { return running; }

		public long Run(bool pauseOnOutput = false)
		{
			bool test = false;

			running = true;
			isPaused = false;
			while (running && !isPaused)
			{
				isPaused = false;

				//position mode DEFAULT
				long param1Add = 0, param2Add = 0, param3Add = 0;
				long opCode = memory[instructionAddress];

				long tempOP = opCode;

				if (instructionAddress + 1 < memory.Length)
					param1Add = memory[instructionAddress + 1];
				if (instructionAddress + 2 < memory.Length)
					param2Add = memory[instructionAddress + 2];
				if (instructionAddress + 3 < memory.Length)
					param3Add = memory[instructionAddress + 3];

				//position mode = 0 (Default) , points to another memory location to get data
				//immediate mode = 1, the data to get is its own
				//relatice mode = 2, 

				if (opCode > 99)
				{
					//get base opCode
					string reversedOp = string.Concat(opCode.ToString().Reverse());
					opCode = long.Parse(opCode.ToString().Remove(0, opCode.ToString().Length - 2));

					//first param
					if (reversedOp.Length > 2)
					{
						//immediate mode = 1
						if ('1' == reversedOp[2])
							param1Add = instructionAddress + 1;
						//relative mode = 2
						if ('2' == reversedOp[2])
							param1Add = relativeBase + memory[instructionAddress + 1];
					}
					//second param
					if (reversedOp.Length > 3)
					{
						if ('1' == reversedOp[3])
							param2Add = instructionAddress + 2;
						//relative mode = 2
						if ('2' == reversedOp[3])
							param2Add = relativeBase + memory[instructionAddress + 2];
					}
					//third param
					if (reversedOp.Length > 4)
					{
						if ('1' == reversedOp[4])
							param3Add = instructionAddress + 3;
						//relative mode = 2
						if ('2' == reversedOp[4])
							param3Add = relativeBase + memory[instructionAddress + 3];
					}
				}

				//run the instuction
				switch (opCode)
				{
					case 99://halt
						running = false;
						if (test) Console.WriteLine(tempOP + " HLT");
						instructionAddress += 1;
						break;
					case 1://add
						memory[param3Add] = memory[param1Add] + memory[param2Add];
						if (test) Console.WriteLine(tempOP + " ADD {0} {1} {2} : Addresses {3},{4},{5} out", memory[param1Add], memory[param2Add], memory[param3Add], param1Add, param2Add, param3Add);
						instructionAddress += 4;
						break;
					case 2://multiply
						memory[param3Add] = memory[param1Add] * memory[param2Add];
						if (test) Console.WriteLine(tempOP + " MLT {0} {1} {2} : Addresses {3},{4},{5} out", memory[param1Add], memory[param2Add], memory[param3Add], param1Add, param2Add, param3Add);
						instructionAddress += 4;
						break;
					case 3://input
						memory[param1Add] = input[(int)inputCount];
						if (test) Console.WriteLine(tempOP + " INP {0} : Addresses {1} out", memory[param1Add], param1Add);
						inputCount++;
						//if (inputCount >= input.Count)
						//inputCount = 0;
						instructionAddress += 2;
						break;
					case 4://output
						output.Add(memory[param1Add]);
						if (test) Console.WriteLine(tempOP + " OUT {0} : Addresses {1}", memory[param1Add], param1Add);
						instructionAddress += 2;
						if (pauseOnOutput)
							isPaused = true;
						break;
					case 5://jump-if-true
						if (memory[param1Add] != 0)
							instructionAddress = memory[param2Add];
						else
							instructionAddress += 3;
						if (test) Console.WriteLine(tempOP + " JIT {0} {1} : Addresses {2},{3}", memory[param1Add], memory[param2Add], param1Add, param2Add);
						break;
					case 6://jump-if-false
						if (memory[param1Add] == 0)
							instructionAddress = memory[param2Add];
						else
							instructionAddress += 3;
						if (test) Console.WriteLine(tempOP + " JIF {0} {1} : Addresses {2},{3}", memory[param1Add], memory[param2Add], param1Add, param2Add);
						break;
					case 7://less than
						if (memory[param1Add] < memory[param2Add])
							memory[param3Add] = 1;
						else
							memory[param3Add] = 0;
						if (test) Console.WriteLine(tempOP + " LT< {0} {1} {2} : Addresses {3},{4},{5} out", memory[param1Add], memory[param2Add], memory[param3Add], param1Add, param2Add, param3Add);
						instructionAddress += 4;
						break;
					case 8://equals
						if (memory[param1Add] == memory[param2Add])
							memory[param3Add] = 1;
						else
							memory[param3Add] = 0;
						if (test) Console.WriteLine(tempOP + " EQ= {0} {1} {2} : Addresses {3},{4},{5} out", memory[param1Add], memory[param2Add], memory[param3Add], param1Add, param2Add, param3Add);
						instructionAddress += 4;
						break;
					case 9://relative base
						relativeBase += memory[param1Add];
						if (test) Console.WriteLine(tempOP + " RLB {0} : Addresses {1}", memory[param1Add], param1Add);
						instructionAddress += 2;
						break;
				}
			}
			return memory[0];
		}

	}
}
