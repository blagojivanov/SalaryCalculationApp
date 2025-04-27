using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InsurancePolicy : BaseEntity
    {
        public string? Name { get; set; }
        public virtual ICollection<Employee_InsurancePolicy>? InsurancePolicies { get; set; }
        public virtual ICollection<InsuranceItemPolicy>? InsuranceItems { get; set; }
    }
}
