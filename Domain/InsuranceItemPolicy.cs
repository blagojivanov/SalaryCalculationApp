using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InsuranceItemPolicy : BaseEntity
    {
        public Guid InsuranceItemId { get; set; }
        public Guid InsurancePolicyId { get; set; }
        public virtual InsuranceItem InsItem { get; set;}
        public virtual InsurancePolicy InsPolicy { get; set;}
    }
}
