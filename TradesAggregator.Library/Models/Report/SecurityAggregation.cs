namespace TradesAggregator.Library.Models.Report
{
    /// <summary>
    /// Represents security aggregations report entry
    /// </summary>
    public class SecurityAggregation
    {
        public string Security { get; set; }
        public decimal QuantitySum { get; set; }
        public decimal PriceAverage { get; set; }
    }
}
