using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Department_Position : BaseEntity
    {
        public Guid DepartmentId { get; set; }
        //public string EmployeeId {  get; set; }
        public Department? Department { get; set; }
        public Employee? Employee { get; set; }
        public int? PositionCount {  get; set; }
        public int? FreeSpaces { get; set; }
        
    }
}
