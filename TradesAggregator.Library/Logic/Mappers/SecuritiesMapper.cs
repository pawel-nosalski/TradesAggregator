using System;
using TradesAggregator.Library.Models.Domain;

namespace TradesAggregator.Library.Logic.Mappers
{
    public class SecuritiesMapper : IMapper<Models.Xml.Security, Security>
    {
        /// <summary>
        /// Maps Security from polluted xml model to domain one
        /// </summary>
        /// <param name="securityInput"></param>
        /// <returns></returns>
        public Security Map(Models.Xml.Security securityInput)
        {
            if(securityInput == null)
            {
                throw new ArgumentNullException(nameof(securityInput));
            }

            var security = new Security
            {
                Id = Convert.ToInt32(securityInput.Id),
                BloombergId = securityInput.BloombergId
            };

            return security;
        }
    }
}
