using System;
using System.Collections.Generic;
using WealthManager.Shared.ViewModels;
using Xunit;

namespace WealthManager.Unittest
{
    public class MutualFundTests
    {
        [Theory]
        [InlineData(25.2361, 65, 1640.3465)] //Positive 
        [InlineData(0, 65, 0)] //0
        [InlineData(-2.5, 65, -162.5)] //Negative
        public void Test_1_CurrentValueCalculation(decimal nav, decimal units, decimal currentval)
        {
            var mf = new MutualFundVM();
            mf.CurrentNav = nav;
            mf.UnitsHeld = units;

            //Assert
            Assert.Equal(mf.CurrentValue, currentval);
        }

        [Theory]
        [InlineData(25.2361, 65, 1350, 290.3465)] // Positive
        [InlineData(25.2361, 65, 1640.3465, 0)] //0
        [InlineData(25.2361, 65, 1740.3465, -100)] //Negative
        public void Test_2_UnrealizedGainCalculation(decimal nav, decimal units, decimal investedAmt, decimal unGain)
        {
            var mf = new MutualFundVM();
            mf.CurrentNav = nav;
            mf.UnitsHeld = units;
            mf.InvestedAmount = investedAmt;

            //Assert
            Assert.Equal(mf.UnrealizedGain, unGain);
        }

        [Fact]
        public void Test_3_XIRRCalculation()
        {
            var mf = new MutualFundVM();
            mf.MutualFundTxns = new List<MutualFundTxnVM>()
            {
                new MutualFundTxnVM(){ Amount = -1000, TxnDate = new DateTime(2017, 1, 1)},
                new MutualFundTxnVM(){ Amount = 1010, TxnDate = new DateTime(2018, 1, 1)},
            };
            Assert.Equal(0.01, mf.XIRR);
        }
    }
}
