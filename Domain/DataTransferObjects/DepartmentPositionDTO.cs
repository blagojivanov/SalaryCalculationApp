namespace Domain.DataTransferObjects;

public class DepartmentPositionDTO
{
    public string? PositionName { get; set; }
    public int? FreeSlots { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? PositionId { get; set; }
    public int? PositionCount { get; set; }
    public virtual ICollection<Position>? Positions { get; set; } = new List<Position>();
    // public string 
}