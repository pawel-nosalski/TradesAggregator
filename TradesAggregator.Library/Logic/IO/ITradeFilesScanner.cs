using System.Collections.Generic;

namespace TradesAggregator.Library.Logic.IO
{
    /// <summary>
    /// Abstraction for providing trade files paths sequentially
    /// </summary>
    public interface ITradeFilesScanner
    {
        IEnumerable<string> Scan(string tradesFolderRootPath, string tradeFilesMask);
    }
}
