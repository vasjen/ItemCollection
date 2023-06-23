namespace  Common.Models
{
    public interface IEntity
    {
        Guid Id { get; init; }
        string Name { get; set; }
        List<Tag> Tags { get; }
    }
}