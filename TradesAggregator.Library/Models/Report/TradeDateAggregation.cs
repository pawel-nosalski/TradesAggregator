using System;

namespace TradesAggregator.Library.Models.Report
{
    /// <summary>
    /// Represents trade date aggregations report entry
    /// </summary>
    public class TradeDateAggregation
    {
        public DateTime TradeDate { get; set; }
        public decimal QuantitySum { get; set; }
        public decimal PriceAverage { get; set; }
    }
}
