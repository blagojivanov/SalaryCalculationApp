namespace Domain
{
    public class PointPrice : BaseEntity
    {
        public DateOnly? Start { get; set; }
        public DateOnly? End { get; set; }
        public virtual ICollection<Position>? Positions { get; set; }
        public double? Price { get; set; }
    }
}