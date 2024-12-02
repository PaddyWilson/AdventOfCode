
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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

        /// <summary>
        /// Split string to List of long
        /// </summary>
        /// <param name="text"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<long> SplitToLong(this string text, string delimiter = " ")
        {
            List<long> output = new List<long>();
            text.Split(delimiter).ToList().ForEach(f => { output.Add(long.Parse(f)); });
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
    }
}
