using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Client.Helpers
{
    public static class Constants
    {
        public static class RecurringDepositApi
        {
            public const string GetAll = "api/RecurringDeposit";
            public const string Get = "api/RecurringDeposit/{0}";
            public const string Create = "api/RecurringDeposit";
            public const string Update = "api/RecurringDeposit/{0}";
            public const string Delete = "api/RecurringDeposit/{0}";
        }

        public static class FixedDepositApi
        {
            public const string GetAll = "api/FixedDeposit";
            public const string Get = "api/FixedDeposit/{0}";
            public const string Create = "api/FixedDeposit";
            public const string Update = "api/FixedDeposit/{0}";
            public const string Delete = "api/FixedDeposit/{0}";
        }

        public static class MutualFundApi
        {
            public const string GetAll = "api/MutualFund";
            public const string Get = "api/MutualFund/{0}";
            public const string Create = "api/MutualFund";
        }


        public static class BankApi
        {
            public const string GetAll = "api/Bank";
        }

        public static class FixedDepositTypeApi
        {
            public const string GetAll = "api/FixedDepositType";
        }

        public static class Cultures
        {
            public const string India = "en-IN";
        }
    }
}
