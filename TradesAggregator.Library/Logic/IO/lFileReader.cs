namespace TradesAggregator.Library.Logic.IO
{
    /// <summary>
    /// Abstraction for file data read and transformation to model class
    /// </summary>
    public interface lFileReader
    {
        T ReadFile<T>(string filePath);
    }
}
