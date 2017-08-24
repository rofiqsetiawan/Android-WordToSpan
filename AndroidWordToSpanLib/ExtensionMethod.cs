// Created by Rofiq Setiawan (rofiqsetiawan@gmail.com)

using System.Collections.Generic;
using System.Text.RegularExpressions;
using Android.Graphics;

#if DEBUG
using Android.Util;
#endif

namespace Bachors.WordToSpan
{
	internal static class ExtensionMethod
	{
		/// <summary>Convert int to Android Color</summary>
		/// <returns>The Android color.</returns>
		/// <param name="color">Color.</param>
		public static Color ToColor(this int color)
		{
			var alpha = (byte)(color >> 24);
			var red = (byte)(color >> 16);
			var green = (byte)(color >> 8);
			var blue = (byte)(color >> 0);
			return Color.Argb(alpha, red, green, blue);
		}

	    /// <summary>
	    /// Gets matched string with regex.
	    /// </summary>
	    /// <param name="inputText">Input text.</param>
	    /// <param name="regexPattern">Regex pattern.</param>
	    public static Dictionary<int, int> GetMatchedString(this string inputText, string regexPattern)
	    {
	        var result = new Dictionary<int, int>();

	        foreach (Match match in Regex.Matches(inputText, regexPattern))
	        {
	            var matchedString = match.Groups[0].Value;
	            var start = match.Groups[0].Index;
	            var length = match.Groups[0].Length;
#if DEBUG
	            Log.Debug("String Matching", $"Get \"{matchedString}\" on index {start} with length {length}.");
#endif
	            result.Add(start, length);
	        }

	        return result;
	    }
    }
}
