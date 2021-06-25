using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WealthManager.Server.DTO
{
    public class CasResponse
    {
        [JsonProperty("cas_type")]
        public string CasType { get; set; }
        [JsonProperty("file_type")]
        public string FileType { get; set; }
        [JsonProperty("statement_period")]
        public Statementperiod StatementPeriod { get; set; }
        [JsonProperty("folios")]
        public List<Myfolio> Folios { get; set; }
        [JsonProperty("investor_info")]
        public Investorinfo InvestorInfo { get; set; }

    }
    public class Myfolio
    {
        [JsonProperty("folio")]
        public string Folio { get; set; }
        [JsonProperty("amc")]
        public string Amc { get; set; }
        [JsonProperty("PAN")]
        public string PAN { get; set; }
        [JsonProperty("KYC")]
        public string KYC { get; set; }
        [JsonProperty("PANKYC")]
        public string PANKYC { get; set; }
        [JsonProperty("schemes")]
        public List<Scheme> Schemes { get; set; }
    }

    public class Scheme
    {
        [JsonProperty("scheme")]
        public string SchemeName { get; set; }
        [JsonProperty("advisor")]
        public string Advisor { get; set; }
        [JsonProperty("rta_code")]
        public string RtaCode { get; set; }
        [JsonProperty("rta")]
        public string Rta { get; set; }
        [JsonProperty("isin")]
        public string Isin { get; set; }
        [JsonProperty("amfi")]
        public string Amfi { get; set; }
        [JsonProperty("open")]
        public decimal Open { get; set; }
        [JsonProperty("close")]
        public decimal Close { get; set; }
        [JsonProperty("close_calculated")]
        public decimal CloseCalculated { get; set; }
        [JsonProperty("valuation")]
        public Valuation Valuation { get; set; }
        [JsonProperty("transactions")]
        public List<Transaction> Transactions { get; set; }
    }

    public class Valuation
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("value")]
        public decimal Value { get; set; }
        [JsonProperty("nav")]
        public decimal Nav { get; set; }
    }

    public class Transaction
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("units")]
        public decimal? Units { get; set; }
        [JsonProperty("nav")]
        public decimal? Nav { get; set; }
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("dividend_rate")]
        public string DividendRate { get; set; }
    }
    public class Statementperiod
    {
        [JsonProperty("from")]
        public DateTime From { get; set; }
        [JsonProperty("to")]
        public DateTime To { get; set; }
    }
    public class Investorinfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
    }
}
