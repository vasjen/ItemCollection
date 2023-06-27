namespace  Common.Core.Entities
{
    public interface IEntity
    {
        Guid Id { get; init; }
        string Name { get; set; }
        DateTimeOffset CreatedTime {get;init;}
    }
}