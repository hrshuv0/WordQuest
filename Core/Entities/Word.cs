namespace Core.Entities;

public class Word : BaseEntity<Guid>
{
    public string? Name { get; set; }
}