using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Jr.Backend.Libs.Utilities
{
    public static class StringUtilities
    {
        /// <summary>
        /// Alternates cases between letters of a string, first letter's case stays the same.
        /// Ex.: longstring -> lOnGsTrInG
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string AlternateCases(string input)
        {
            if (input.Length == 0) return string.Empty;
            if (input.Length == 1) return input; //Cannot automatically alternate

            var firstIsUpper = string.CompareOrdinal(input.Substring(0, 1), input.Substring(0, 1).ToUpper()) != 0;
            var ret = input.Substring(0, 1);
            for (var i = 1; i < input.Length; i++)
            {
                if (firstIsUpper)
                    ret += input.Substring(i, 1).ToUpper();
                else
                    ret += input.Substring(i, 1).ToLower();

                firstIsUpper = !firstIsUpper;
            }

            return ret;
        }

        /// <summary>
        /// Returns an array converted into a strings.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ArrayToString(Array input, string separator)
        {
            var ret = new StringBuilder();
            for (var i = 0; i < input.Length; i++)
            {
                ret.Append(input.GetValue(i));
                if (i != input.Length - 1)
                    ret.Append(separator);
            }
            return ret.ToString();
        }

        /// <summary>
        /// Capitalizes a word or sentence.
        /// Ex.: this is a sentence -> This is a sentence.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Capitalize(string input)
        {
            if (input.Length == 0) return string.Empty;
            if (input.Length == 1) return input.ToUpper();

            return input.Substring(0, 1).ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Gets the char in a string from a starting position plus the index
        /// </summary>
        /// <param name="input"></param>
        /// <param name="startingIndex"></param>
        /// <param name="countIndex"></param>
        /// <returns></returns>
        public static char CharMid(string input, int startingIndex, int countIndex)
        {
            if (startingIndex + countIndex < input.Length)
            {
                var str = input.Substring(startingIndex + countIndex, 1);
                return str[0];
            }
            else
                return new char();
        }

        /// <summary>
        /// Gets the char in a string at a given position, but counting from right to left.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static char CharRight(string input, int index)
        {
            if (input.Length - index - 1 >= input.Length ||
                input.Length - index - 1 < 0)
                return new char();

            var str = input.Substring(input.Length - index - 1, 1);
            return str[0];
        }

        /// <summary>
        /// Counts total number of a char or chars in a string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="chars"></param>
        /// <param name="ignoreCases"></param>
        /// <returns></returns>
        public static int CountTotal(string input, string chars, bool ignoreCases)
        {
            var count = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (!(i + chars.Length > input.Length) &&
                    string.Compare(input.Substring(i, chars.Length), chars, ignoreCases) == 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Find the position of "searchString" withing the given "mainString"
        /// </summary>
        /// <param name="mainString"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static int FindStringPositionWithinString(this string mainString, string searchString)
        {
            return IsNull(mainString) || IsNull(searchString)
                ? -1
                : mainString.IndexOf(searchString, StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns the initials of a name or sentence.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="capitalize"></param>
        /// <param name="includeSpace"></param>
        /// <returns></returns>
        public static string GetInitials(string input, bool capitalize, bool includeSpace)
        {
            var words = input.Split(new char[] { ' ' });

            for (var i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                    if (capitalize)
                        words[i] = words[i].Substring(0, 1).ToUpper() + ".";
                    else
                        words[i] = words[i].Substring(0, 1) + ".";
            }

            if (includeSpace)
                return string.Join(" ", words);
            else
                return string.Join("", words);
        }

        /// <summary>
        /// Capitalizes the first letter of every word
        /// Ex. The Big Story.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetTitle(string input)
        {
            var words = input.Split(new char[] { ' ' });

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                    words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1);
            }

            return string.Join(" ", words);
        }

        /// <summary>
        /// Checks if the string contains numbers.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasNumbers(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, "\\d+");
        }

        /// <summary>
        /// Checks to see if a string contains vowels.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasVowels(string input)
        {
            string currentLetter;
            for (var i = 0; i < input.Length; i++)
            {
                currentLetter = input.Substring(i, 1);

                if (string.Compare(currentLetter, "a", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(currentLetter, "e", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(currentLetter, "i", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(currentLetter, "o", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(currentLetter, "u", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    //A vowel found
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns all the locations of a char in a string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static int[] IndexOfAll(string input, string chars)
        {
            var indices = new List<int>();
            for (var i = 0; i < input.Length; i++)
            {
                if (input.Substring(i, 1) == chars)
                    indices.Add(i);
            }

            if (indices.Count == 0)
                indices.Add(-1);

            return indices.ToArray();
        }

        /// <summary>
        /// Checks if string is numbers and letters
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsAlphaNumberic(string input)
        {
            char currentLetter;
            for (var i = 0; i < input.Length; i++)
            {
                currentLetter = input[i];

                if (!(Convert.ToInt32(currentLetter) >= 48 && Convert.ToInt32(currentLetter) <= 57) &&
                    !(Convert.ToInt32(currentLetter) >= 65 && Convert.ToInt32(currentLetter) <= 90) &&
                    !(Convert.ToInt32(currentLetter) >= 97 && Convert.ToInt32(currentLetter) <= 122))
                {
                    //Not a number or a letter
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks to see if a string has alternate cases
        /// Ex.: lOnGsTrInG -> True
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsAlternateCases(string input)
        {
            if (input.Length <= 1) return false;

            var lastIsUpper = string.CompareOrdinal(input.Substring(0, 1), input.Substring(0, 1).ToUpper()) == 0;

            for (var i = 1; i < input.Length; i++)
            {
                if (lastIsUpper)
                {
                    if (string.CompareOrdinal(input.Substring(i, 1), input.Substring(i, 1).ToLower()) != 0)
                        return false;
                }
                else
                {
                    if (string.CompareOrdinal(input.Substring(i, 1), input.Substring(i, 1).ToUpper()) != 0)
                        return false;
                }

                lastIsUpper = !lastIsUpper;
            }

            return true;
        }

        /// <summary>
        /// Checks whether a word or sentence is capitalized.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsCapitalized(string input)
        {
            if (input.Length == 0) return false;
            return string.CompareOrdinal(input.Substring(0, 1), input.Substring(0, 1).ToUpper()) == 0;
        }

        /// <summary>
        /// Checks if a string contains only letters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsLetters(string input)
        {
            char currentLetter;
            for (var i = 0; i < input.Length; i++)
            {
                currentLetter = input[i];

                if (!(Convert.ToInt32(currentLetter) >= 65 && Convert.ToInt32(currentLetter) <= 90) &&
                    !(Convert.ToInt32(currentLetter) >= 97 && Convert.ToInt32(currentLetter) <= 122))
                {
                    //Not a letter
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks whether a string is in all lower case.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsLowerCase(string input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (string.CompareOrdinal(input.Substring(i, 1), input.Substring(i, 1).ToLower()) != 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a string is not null.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotNull(this string input)
        {
            return !IsNull(input);
        }

        /// <summary>
        /// Checks  if a string is not null or not empty.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string input)
        {
            return !IsNullOrEmpty(input);
        }

        /// <summary>
        /// Checks if string has only numbers.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumeric(string input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (!(Convert.ToInt32(input[i]) >= 48 && Convert.ToInt32(input[i]) <= 57))
                {
                    //Not integer value
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks whether a string is in all upper case.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUpperCase(string input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (string.CompareOrdinal(input.Substring(i, 1), input.Substring(i, 1).ToUpper()) != 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Replace the "errorString" with "correctString" within "mainString"
        /// </summary>
        /// <param name="mainString">Main string which which replace is to be done.</param>
        /// <param name="errorString"></param>
        /// <param name="correctString"></param>
        /// <returns></returns>
        public static string ReplaceString(string mainString, string errorString, string correctString)
        {
            return IsNullOrEmpty(mainString)
                ? mainString
                : mainString.Replace(errorString, correctString);
        }

        /// <summary>
        /// Reverses a string
        /// Ex. Hello -> olleH
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Reverse(string input)
        {
            var ret = new StringBuilder();
            for (var i = input.Length - 1; i >= 0; i--)
            {
                ret.Append(input.Substring(i, 1));
            }
            return ret.ToString();
        }

        //Function that works the same way as the default Substring, but
        //it takes Start and End (exclusive) parameters instead of Start and Length
        //hello, 1, 3 -> el
        public static string SubstringEnd(string input, int start, int end)
        {
            if (start > end) //Flip the values
            {
                start ^= end;
                end = start ^ end;
                start ^= end;
            }

            if (end > input.Length) end = input.Length; //avoid errors

            return input.Substring(start, end - start);
        }

        /// <summary>
        /// Convert the given string in title case (e.g.: "title case string => Title Case String")
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string ToTitleCase(string title)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(title);
        }

        private static bool IsNull(string mainString)
        {
            return null == mainString;
        }

        private static bool IsNullOrEmpty(string mainString)
        {
            return string.IsNullOrEmpty(mainString);
        }

        /// <summary>
        /// Converts an string to a Guid. This could be used within a unit test to mock objects.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string value)
        {
            Guid.TryParse(value, out Guid result);

            return result;
        }
    }
}