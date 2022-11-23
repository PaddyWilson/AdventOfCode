using System;
using System.Collections;
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

		public static void PrintMatrix(bool[,] arr, int size, string delimiter = " ")
		{
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (arr[i, j])
						Console.Write('#'.ToString() + delimiter);
					else
						Console.Write('.'.ToString() + delimiter);

				}
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
					output[(size - 1) - x, y] = tile[x, y];
				}
			}

			return output;
		}

		public static List<List<T>> GetPermutations<T>(List<T> items)
		{
			List<List<T>> output = new List<List<T>>();
			List<T> itemsLeft = new List<T>(items);
			List<T> current = new List<T>();

			for (int i = itemsLeft.Count - 1; i > -1; i--)
			{
				T temp = itemsLeft[0];
				current.Add(temp);
				itemsLeft.Remove(temp);

				GetPermutations(current, itemsLeft, ref output);

				itemsLeft.Add(temp);
				current.Remove(temp);
			}

			return output;
		}

		private static void GetPermutations<T>(List<T> current, List<T> itemsLeft, ref List<List<T>> output)
		{
			if (itemsLeft.Count == 0)
			{
				output.Add(new List<T>(current));
				return;
			}

			for (int i = itemsLeft.Count - 1; i > -1; i--)
			{
				T temp = itemsLeft[0];
				current.Add(temp);
				itemsLeft.Remove(temp);

				GetPermutations(current, itemsLeft, ref output);

				itemsLeft.Add(temp);
				current.Remove(temp);
			}
		}
		public static bool ArrayMatch<T>(T[] array1, T[] array2)
		{
			int matchCount = 0;
			int count = 0;

			//cant be same different sizes
			if (array1.Length != array2.Length) 
				return false;

			EqualityComparer<T> comparer = EqualityComparer<T>.Default;
			for (int i = 0; i < array1.Length; i++)
			{
				if (comparer.Equals(array1[i], array2[i]))
					matchCount++;
				count++;
			}

			if (matchCount == count)
				return true;
			return false;
		}

		public static bool ArrayMatch<T>(T[,] array1, T[,] array2, int width, int height)
		{
			int matchCount = 0;
			int count = 0;
			EqualityComparer<T> comparer = EqualityComparer<T>.Default;
			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
				{
					if (comparer.Equals(array1[x, y], array2[x, y]))
						matchCount++;
					count++;
				}

			if (matchCount == count)
				return true;
			return false;
		}

	}

	//for using an array as dictionary key
	// example
	// Dictionary<int[], int> dicName = new Dictionary<int[], int>(new MyEqualityComparer());
	public class MyEqualityComparer : IEqualityComparer<int[]>
	{
		public bool Equals(int[] x, int[] y)
		{
			if (x.Length != y.Length)
			{
				return false;
			}
			for (int i = 0; i < x.Length; i++)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		public int GetHashCode(int[] obj)
		{
			int result = 17;
			for (int i = 0; i < obj.Length; i++)
			{
				unchecked
				{
					result = result * 23 + obj[i];
				}
			}
			return result;
		}
	}
}
