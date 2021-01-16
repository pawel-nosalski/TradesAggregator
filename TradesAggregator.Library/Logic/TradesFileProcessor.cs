using System;
using System.Collections.Generic;
using TradesAggregator.Library.Models.Domain;
using TradesAggregator.Library.Models.Report;

namespace TradesAggregator.Library.Logic
{
    /// <summary>
    /// Calculation engine for whole project. Since we iterate throught trade files rather than load everything to memory and aggregate it via LINQ, there is a complex internal state
    /// It processes trades from single file - that means changing states of internal dictionaries responsible for aggregations calculation.
    /// In the end, it generates report data object
    /// </summary>
    public class TradesFileProcessor : ITradesFileProcessor
    {
        private readonly Dictionary<string, decimal> quantitiesByBloombergId = new Dictionary<string, decimal>();
        private readonly Dictionary<string, decimal> quantitiesByTransactionCode = new Dictionary<string, decimal>();
        private readonly Dictionary<DateTime, decimal> quantitiesByTradeDate = new Dictionary<DateTime, decimal>();

        // for price calculations, we have two dictionaries per one aggregation - one for sum of prices, second for price count
        // price average is obviously first aggregation divided by second one

        private readonly Dictionary<string, decimal> priceSumByBloombergId = new Dictionary<string, decimal>();
        private readonly Dictionary<string, int> priceCountByBloombergId = new Dictionary<string, int>();

        private readonly Dictionary<string, decimal> priceSumByTransactionCode = new Dictionary<string, decimal>();
        private readonly Dictionary<string, int> priceCountByTransactionCode = new Dictionary<string, int>();

        private readonly Dictionary<DateTime, decimal> priceSumByTradeDate = new Dictionary<DateTime, decimal>();
        private readonly Dictionary<DateTime, int> priceCountByTradeDate = new Dictionary<DateTime, int>();

        private readonly Dictionary<string, int> fileCountsDictionary = new Dictionary<string, int>();

        /// <summary>
        /// Initialize internal dictionaries with the list of available securities
        /// </summary>
        /// <param name="securities"></param>
        public void InitializeWithSecuritiesData(List<Security> securities)
        {
            foreach (var security in securities)
            {
                this.quantitiesByBloombergId.Add(security.BloombergId, 0);
                this.priceSumByBloombergId.Add(security.BloombergId, 0);
                this.priceCountByBloombergId.Add(security.BloombergId, 0);
            }
        }

        /// <summary>
        /// Process single trades file by updating the stateof internal dictionaries
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileTrades"></param>
        public void Process(string fileName, List<Trade> fileTrades)
        {
            // process file trades, one by one
            foreach (var trade in fileTrades)
            {
                // only if trade is valid, we process (aggregate) it further
                if (this.IsTradeValid(trade.Security))
                {
                    this.UpdateQuantitiesByBloombergIdDictionary(trade.Security, trade.Quantity);
                    this.UpdateQuantitiesByTransactionCodeDictionary(trade.TransactionCode, trade.Quantity);
                    this.UpdateQuantitiesByTradeDateDictionary(trade.TradeDate, trade.Quantity);

                    this.UpdatePricesByBloombergIdDictionaries(trade.Security, trade.Price);
                    this.UpdatePricesByTransactionCodeDictionaries(trade.TransactionCode, trade.Price);
                    this.UpdatePricesByTradeDateDictionaries(trade.TradeDate, trade.Price);
                }
                else
                {
                    this.UpdateFileCountsDictionary(fileName, trade.Security);
                }
            }
        }

        private bool IsTradeValid(string security)
        {
            // check if trade security is a valid one
            if (string.IsNullOrEmpty(security) || !quantitiesByBloombergId.ContainsKey(security))
            {
                return false;
            }
            return true;
        }

        private void UpdateFileCountsDictionary(string fileName, string tradeSecurity)
        {
            // trade has invalid security, thus we need to update fileCounts
            if (fileCountsDictionary.ContainsKey(fileName))
            {
                fileCountsDictionary[fileName]++;
            }
            else
            {
                fileCountsDictionary.Add(fileName, 1);
            }
        }

        private void UpdateQuantitiesByBloombergIdDictionary(string tradeSecurity, decimal tradeQuantity)
        {
            quantitiesByBloombergId[tradeSecurity] += tradeQuantity;
        }

        private void UpdateQuantitiesByTransactionCodeDictionary(string transactionCode, decimal tradeQuantity)
        {
            if(!string.IsNullOrEmpty(transactionCode))
            {
                if (quantitiesByTransactionCode.ContainsKey(transactionCode))
                {
                    quantitiesByTransactionCode[transactionCode] += tradeQuantity;
                }
                else
                {
                    quantitiesByTransactionCode.Add(transactionCode, tradeQuantity);
                }
            }
        }

        private void UpdateQuantitiesByTradeDateDictionary(DateTime tradeDate, decimal tradeQuantity)
        {               
            if (quantitiesByTradeDate.ContainsKey(tradeDate))
            {
                quantitiesByTradeDate[tradeDate] += tradeQuantity;
            }
            else
            {
                quantitiesByTradeDate.Add(tradeDate, tradeQuantity);
            }
        }

        private void UpdatePricesByBloombergIdDictionaries(string tradeSecurity, decimal tradePrice)
        {
            priceSumByBloombergId[tradeSecurity] += tradePrice;
            priceCountByBloombergId[tradeSecurity]++;
        }

        private void UpdatePricesByTransactionCodeDictionaries(string transactionCode, decimal tradePrice)
        {
            if (!string.IsNullOrEmpty(transactionCode))
            {
                if (priceSumByTransactionCode.ContainsKey(transactionCode))
                {
                    priceSumByTransactionCode[transactionCode] += tradePrice;
                    priceCountByTransactionCode[transactionCode]++;
                }
                else
                {
                    priceSumByTransactionCode.Add(transactionCode, tradePrice);
                    priceCountByTransactionCode.Add(transactionCode, 1);
                }
            }
        }

        private void UpdatePricesByTradeDateDictionaries(DateTime tradeDate, decimal tradePrice)
        {
            if (priceSumByTradeDate.ContainsKey(tradeDate))
            {
                priceSumByTradeDate[tradeDate] += tradePrice;
                priceCountByTradeDate[tradeDate]++;
            }
            else
            {
                priceSumByTradeDate.Add(tradeDate, tradePrice);
                priceCountByTradeDate.Add(tradeDate, 1);
            }
        }

        /// <summary>
        /// Generate report after processing is done, based on internal state
        /// </summary>
        /// <returns></returns>
        public TradesAggregationsReport GenerateReport()
        {
            var report = new TradesAggregationsReport();

            // map securities aggregations
            var securitiesAggregations = new List<SecurityAggregation>();
            foreach(var security in this.quantitiesByBloombergId.Keys)
            {
                securitiesAggregations.Add(new SecurityAggregation
                {
                    Security = security,
                    QuantitySum = this.quantitiesByBloombergId[security],
                    PriceAverage = this.priceSumByBloombergId[security] / this.priceCountByBloombergId[security]
                }); 
            }

            report.SecuritiesAggregations = securitiesAggregations;


            // map transaction code aggregations
            var transactionCodeAggregations = new List<TransactionCodeAggregation>();
            foreach (var transactionCode in this.quantitiesByTransactionCode.Keys)
            {
                transactionCodeAggregations.Add(new TransactionCodeAggregation
                {
                    TransactionCode = transactionCode,
                    QuantitySum = this.quantitiesByTransactionCode[transactionCode],
                    PriceAverage = this.priceSumByTransactionCode[transactionCode] / this.priceCountByTransactionCode[transactionCode]
                });
            }

            report.TransactionCodeAggregations = transactionCodeAggregations;


            // map trade date aggregations
            var tradeDateAggregations = new List<TradeDateAggregation>();
            foreach (var tradeDate in this.quantitiesByTradeDate.Keys)
            {
                tradeDateAggregations.Add(new TradeDateAggregation
                {
                    TradeDate = tradeDate,
                    QuantitySum = this.quantitiesByTradeDate[tradeDate],
                    PriceAverage = this.priceSumByTradeDate[tradeDate] / this.priceCountByTradeDate[tradeDate]
                });
            }

            report.TradeDateAggregations = tradeDateAggregations;


            // map file aggregations
            var fileAggregations = new List<FileAggregation>();
            foreach (var fileName in this.fileCountsDictionary.Keys)
            {
                fileAggregations.Add(new FileAggregation
                {
                    FilePath = fileName,
                    InvalidTradesCount = this.fileCountsDictionary[fileName]
                });
            }

            report.FileAggregations = fileAggregations;

            return report;
        }
    }
}
