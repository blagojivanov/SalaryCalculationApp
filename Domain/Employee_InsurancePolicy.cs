using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Employee_InsurancePolicy : BaseEntity
    {
        //public string EmployeeId { get; set; }
        public Guid InsurancePolicyId { get; set;}
        public DateOnly? StartDate {  get; set; }
        public DateOnly? EndDate { get;set; }
        public virtual Employee Employee { get; set; }
        public virtual InsurancePolicy Insurance { get; set; }
    }
}
