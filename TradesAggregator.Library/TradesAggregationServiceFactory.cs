using TradesAggregator.Library.Logic;
using TradesAggregator.Library.Logic.IO;
using TradesAggregator.Library.Logic.Mappers;

namespace TradesAggregator.Library
{
    public class TradesAggregationServiceFactory
    {
        /// <summary>
        /// DI framework simulator. Creates abstractions implementation starting from aggregation root. Everything remains at single place
        /// </summary>
        /// <returns></returns>
        public static TradesAggregationService Create()
        {
            var xmlFileReader = new XmlFileReader();

            return new TradesAggregationService(
                        new SecuritiesProvider(xmlFileReader, new SecuritiesMapper()),
                        new FilesProcessor(new TradesFileScanner(), xmlFileReader, new TradesFileProcessor(), new TradesMapper()),
                        new ReportTxtFileWriter()                        
                );
        }
    }
}
