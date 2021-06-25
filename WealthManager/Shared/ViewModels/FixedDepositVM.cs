using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WealthManager.Shared.Helpers;

namespace WealthManager.Shared.ViewModels
{
    public class FixedDepositVM : BaseVM<int>
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Bank")]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "Mandatory")]
        public int BankId { get; set; }

        [Display(Name = "Bank")]
        public string BankName { get; set; }

        [Display(Name = "Type")]
        [Required]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "Mandatory")]
        public int FixedDepositTypeId { get; set; }

        [Display(Name = "Type")]
        public string FixedDepositTypeName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = Constants.StringFormats.Currency0DecimalPlaces)]
        [Range(1000, Double.PositiveInfinity, ErrorMessage = "Must be greater than 1000")]
        public decimal Principal { get; set; }
        
        [Display(Name = "Principal")]
        public string PrincipalRef
        {
            get
            {
                return Principal.ToCurrencyString();
            }
            set
            {

            }
        }

        [Required]
        [Range(1, 100, ErrorMessage = "Should be between 1 and 100")]
        [Display(Name = "Rate of Interest")]
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
        [Display(Name = "Maturity Amount")]
        [Range(1000, Double.PositiveInfinity, ErrorMessage = "Must be greater than 1000")]
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
