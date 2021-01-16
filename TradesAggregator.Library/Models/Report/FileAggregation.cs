namespace TradesAggregator.Library.Models.Report
{
    /// <summary>
    /// Represents invalid trades per file report entry
    /// </summary>
    public class FileAggregation
    {
        public string FilePath { get; set; }
        public int InvalidTradesCount { get; set; }
    }
}
