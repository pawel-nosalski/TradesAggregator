using System.Collections.Generic;
using System.IO;
using TradesAggregator.Library.Models.Report;

namespace TradesAggregator.Library.Logic.IO
{
    /// <summary>
    // Writes report data to txt file in human-readable way
    /// </summary>
    public class ReportTxtFileWriter : IReportWriter
    {
        public void Write(string reportFilePath, TradesAggregationsReport reportData)
        {
            var lines = new List<string>();

            lines.Add("----------------------------------- Securities ------------------------------------------------------");
            lines.Add(string.Empty);
            foreach (var security in reportData.SecuritiesAggregations)
            {
                lines.Add($"{security.Security} - Quantity Sum: {security.QuantitySum}; Price Average: {security.PriceAverage}");
            }

            lines.Add(string.Empty);
            lines.Add("----------------------------------- Transaction Codes -----------------------------------------------");
            foreach (var transactionCodes in reportData.TransactionCodeAggregations)
            {
                lines.Add($"{transactionCodes.TransactionCode} - Quantity Sum: {transactionCodes.QuantitySum}; Price Average: {transactionCodes.PriceAverage}");
            }

            lines.Add(string.Empty);
            lines.Add("----------------------------------- Trade Dates ------------------------------------------------------");
            foreach (var tradeDate in reportData.TradeDateAggregations)
            {
                lines.Add($"{tradeDate.TradeDate.ToString("yyyy/MM/dd")} - Quantity Sum: {tradeDate.QuantitySum}; Price Average: {tradeDate.PriceAverage}");
            }

            lines.Add(string.Empty);
            lines.Add("------------------------------------ Files with without valid security ------------------------------");
            foreach (var file in reportData.FileAggregations)
            {
                lines.Add($"{file.FilePath} - Count: {file.InvalidTradesCount}");
            }

            File.WriteAllLines(reportFilePath, lines);
        }
    }
}
