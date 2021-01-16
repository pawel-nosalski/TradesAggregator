using TradesAggregator.Library.Models.Report;

namespace TradesAggregator.Library.Logic.IO
{
    /// <summary>
    /// Abstraction for report persistence on disk
    /// </summary>
    public interface IReportWriter
    {
        void Write(string reportFilePath, TradesAggregationsReport reportData);
    }
}
