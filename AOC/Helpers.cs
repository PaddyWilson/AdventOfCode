using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AOC
{
	public class Helpers
	{
		public static string CreateMD5(string input)
		{
			// Use input string to calculate MD5 hash
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				// Convert the byte array to hexadecimal string
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				return sb.ToString();
			}
		}

		public static string ConsoleReadKeyWindows()
		{
			//windows closes the console window, linux does not
			//keep open on windows
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				Console.WriteLine("Press any key ...");
				return Console.ReadKey().ToString();
			}

			return "Your on linux. Why do you want input?";
		}
		public static void PrintMatrix<T>(T[,] arr, int size, string delimiter = " ")
		{
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
					Console.Write(arr[i, j].ToString() + delimiter);
				Console.Write("\n");
			}
			Console.Write("\n");
		}

		public static T[,] Rotate<T>(T[,] tile, int size)
		{
			T[,] output = new T[size, size];

			for (int i = 0; i < size / 2; i++)
			{
				for (int j = 0; j < size - i - 1; j++)
				{
					T temp = tile[i, j];
					output[i, j] = tile[size - 1 - j, i];
					output[size - 1 - j, i] = tile[size - 1 - i, size - 1 - j];
					output[size - 1 - i, size - 1 - j] = tile[j, size - 1 - i];
					output[j, size - 1 - i] = temp;
				}
			}

			return output;
		}

		public static T[,] FlipX<T>(T[,] tile, int size)
		{
			T[,] output = new T[size, size];

			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++)
				{
					output[(size-1) - x, y] = tile[x, y];
				}
			}

			return output;
		}
	}
}
