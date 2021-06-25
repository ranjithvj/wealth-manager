using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Server.Models
{
    public class MutualFund : BaseEntity<int>
    {
        [Required]
        [ForeignKey("MutualFundDetail")]
        public int MutualFundDetailId { get; set; }
        public MutualFundDetail MutualFundDetail { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string FolioNo { get; set; }
        public decimal UnitsHeld { get; set; }
        public decimal InvestedAmount { get; set; }
        public decimal AvgBuyingNav { get; set; }
        public virtual ICollection<MutualFundTxn> MutualFundTxns { get; set; }
    }
}
