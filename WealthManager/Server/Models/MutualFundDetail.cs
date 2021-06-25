using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Server.Models
{
    public class MutualFundDetail : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Isin { get; set; }
        public string AmfiCode { get; set; }
        public string Rta { get; set; }
        public string RtaCode { get; set; }
        public string AmcCode { get; set; }
    }
}
