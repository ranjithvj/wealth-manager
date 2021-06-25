using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Shared.ViewModels
{
    public class MutualFundTxnVM
    {
        public int Id { get; set; }
        public int MutualFundId { get; set; }
        public DateTime TxnDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal? Units { get; set; }
        public decimal? Nav { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public string Dividend_rate { get; set; }
        public DateTime LastUpdatedOnUtc { get; set; }
        public string LastUpdatedBy { get; set; }



    }
}
