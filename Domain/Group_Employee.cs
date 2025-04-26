using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Group_Employee : BaseEntity
    {
        public Guid GroupId { get; set; }
        public string EmployeeId { get; set;}
        public DateOnly? StartDate {  get; set; }
        public DateOnly? EndDate { get; set; }
        public virtual Group? Group { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
