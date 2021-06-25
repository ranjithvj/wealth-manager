using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WealthManager.Shared.Attributes;
using WealthManager.Shared.Helpers;

namespace WealthManager.Shared.ViewModels
{
    public class RecurringDepositVM : BaseVM<int>
    {
        public CultureInfo cultureInfo = new CultureInfo(Constants.Cultures.India);
        public string UserId { get; set; }

        [Required]
        [Range(1, Double.PositiveInfinity,ErrorMessage = "Mandatory")]
        [Display(Name ="Bank")]
        public int BankId { get; set; }

        [Display(Name = "Bank")]
        public string BankName { get; set; }

        [Required]
        [Range(100,Double.PositiveInfinity, ErrorMessage = "Must be greater than 100")]
        [Display(Name = "Monthly Installment")]
        [DisplayFormat(DataFormatString = Constants.StringFormats.Currency0DecimalPlaces)]
        public decimal MonthlyInstallment { get; set; }

        [Display(Name = "Monthly Installment")]
        public string MonthlyInstallmentRef
        {
            get
            {
                return MonthlyInstallment.ToCurrencyString();
            }
            set
            {

            }
        }

        [Required]
        [Range(1, 100, ErrorMessage = "Should be between 1 and 100")]
        [Display(Name = "Rate of Interest %")]
        public decimal RateOfInterest { get; set; }

        [Display(Name = "Rate of Interest %")]
        [DisplayFormat(DataFormatString = "{0:P}")]
        public string RateOfInterestRef 
        { 
            get 
            {
                return RateOfInterest.ToPercentageString();
            }
            set
            {

            }
        }

        [Display(Name = "Compound Frequency")]
        public int CompoundFrequency { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Maturity Date")]
        public DateTime MaturityDate { get; set; }

        [Display(Name = "Duration (Years)")]
        public int Duration_Years { get; set; }

        [Display(Name = "Duration (Months)")]
        public int Duration_Months { get; set; }

        [Required]
        [Range(100, Double.PositiveInfinity, ErrorMessage = "Must be greater than 100")]
        [Display(Name = "Maturity Amount")]
        [DisplayFormat(DataFormatString = Constants.StringFormats.Currency0DecimalPlaces)]
        public decimal MaturityAmount { get; set; }

        [Display(Name = "Maturity Amount")]
        public string MaturityAmountRef
        {
            get
            {
                return MaturityAmount.ToCurrencyString();
            }
            set
            {

            }
        }
    }
}
