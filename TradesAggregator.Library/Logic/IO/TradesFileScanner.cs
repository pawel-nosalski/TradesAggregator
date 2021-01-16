using System;
using System.Collections.Generic;
using System.IO;

namespace TradesAggregator.Library.Logic.IO
{
    /// <summary>
    /// Scans for trade files on disk and returns their paths sequentially
    /// </summary>
    public class TradesFileScanner : ITradeFilesScanner
    {
        public IEnumerable<string> Scan(string tradesFolderRootPath, string tradeFilesMask)
        {
            if(string.IsNullOrEmpty(tradesFolderRootPath))
            {
                throw new ArgumentException($"{nameof(tradesFolderRootPath)} is either null or empty !");
            }

            if (string.IsNullOrEmpty(tradeFilesMask))
            {
                throw new ArgumentException($"{nameof(tradeFilesMask)} is either null or empty !");
            }

            if (!Directory.Exists(tradesFolderRootPath))
            {
                throw new InvalidOperationException($"Directory under the path {tradesFolderRootPath} does not exist");
            }

            string[] files = Directory.GetFiles(tradesFolderRootPath, tradeFilesMask, SearchOption.AllDirectories);

            foreach (string file in files)
            {
                yield return file;
            }
        }
    }
}
