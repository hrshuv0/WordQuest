using Core.Common.Enums;

namespace Core.Entities;

public class BaseEntity<T> : IBaseEntity<T>
{
    public T? Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public Status Status { get; set; }

    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }

    public BaseEntity()
    {
        CreatedTime = DateTime.Now;
        UpdatedTime = DateTime.Now;
        Status = Status.Active;
        CreatedBy = String.Empty;
        ModifiedBy = String.Empty;
    }
}