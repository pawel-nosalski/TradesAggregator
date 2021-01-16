using System.Collections.Generic;

namespace TradesAggregator.Library.Models.Report
{
    /// <summary>
    /// Report data root class
    /// </summary>
    public class TradesAggregationsReport
    {
        public List<SecurityAggregation> SecuritiesAggregations { get; set; }
        public List<TransactionCodeAggregation> TransactionCodeAggregations { get; set; }
        public List<TradeDateAggregation> TradeDateAggregations { get; set; }
        public List<FileAggregation> FileAggregations { get; set; }

        public TradesAggregationsReport()
        {
            this.SecuritiesAggregations = new List<SecurityAggregation>();
            this.TransactionCodeAggregations = new List<TransactionCodeAggregation>();
            this.TradeDateAggregations = new List<TradeDateAggregation>();
            this.FileAggregations = new List<FileAggregation>();
        }
    }
}
