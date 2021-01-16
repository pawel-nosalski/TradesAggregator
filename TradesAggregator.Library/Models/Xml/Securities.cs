/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TradesAggregator.Library.Models.Xml
{
	[XmlRoot(ElementName = "HistoricalPrice")]
	public class HistoricalPrice
	{
		[XmlElement(ElementName = "Bid")]
		public string Bid { get; set; }
		[XmlElement(ElementName = "Ask")]
		public string Ask { get; set; }
		[XmlElement(ElementName = "EffectiveDate")]
		public string EffectiveDate { get; set; } 
	}

	[XmlRoot(ElementName = "HistoricalPrices")]
	public class HistoricalPrices
	{
		[XmlElement(ElementName = "HistoricalPrice")]
		public List<HistoricalPrice> HistoricalPrice { get; set; }
	}

	[XmlRoot(ElementName = "Security")]
	public class Security
	{
		[XmlElement(ElementName = "Id")]
		public string Id { get; set; }
		[XmlElement(ElementName = "BloombergId")]
		public string BloombergId { get; set; }
		[XmlElement(ElementName = "IssueCountry")]
		public string IssueCountry { get; set; }
		[XmlElement(ElementName = "HistoricalPrices")]
		public HistoricalPrices HistoricalPrices { get; set; }
	}

	[XmlRoot(ElementName = "Securities")]
	public class Securities
	{
		[XmlElement(ElementName = "Security")]
		public List<Security> Security { get; set; }
	}

}