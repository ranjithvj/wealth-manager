using Klear.Financial.Lib;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WealthManager.Unittest
{
    public class XIRRLibraryTests
    {
        [Fact]
        public void OneYearDeposit()
        {
            var cashFlows = new List<CashFlowDates>()
            {
                new CashFlowDates(-1000, new DateTime(2017, 1, 1)),
                new CashFlowDates(1010, new DateTime(2018, 1, 1))
            };
            var result = CalculationWrapper.XIRR(cashFlows);
            Assert.Equal(0.01, result);
        }

        [Fact]
        public void OneYearDepositWithWithdrawal()
        {
            var cashFlows = new List<CashFlowDates>()
            {
                new CashFlowDates(-1000, new DateTime(2017, 1, 1)),
                new CashFlowDates(500, new DateTime(2017, 7, 1)),
                new CashFlowDates(507.5, new DateTime(2018, 1, 1))
            };
            var result = CalculationWrapper.XIRR(cashFlows, 6);
            Assert.Equal(0.010019, result);
        }

        [Fact]
        public void StockInvestment()
        {
            var cashFlows = new List<CashFlowDates>()
            {
                new CashFlowDates(-500, new DateTime(2017, 1, 1)),
                new CashFlowDates(-500, new DateTime(2017, 2, 1)),
                new CashFlowDates(-500, new DateTime(2017, 3, 1)),
                new CashFlowDates(-500, new DateTime(2017, 4, 1)),
                new CashFlowDates(-500, new DateTime(2017, 5, 1)),
                new CashFlowDates(-500, new DateTime(2017, 6, 1)),
                new CashFlowDates(-500, new DateTime(2017, 7, 1)),
                new CashFlowDates(-500, new DateTime(2017, 8, 1)),
                new CashFlowDates(-500, new DateTime(2017, 9, 1)),
                new CashFlowDates(-500, new DateTime(2017, 10, 1)),
                new CashFlowDates(-500, new DateTime(2017, 11, 1)),
                new CashFlowDates(-500, new DateTime(2017, 12, 1)),
                new CashFlowDates(6545.08, new DateTime(2018, 1, 1))
            };
            var result = CalculationWrapper.XIRR(cashFlows, 6);
            Assert.Equal(0.171156, result);
        }
        [Fact]
        //[ExpectedException(typeof(IncosistentCashFlowException))
        public void NoPositiveCashFlows()
        {
            var cashFlows = new List<CashFlowDates>()
            {
                new CashFlowDates(-1000, new DateTime(2017, 1, 1))
            };
            //var result = CalculationWrapper.XIRR(cashFlows, 6);
            Assert.Throws<IncosistentCashFlowException>(() => CalculationWrapper.XIRR(cashFlows, 6));
        }

        [Fact]
        //[ExpectedException(typeof(IncosistentCashFlowException))]
        public void NoNegativeCashFlows()
        {
            var cashFlows = new List<CashFlowDates>()
            {
                new CashFlowDates(1000, new DateTime(2017, 1, 1))
            };
            //var result = CalculationWrapper.XIRR(cashFlows, 6);

            Assert.Throws<IncosistentCashFlowException>(() => CalculationWrapper.XIRR(cashFlows, 6));
        }
    }
}
