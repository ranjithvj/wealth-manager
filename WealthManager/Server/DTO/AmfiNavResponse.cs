using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Server.DTO
{
    public class AmfiNavResponse
    {
        [JsonProperty("SchemeCode")]
        public string SchemeCode { get; set; }

        [JsonProperty("ISIN Div Payout/ ISIN Growth")]
        public string Isin { get; set; }

        [JsonProperty("Scheme Name")]
        public string SchemeName { get; set; }

        [JsonProperty("Net Asset Value")]
        public decimal Nav { get; set; }

        [JsonProperty("Date")]
        public DateTime PriceDate { get; set; }
    }
}
