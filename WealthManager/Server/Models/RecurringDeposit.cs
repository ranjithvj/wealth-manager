using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Server.Models
{
    public class RecurringDeposit : BaseEntity<int>
    {
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey("Bank")]
        public int BankId { get; set; }
        public Bank Bank { get; set; }

        [Required]
        public decimal MonthlyInstallment { get; set; }
        public decimal RateOfInterest { get; set; }
        public int CompoundFrequency { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime MaturityDate { get; set; }
        public int Duration_Years { get; set; }
        public int Duration_Months { get; set; }
        [Required]
        public decimal MaturityAmount { get; set; }

    }
}
