using System;
using System.IO;
using AOC;

namespace AdventOfCode2019
{
	internal class Day8 : BaseDay
	{
		public Day8()
		{
			Day = "8";
			Answer1 = "2064";
			Answer2 = "This is a picture that says:KAUZA";
		}

		protected override string Solution1(string[] input)
		{
			long[] data = new long[input[0].Length];
			for (long i = 0; i < input[0].Length; i++)
				data[i] = long.Parse(new char[] { input[0][(int)i] });

			long height = 25;
			long length = 6;
			long layers = data.Length / (height * length);

			long[,,] image = new long[height, length, layers];

			long pos = 0;
			for (long lay = 0; lay < layers; lay++)
			{
				for (long y = 0; y < length; y++)
				{
					for (long x = 0; x < height; x++)
					{
						image[x, y, lay] = data[pos];
						pos++;
					}
				}
			}

			long layerLowest = 0;
			long digit0Lowest = long.MaxValue - 1;
			long digit1Lowest = long.MaxValue - 1;
			long digit2Lowest = long.MaxValue - 1;

			for (long lay = 0; lay < layers; lay++)
			{
				long digit0 = 0;
				long digit1 = 0;
				long digit2 = 0;

				for (long y = 0; y < length; y++)
				{
					for (long x = 0; x < height; x++)
					{
						if (image[x, y, lay] == 0)
							digit0++;
						if (image[x, y, lay] == 1)
							digit1++;
						if (image[x, y, lay] == 2)
							digit2++;
					}
				}

				if (digit0 < digit0Lowest)
				{
					layerLowest = lay;
					digit0Lowest = digit0;
					digit1Lowest = digit1;
					digit2Lowest = digit2;
				}
			}

			return (digit1Lowest * digit2Lowest).ToString();
		}


		protected override string Solution2(string[] input)
		{
			long[] data = new long[input[0].Length];
			for (long i = 0; i < input[0].Length; i++)
				data[i] = long.Parse(new char[] { input[0][(int)i] });

			long height = 25;
			long length = 6;
			long layers = data.Length / (height * length);

			long[,,] image = new long[height, length, layers];

			long pos = 0;
			for (long lay = 0; lay < layers; lay++)
			{
				for (long y = 0; y < length; y++)
				{
					for (long x = 0; x < height; x++)
					{
						image[x, y, lay] = data[pos];
						pos++;
					}
				}
			}

			long[,] imageRender = new long[height, length];
			for (long y = 0; y < length; y++)
				for (long x = 0; x < height; x++)
					imageRender[x, y] = 9;


			for (long y = 0; y < length; y++)
			{
				for (long x = 0; x < height; x++)
				{
					for (long lay = 0; lay < layers; lay++)
					{
						if (image[x, y, lay] < 2)
						{
							imageRender[x, y] = image[x, y, lay];
							break;
						}
					}
				}
			}

			for (long y = 0; y < length; y++)
			{
				for (long x = 0; x < height; x++)
				{
					Console.Write(imageRender[x, y]);
				}
				Console.WriteLine();
			}

			return "This is a picture that says:KAUZA";
		}
	}
}