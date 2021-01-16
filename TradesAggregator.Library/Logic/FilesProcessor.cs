using System;
using System.Collections.Generic;
using TradesAggregator.Library.Logic.IO;
using TradesAggregator.Library.Logic.Mappers;
using TradesAggregator.Library.Models.Domain;
using TradesAggregator.Library.Models.Report;

namespace TradesAggregator.Library.Logic
{
    /// <summary>
    /// Processes trade files, one by one. This is done to optimize memory usage, since loading all the files to memory and perform calculations on full data set 
    /// would be convenient, but not applicable for large files
    /// </summary>
    public class FilesProcessor : IFilesProcessor
    {
        private readonly ITradeFilesScanner tradeFilesScanner;
        private readonly lFileReader tradesFileReader;
        private readonly ITradesFileProcessor fileTradesProcessor;
        private readonly IMapper<Models.Xml.Trade, Trade> tradesMapper;

        public FilesProcessor(ITradeFilesScanner tradeFilesScanner, lFileReader tradesFileReader, ITradesFileProcessor fileTradesProcessor, IMapper<Models.Xml.Trade, Trade> tradesMapper)
        {
            if(tradeFilesScanner == null)
            {
                throw new ArgumentNullException(nameof(tradeFilesScanner));
            }

            if(tradesFileReader == null)
            {
                throw new ArgumentNullException(nameof(tradesFileReader));
            }

            if(fileTradesProcessor == null)
            {
                throw new ArgumentNullException(nameof(fileTradesProcessor));
            }

            if (tradesMapper == null)
            {
                throw new ArgumentNullException(nameof(tradesMapper));
            }

            this.tradeFilesScanner = tradeFilesScanner;
            this.tradesFileReader = tradesFileReader;
            this.fileTradesProcessor = fileTradesProcessor;
            this.tradesMapper = tradesMapper;
        }


        public TradesAggregationsReport Process(string rootDataFolderPath, List<Security> securities)
        {
            if(securities == null)
            {
                throw new ArgumentNullException(nameof(securities));
            }

            if(string.IsNullOrEmpty(rootDataFolderPath))
            {
                throw new ArgumentException($"{nameof(rootDataFolderPath)} is either null or empty !");
            }

            this.fileTradesProcessor.InitializeWithSecuritiesData(securities);

            // go through trade files, one by one
            foreach (var file in tradeFilesScanner.Scan(rootDataFolderPath, "Trades*.xml"))
            {
                // first, get the raw file data
                var tradeFileObject = this.tradesFileReader.ReadFile<Models.Xml.Trades>(file);

                // if we aren't able to read trades file, just ignore it
                if (tradeFileObject != null)
                {
                    // map trades to the domain model (with only necessary data required for the report)
                    var fileTrades = new List<Trade>();
                    foreach (var fileTrade in tradeFileObject.Trade)
                    {
                        var trade = this.tradesMapper.Map(fileTrade);
                        fileTrades.Add(trade);
                    }

                    // process given trades from single file
                    this.fileTradesProcessor.Process(file, fileTrades);
                }             
            }

            // finally, when all the files have been processed, we are able to generate aggregations report
            return this.fileTradesProcessor.GenerateReport();
        }
    }
}
