using System.Text;
using System.Text.RegularExpressions;
using MarkdownSharp;
using Nancy;

namespace Rosanna
{
    public static class ExtensionMethods
    {
        public static string ToSlug(this string text)
        {
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);

            string value = Regex.Replace(Regex.Replace(Encoding.ASCII.GetString(bytes), @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(), @"\s+", "-");

            return value.ToLowerInvariant();
        }

        public static string TransformMarkdown(this string text)
        {
            return new Markdown().Transform(text);
        }

        public static string Squeeze(this string s, string replacement)
        {
            string pattern = Regex.Escape(replacement) + "{2,}";
            return Regex.Replace(s, pattern, replacement);
        }

        public static DynamicDictionary ToDynamicDictionary(this string text, char delimiter = ':')
        {
            var dictionary = new DynamicDictionary();
            string[] lines = text.Split('\n');

            foreach (string line in lines)
            {
                string[] keyValuePair = line.Split(delimiter);
                if (keyValuePair.Length == 2)
                {
                    dictionary[keyValuePair[0].Trim()] = keyValuePair[1].Trim();
                }
            }

            return dictionary;
        }

        public static string ToOrdinal(this int number)
        {
            switch (number%100)
            {
                case 11:
                case 12:
                case 13:
                    return number + "th";
            }

            switch (number%10)
            {
                case 1:
                    return number + "st";
                case 2:
                    return number + "nd";
                case 3:
                    return number + "rd";
                default:
                    return number + "th";
            }
        }
    }
}