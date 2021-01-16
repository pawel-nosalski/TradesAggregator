namespace TradesAggregator.Library.Logic.Mappers
{
    /// <summary>
    /// Most generic abstraction we can have ;), but still required for DI
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface IMapper<T1, T2>
    {
        T2 Map(T1 input);
    }
}
