namespace TradesAggregator.Library.Models.Report
{
    /// <summary>
    /// Represents transaction code aggregations report entry
    /// </summary>
    public class TransactionCodeAggregation
    {
        public string TransactionCode { get; set; }
        public decimal QuantitySum { get; set; }
        public decimal PriceAverage { get; set; }
    }
}
