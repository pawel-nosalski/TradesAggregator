using System;
using System.IO;
using System.Xml.Serialization;

namespace TradesAggregator.Library.Logic.IO
{
    /// <summary>
    /// Reads given XML file from disk and serializes it to template class
    /// </summary>
    public class XmlFileReader : lFileReader
    {
        public T ReadFile<T>(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException($"{nameof(filePath)} is either null or empty !");
            }

            if(!File.Exists(filePath))
            {
                throw new InvalidOperationException($"File under the path {filePath} does not exist");
            }

            try
            {
                T fileObject = default(T);
                XmlSerializer xmlFileSerializer = new XmlSerializer(typeof(T));

                using (var reader = new StreamReader(filePath))
                {
                    fileObject = (T)xmlFileSerializer.Deserialize(reader);
                }

                return fileObject;
            }
            catch(IOException)
            {
                // For some IO - related reason, we can't read file. Consuming exception and returning null to the caller to handle it

                return default(T);
            }
        }
    }
}
