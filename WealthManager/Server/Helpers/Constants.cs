using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Server.Helpers
{
    public static class Constants
    {
        public static class CasReaderApi
        {
            public const string ParsePdfResource = "/parsecasfile?fileLocation={0}&filePassword={1}";
            public const string TaxTxnType = "STAMP_DUTY_TAX";
        }

        public static class AmfiNavReaderApi
        {
            public const string GetByIsinResource = "/getbyisin?isin={0}";
        }

        public static class Cultures
        {
            public const string India = "en-IN";
        }
    }
}
