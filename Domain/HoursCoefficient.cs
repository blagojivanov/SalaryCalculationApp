namespace Domain
{
    public class HoursCoefficient : BaseEntity
    {
        public DateOnly? Start {  get; set; }
        public DateOnly? End { get; set;}
        public double? OvertimeCoefficient { get; set; }
        public double? NightCoefficient { get; set; }
        public virtual ICollection<Attendance>? Attendances { get; set; }
    }
}