using Core.Common.Enums;

namespace Core.Entities;

public interface IBaseEntity<T>
{
    public T? Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public Status Status { get; set; }
}