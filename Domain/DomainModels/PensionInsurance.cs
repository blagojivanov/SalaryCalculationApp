using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PensionInsurance : BaseEntity
    {
        public Guid TypeId { get; set; }
        public virtual PensionInsuranceType Type { get; set; }
    }
}
