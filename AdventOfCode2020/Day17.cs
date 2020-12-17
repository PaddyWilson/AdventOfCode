using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day17 : BaseDay
	{
		public Day17()
		{
			Day = "17";
			Answer1 = "324";
			Answer2 = "0";
		}

		protected override string Solution1(string[] input)
		{
			int size = 102;
			int offset = size / 2;
			bool[,,] cubes = new bool[size, size, size];

			//parse input
			for (int x = 0; x < input.Length; x++)
			{
				for (int y = 0; y < input[0].Length; y++)
				{
					if (input[x][y] == '#')
						cubes[offset + x, offset + y, offset] = true;
				}
			}

			int turnCount = 0;
			int maxTurns = 6;

			while (turnCount < maxTurns)
			{
				turnCount++;
				bool[,,] temp = new bool[size, size, size];


				for (int x = 1; x < size - 1; x++)
				{
					for (int y = 1; y < size - 1; y++)
					{
						for (int z = 1; z < size - 1; z++)
						{
							//main area
							int count = 0;

							for (int x1 = x - 1; x1 < x + 2; x1++)
							{
								for (int y1 = y - 1; y1 < y + 2; y1++)
								{
									for (int z1 = z - 1; z1 < z + 2; z1++)
									{
										//dont check it self
										if (x1 == x && y1 == y && z1 == z)
											continue;

										if (cubes[x1, y1, z1] == true)
											count++;
									}
								}
							}

							if ((count == 2 || count == 3) && cubes[x, y, z] == true)
							{
								temp[x, y, z] = true;//stay true
							}
							//else if((count == 2 || count == 3) && cubes[x, y, z] == false)
							//{
							//	cubes[x, y, z] = false;
							//}
							else if (count == 3 && cubes[x, y, z] == false)
							{
								temp[x, y, z] = true;
							}
							else
							{
								temp[x, y, z] = false;
							}

						}//for z
					}//for y
				}//for x

				Array.Copy(temp, cubes, size * size * size);
			}//end while

			int output = 0;
			for (int x = 1; x < size - 1; x++)
			{
				for (int y = 1; y < size - 1; y++)
				{
					for (int z = 1; z < size - 1; z++)
					{
						if (cubes[x, y, z] == true)
						{
							output++;
						}
					}//for z
				}//for y
			}//for x

			return output.ToString();
		}


		//2588 to high
		protected override string Solution2(string[] input)
		{
			int size = 102;
			int offset = size / 2;
			bool[,,,] cubes = new bool[size, size, size, size];

			//parse input
			for (int x = 0; x < input.Length; x++)
			{
				for (int y = 0; y < input[0].Length; y++)
				{
					if (input[x][y] == '#')
						cubes[offset + x, offset + y, offset, offset] = true;
				}
			}

			int turnCount = 0;
			int maxTurns = 6;

			while (turnCount < maxTurns)
			{
				turnCount++;
				bool[,,,] temp = new bool[size, size, size, size];


				for (int x = 1; x < size - 1; x++)
				{
					for (int y = 1; y < size - 1; y++)
					{
						for (int z = 1; z < size - 1; z++)
						{
							for (int w = 1; w < size - 1; w++)
							{
								//main area
								int count = 0;

								for (int x1 = x - 1; x1 < x + 2; x1++)
								{
									for (int y1 = y - 1; y1 < y + 2; y1++)
									{
										for (int z1 = z - 1; z1 < z + 2; z1++)
										{
											for (int w1 = w - 1; w1 < w + 2; w1++)
											{
												//dont check it self
												if (x1 == x && y1 == y && z1 == z && w1 == w)
													continue;

												if (cubes[x1, y1, z1, w1] == true)
													count++;
											}
										}
									}
								}

								if ((count == 2 || count == 3) && cubes[x, y, z, w] == true)
								{
									temp[x, y, z, w] = true;//stay true
								}
								//else if((count == 2 || count == 3) && cubes[x, y, z] == false)
								//{
								//	cubes[x, y, z] = false;
								//}
								else if (count == 3 && cubes[x, y, z, w] == false)
								{
									temp[x, y, z, w] = true;
								}
								else
								{
									temp[x, y, z, w] = false;
								}

							}//for w
						}//for z
					}//for y
				}//for x

				Array.Copy(temp, cubes, size * size * size * size);
			}//end while

			int output = 0;
			for (int x = 1; x < size - 1; x++)
			{
				for (int y = 1; y < size - 1; y++)
				{
					for (int z = 1; z < size - 1; z++)
					{
						for (int w = 1; w < size - 1; w++)
						{
							if (cubes[x, y, z, w] == true)
							{
								output++;
							}
						}
					}//for z
				}//for y
			}//for x

			return output.ToString();
		}
	}
}

