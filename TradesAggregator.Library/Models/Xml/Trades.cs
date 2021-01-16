/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TradesAggregator.Library.Models.Xml
{
	[XmlRoot(ElementName = "security")]
	public class SecurityLink
	{
		[XmlElement(ElementName = "id")]
		public string Id { get; set; }
		[XmlElement(ElementName = "code")]
		public string Code { get; set; }
	}

	[XmlRoot(ElementName = "trade")]
	public class Trade
	{
		[XmlElement(ElementName = "tradingAccount")]
		public string TradingAccount { get; set; }
		[XmlElement(ElementName = "tradeDate")]
		public string TradeDate { get; set; }
		[XmlElement(ElementName = "settleDate")]
		public string SettleDate { get; set; }
		[XmlElement(ElementName = "actualDate")]
		public string ActualDate { get; set; }
		[XmlElement(ElementName = "transactionCode")]
		public string TransactionCode { get; set; }
		[XmlElement(ElementName = "quantity")]
		public string Quantity { get; set; }
		[XmlElement(ElementName = "tradeCurrency")]
		public string TradeCurrency { get; set; }
		[XmlElement(ElementName = "settlementCurrency")]
		public string SettlementCurrency { get; set; }
		[XmlElement(ElementName = "price")]
		public string Price { get; set; }
		[XmlElement(ElementName = "execBroker")]
		public string ExecBroker { get; set; }
		[XmlElement(ElementName = "security")]
		public SecurityLink Security { get; set; }
	}

	[XmlRoot(ElementName = "trades")]
	public class Trades
	{
		[XmlElement(ElementName = "trade")]
		public List<Trade> Trade { get; set; }
	}

}