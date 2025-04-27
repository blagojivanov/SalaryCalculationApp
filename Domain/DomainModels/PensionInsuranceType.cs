using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PensionInsuranceType : BaseEntity
    {
        public string? Name { get; set; }
        public double? PercentOff {  get; set; }
        public virtual ICollection<PensionInsurance>? Insurances { get; set; }
    }
}
