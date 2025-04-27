using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Employee_Position : BaseEntity
    {
        public string EmployeeId { get; set; }
        public Guid PositionId { get; set; }
        public Employee Employee { get; set; }
        public Position Position { get; set; }
        public DateOnly? Start_Date { get; set; }
        public DateOnly? End_Date { get; set; }
    }
}
