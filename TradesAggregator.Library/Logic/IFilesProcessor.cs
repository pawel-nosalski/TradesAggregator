using System.Collections.Generic;
using TradesAggregator.Library.Models.Domain;
using TradesAggregator.Library.Models.Report;

namespace TradesAggregator.Library.Logic
{
    /// <summary>
    /// Abstraction for trade files processing and calculations orchestration
    /// </summary>
    public interface IFilesProcessor
    {
        TradesAggregationsReport Process(string rootDataFolderPath, List<Security> securities);
    }
}
