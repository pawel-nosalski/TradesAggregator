using System;

namespace TradesAggregator.Library.Models.Domain
{
	/// <summary>
	/// Represents trade essential data model for reporting purposes
	/// </summary>
    public class Trade
    {
		public DateTime TradeDate { get; set; }	
		public string TransactionCode { get; set; }		
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public string Security { get; set; }
	}
}
