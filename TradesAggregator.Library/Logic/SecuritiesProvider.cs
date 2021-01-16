using System;
using System.Collections.Generic;
using TradesAggregator.Library.Logic.IO;
using TradesAggregator.Library.Logic.Mappers;
using TradesAggregator.Library.Models.Domain;

namespace TradesAggregator.Library.Logic
{
    /// <summary>
    /// Reads securities data and maps it to domain model
    /// </summary>
    public class SecuritiesProvider : ISecuritiesProvider
    {
        private readonly lFileReader securitiesFileReader;
        private readonly IMapper<Models.Xml.Security, Security> securitiesMapper;

        public SecuritiesProvider(lFileReader securitiesFileReader, IMapper<Models.Xml.Security, Security> securitiesMapper)
        {
            if (securitiesFileReader == null)
            {
                throw new ArgumentNullException(nameof(securitiesFileReader));
            }

            if (securitiesMapper == null)
            {
                throw new ArgumentNullException(nameof(securitiesMapper));
            }

            this.securitiesFileReader = securitiesFileReader;
            this.securitiesMapper = securitiesMapper;
        }

        public List<Security> GetSecurities(string securitiesFilePath)
        {
            if (string.IsNullOrEmpty(securitiesFilePath))
            {
                throw new ArgumentException($"{nameof(securitiesFilePath)} is either null or empty !");
            }

            // first, get the raw file data
            var securitiesFileObject = this.securitiesFileReader.ReadFile<Models.Xml.Securities>(securitiesFilePath);

            if (securitiesFileObject != null)
            {
                // map securities to the domain model (with only necessary data required for the report)
                var securities = new List<Security>();
                foreach (var securityEntry in securitiesFileObject.Security)
                {
                    var security = this.securitiesMapper.Map(securityEntry);
                    securities.Add(security);
                }

                return securities;
            }

            return null;
        }
    }
}
