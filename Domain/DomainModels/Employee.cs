using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Employee : IdentityUser

    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateBirth { get; set; }
        public string? Number { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? EMBG { get; set; }
        public DateOnly? Date_Joined { get; set; }
        public Guid? DepartmentId { get; set; }
        //public string? RoleId { get; set; }
        public virtual Department? Department { get; set; }
        //public virtual Role? Role { get; set; } // treba da se vide mapiranjeto
        public virtual ICollection<Group_Employee>? EmployeeGroups { get; set; }
        public virtual ICollection<Employee_InsurancePolicy>? InsurancePolicies { get; set; }
        public virtual ICollection<Leave>? Leaves { get; set; }
        public virtual ICollection<Employee_Position>? Positions { get; set; }
        public virtual ICollection<Employee_PensionInsurance>? PensionsInsurances { get; set; }
        public virtual ICollection<Attendance>? Attendances {  get; set; }
    }
}
