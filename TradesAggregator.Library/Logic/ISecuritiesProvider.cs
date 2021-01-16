using System.Collections.Generic;
using TradesAggregator.Library.Models.Domain;

namespace TradesAggregator.Library.Logic
{
    /// <summary>
    /// Abstraction for securities data retrieval
    /// </summary>
    public interface ISecuritiesProvider
    {
        List<Security> GetSecurities(string securitiesFilePath);
    }
}
