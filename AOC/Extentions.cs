
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC
{
    public static class Extentions
    {
        /// <summary>
        /// Split string to List of int
        /// </summary>
        /// <param name="text"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<int> SplitToInt(this string text, string delimiter = " ")
        {
            List<int> output = new List<int>();
            text.Split(delimiter).ToList().ForEach(f => { output.Add(int.Parse(f)); });
            return output;
        }

        private static readonly Regex extNumber = new Regex(@"\d{1,}", RegexOptions.Multiline | RegexOptions.Compiled);
        public static List<int> ExtractInts(this string text)
        {
            List<int> output = new List<int>();
            extNumber.Matches(text).ToList().ForEach(f => output.Add(int.Parse(f.ToString())));
            return output;
        }

        /// <summary>
        /// Split string to List of long
        /// </summary>
        /// <param name="text"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<long> SplitToLongs(this string text, string delimiter = " ")
        {
            List<long> output = new List<long>();
            text.Split(delimiter).ToList().ForEach(f => { output.Add(long.Parse(f)); });
            return output;
        }

        public static List<long> ExtractLong(this string text)
        {
            List<long> output = new List<long>();
            extNumber.Matches(text).ToList().ForEach(f => output.Add(long.Parse(f.ToString())));
            return output;
        }

        public static List<ulong> ExtractULong(this string text)
        {
            List<ulong> output = new List<ulong>();
            extNumber.Matches(text).ToList().ForEach(f => output.Add(ulong.Parse(f.ToString())));
            return output;
        }

         public static List<BigInteger> ExtractBigInteger(this string text)
        {
            List<BigInteger> output = new List<BigInteger>();
            extNumber.Matches(text).ToList().ForEach(f => output.Add(BigInteger.Parse(f.ToString())));
            return output;
        }

        /// <summary>
        /// Split string to List of string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<string> SplitToString(this string text, string delimiter = " ")
        {
            return text.Split(delimiter).ToList();
        }

        private static readonly Regex extWords = new Regex(@"[a-zA-Z]{1,}", RegexOptions.Multiline | RegexOptions.Compiled);
        public static List<string> ExtractWords(this string text)
        {
            List<string> output = new List<string>();
            extWords.Matches(text).ToList().ForEach(f => output.Add(f.ToString()));
            return output;
        }
    }
}
