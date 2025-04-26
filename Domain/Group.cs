using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Group : BaseEntity
    {
        public string? Name { get; set; }
        public virtual ICollection<Group_Employee>? EmployeeGroups { get;}
    }
}
