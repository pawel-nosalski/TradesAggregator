using System;
using System.IO;
using System.Linq;
using TradesAggregator.Library.Logic;
using TradesAggregator.Library.Logic.IO;

namespace TradesAggregator.Library
{
    /// <summary>
    /// Root class for aggregations report 
    /// </summary>
    public class TradesAggregationService
    {
        private const string SecuritiesFileName = "Securities.xml";
        private const string TradesRootFolder = "Test";

        private readonly ISecuritiesProvider securitiesReader;
        private readonly IFilesProcessor tradesProcessor;
        private readonly IReportWriter reportWriter;

        public TradesAggregationService(ISecuritiesProvider securitiesReader, IFilesProcessor tradesProcessor, IReportWriter reportWriter)
        {
            if (securitiesReader == null)
            {
                throw new ArgumentNullException(nameof(securitiesReader));
            }

            if (tradesProcessor == null)
            {
                throw new ArgumentNullException(nameof(tradesProcessor));
            }

            if (reportWriter == null)
            {
                throw new ArgumentNullException(nameof(reportWriter));
            }

            this.securitiesReader = securitiesReader;
            this.tradesProcessor = tradesProcessor;
            this.reportWriter = reportWriter;
        }

        public void Run(string rootDataFolderPath)
        {
            if (string.IsNullOrEmpty(rootDataFolderPath))
            {
                throw new ArgumentException($"{nameof(rootDataFolderPath)} is either null or empty !");
            }

            // load securities data
            var securitiesFilePath = Path.Combine(rootDataFolderPath, SecuritiesFileName);
            var securities = this.securitiesReader.GetSecurities(securitiesFilePath);

            // we won't be able to progress further if we don't have securities data
            if (securities == null || !securities.Any())
            {
                throw new InvalidOperationException("Securities data was not loaded, shutting down!");
            }

            // run trade files processing
            var tradesFilePath = Path.Combine(rootDataFolderPath, TradesRootFolder);
            var report = this.tradesProcessor.Process(tradesFilePath, securities);

            // write report data to disk
            var reportFilePath = Path.Combine(rootDataFolderPath, $"report_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm")}.txt");
            this.reportWriter.Write(reportFilePath, report);           
        }
    }
}
