using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InsuranceItem : BaseEntity
    {
        public double Price { get; set; }
        public string? Description{ get; set; }
        public virtual ICollection<InsuranceItemPolicy>? Policies { get; set; }
    }
}
