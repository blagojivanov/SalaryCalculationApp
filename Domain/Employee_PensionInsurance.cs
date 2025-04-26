using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Employee_PensionInsurance : BaseEntity
    {
        //public Guid? EmployeeId { get; set; }
        public Guid PensionInsuranceId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual PensionInsurance PensionInsurance { get; set; }
    }
}
