using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Server.Models
{
    public class MutualFundTxn : BaseEntity<int>
    {
        [Required]
        [ForeignKey("MutualFund")]
        public int MutualFundId { get; set; }
        public MutualFund MutualFund { get; set; }

        [Required]
        public DateTime TxnDate { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public decimal? Units { get; set; }
        public decimal? Nav { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public string Dividend_rate { get; set; }
    }
}
