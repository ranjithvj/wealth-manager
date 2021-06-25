using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Server.Models
{
    public class Bank : BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<RecurringDeposit> RecurringDeposits { get; set; }
        public ICollection<FixedDeposit> FixedDeposits { get; set; }
    }
}
