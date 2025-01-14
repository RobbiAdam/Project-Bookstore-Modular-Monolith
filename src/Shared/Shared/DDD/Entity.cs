namespace Shared.DDD;

public abstract class Entity<TId> : IEntity<TId>
{
    public TId Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
