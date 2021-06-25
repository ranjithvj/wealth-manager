using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WealthManager.Shared.Helpers;

namespace WealthManager.Shared.Helpers
{
    public static class DataTypeFormatter
    {
        public static string FormatDecimalToCurrencyString(decimal value)
        {
            return value.ToString("c0");
        }

        public static string ParseCurrencyStringToValue(string value)
        {
            return Regex.Replace(value, @"\$\s?|(,*)", "");
        }

        public static string FormatDecimalToPercentageString(decimal value)
        {
            return value.ToString() + "%";
        }

        public static string ParsePercentageStringToValue(string value)
        {
            return value.Replace("%", "");
        }
    }
}
