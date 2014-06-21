namespace NProg.Distributed.Core.Data
{
    public interface IIdentifiableEntity<TKey>
    {
        TKey EntityId { get; set; }
    }

}
