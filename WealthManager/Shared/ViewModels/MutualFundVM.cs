using Klear.Financial.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WealthManager.Shared.Helpers;

namespace WealthManager.Shared.ViewModels
{

    public class MutualFundVM : BaseVM<int>
    {
        public string UserId { get; set; }
        public string Isin { get; set; }
        public string Name { get; set; }

        [Display(Name = "Units")]
        public decimal UnitsHeld { get; set; }
        public decimal InvestedAmount { get; set; }

        [Display(Name = "Current NAV")]
        [DisplayFormat(DataFormatString = Constants.StringFormats.Currency2DecimalPlaces)]
        public decimal CurrentNav { get; set; }
        public DateTime CurrentNavPriceDate { get; set; }
        public decimal AnnualReturn { get; set; }
        public List<MutualFundTxnVM> MutualFundTxns { get; set; }

        #region Calculated fields

        [DataType(DataType.Date)]
        [Display(Name = "Latest NAV Date")]
        public string LatestNavDate
        {
            get
            {
                return CurrentNavPriceDate.ToShortDateString();
            }
        }

        [Display(Name = "Investment")]
        [DisplayFormat(DataFormatString = Constants.StringFormats.Currency0DecimalPlaces)]
        public string InvestedAmountRef
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling(InvestedAmount)).ToCurrencyString();
            }
            set
            {

            }
        }


        [Display(Name = "Value")]
        [DisplayFormat(DataFormatString = Constants.StringFormats.Currency0DecimalPlaces)]
        public decimal CurrentValue
        {
            get
            {
                return UnitsHeld * CurrentNav;
            }
        }

        [Display(Name = "Current Value")]
        public string CurrentValueRef
        {
            get
            {
                return CurrentValue.ToCurrencyString();
            }
            set
            {

            }
        }

        [Display(Name = "P&L")]
        [DisplayFormat(DataFormatString = Constants.StringFormats.Currency0DecimalPlaces)]
        public decimal UnrealizedGain
        {
            get
            {
                return CurrentValue - InvestedAmount;
            }
        }

        [Display(Name = "P&L")]
        public string UnrealizedGainRef
        {
            get
            {
                return UnrealizedGain.ToCurrencyString();
            }
            set
            {

            }
        }

        [Display(Name = "Avg. Buying NAV")]
        [DisplayFormat(DataFormatString = Constants.StringFormats.Currency2DecimalPlaces)]
        public decimal AvgBuyingNav
        {
            get
            {
                try
                {
                    return Math.Round(InvestedAmount / UnitsHeld, 2);
                }
                catch(DivideByZeroException e)
                {
                    return 0;
                }
            }
        }

        public string AvgBuyingNavRef
        {
            get
            {
                return AvgBuyingNav.ToCurrencyString();
            }
        }


        [Display(Name = "XIRR")]
        [DisplayFormat(DataFormatString = "{0:P}")]
        public double XIRR //Double because the package returns a double
        {
            get
            {
                if(MutualFundTxns == null || MutualFundTxns.Count == 0)
                {
                    return 0;
                }

                var cashFlows = new List<CashFlowDates>();

                foreach (var txn in MutualFundTxns)
                {
                    cashFlows.Add(new CashFlowDates((double)txn.Amount, txn.TxnDate));
                }
                //We need to be redeeming it today in order to calculate the XIRR
                cashFlows.Add(new CashFlowDates(-(double)CurrentValue, DateTime.UtcNow));

                return CalculationWrapper.XIRR(cashFlows, 6);
            }
        }

        [Display(Name = "XIRR")]
        public string XIRRRef
        {
            get
            {
                return (XIRR*100).ToPercentageString();
            }
            set
            {

            }
        }

        [Display(Name = "Current NAV")]
        public string CurrentNavRef
        {
            get
            {
                return this.CurrentNav.ToCurrencyString();
            }
            set
            {

            }
        }
        #endregion
    }
}
