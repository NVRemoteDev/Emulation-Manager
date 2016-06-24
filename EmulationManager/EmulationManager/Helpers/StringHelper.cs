using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationManager.Helpers
{
    public static class StringHelper
    {
        public static string RemoveWhitespace(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            return new string(input.Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }

        public static string RemoveLineBreaks(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            input = input.Replace(Environment.NewLine, "");
            input = input.Replace("\n", "");
            input = input.Replace("\r", "");

            return input;
        }

        public static string CleanXmlValues(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            input = RemoveLineBreaks(input);

            while(input.Contains("; "))
            {
                input = input.Replace("; ", ";");
            }
            while (input.Contains(" ;"))
            {
                input = input.Replace(" ;", ";");
            }
            return input.Trim();
        }
    }
}
