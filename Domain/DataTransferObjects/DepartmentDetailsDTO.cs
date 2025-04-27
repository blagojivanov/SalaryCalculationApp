namespace Domain.DataTransferObjects;

public class DepartmentDetailsDTO
{
    public Department Department { get; set; }
    public ICollection<Employee>? Employees { get; set; }
    public ICollection<DepartmentPositionDTO>? Positions { get; set; }
}