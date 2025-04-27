using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Attendance : BaseEntity
    {
        public string? EmployeeId { get; set; }
        public Guid HoursCoefficientId { get; set; }
        public double? Overtime { get; set; }
        public double? NightHours { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public required Employee Employee { get; set; }
        public HoursCoefficient? HoursCoefficient {  get; set; }
    }
}
