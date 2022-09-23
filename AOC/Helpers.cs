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
	}
}
