using System;
using System.Globalization;
using TradesAggregator.Library.Models.Domain;

namespace TradesAggregator.Library.Logic.Mappers
{
    /// <summary>
    /// Maps Trade from polluted xml model to domain one
    /// </summary>
    /// <param name="securityInput"></param>
    /// <returns></returns>
    public class TradesMapper : IMapper<Models.Xml.Trade, Trade>
    {
        public Trade Map(Models.Xml.Trade input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var trade = new Trade
            {
                TradeDate = DateTime.ParseExact(input.TradeDate, "yyyyMMdd", CultureInfo.InvariantCulture),
                TransactionCode = input.TransactionCode.ToLowerInvariant(),
                Price = decimal.Parse(input.Price, CultureInfo.InvariantCulture),
                Quantity = decimal.Parse(input.Quantity, CultureInfo.InvariantCulture)
            };

            if(input.Security != null && input.Security.Code != null)
            {
                trade.Security = input.Security.Code;
            }

            return trade;
        }
    }
}
