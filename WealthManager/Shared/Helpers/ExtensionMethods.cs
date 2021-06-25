using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthManager.Shared.Helpers
{
    public static class ExtensionMethods
    {
        #region DateTime

        public static DateTime UtcToIst(this DateTime utc)
        {
            var localTime = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utc, localTime);
        }

        #endregion

        #region Decimal
        public static string ToCurrencyString(this decimal value)
        {
            return value.ToString("c0");
        }

        public static string ToPercentageString(this decimal value)
        {
            return value.ToString() + "%";
        }

        #endregion

        #region Double
        public static string ToCurrencyString(this double value)
        {
            return value.ToString("c0");
        }

        public static string ToPercentageString(this double value)
        {
            return value.ToString() + "%";
        }

        #endregion

        #region Int

        public static string ToCurrencyString(this int value)
        {
            return value.ToString("c0");
        }

        public static string ToPercentageString(this int value)
        {
            return value.ToString() + "%";
        }

        #endregion
    }
}
