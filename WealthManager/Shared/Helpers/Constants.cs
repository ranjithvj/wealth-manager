using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Shared.Helpers
{
    public static class Constants
    {
        public static class Cultures
        {
            public const string India = "en-IN";
        }

        public static class StringFormats
        {
            public const string Currency0DecimalPlaces = "{0:C0}";
            public const string Currency2DecimalPlaces = "{0:C2}";
        }
    }
}
