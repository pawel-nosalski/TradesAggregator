using System.Collections.Generic;
using TradesAggregator.Library.Models.Domain;
using TradesAggregator.Library.Models.Report;

namespace TradesAggregator.Library.Logic
{
    /// <summary>
    /// Abstraction for stateful processing of trades and final report 
    /// </summary>
    public interface ITradesFileProcessor
    {
        void Process(string fileName, List<Trade> fileTrades);
        void InitializeWithSecuritiesData(List<Security> securities);
        TradesAggregationsReport GenerateReport();
    }
}
