using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Leave : BaseEntity
    {
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public bool Approved { get; set; }
        public string? Reason { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public Guid? LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }
    }
}
