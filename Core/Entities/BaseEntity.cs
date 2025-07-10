namespace Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdateAt { get; private set; }

    public void SetUpdate()
    {
        UpdateAt = DateTime.UtcNow;
    }
}