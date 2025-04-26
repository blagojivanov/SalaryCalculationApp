using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employee_InsurancePolicy> EmployeePolicies { get; set; }
        public DbSet<Employee_PensionInsurance> EmployeeInsurances { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Group_Employee> EmployeesInGroups { get; set; }
        public DbSet<HoursCoefficient> HoursCoefficients { get; set; }
        public DbSet<InsuranceItem> InsuranceItems { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicies { get; set; }
        public DbSet<InsuranceItemPolicy> InsuranceItemsInPolicies { get; set; }
        public DbSet<PensionInsurance> PensionInsurances { get; set; }
        public DbSet<PensionInsuranceType> PensionInsuranceTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Permission_Roles> PermissionRoles { get; set; }
        public DbSet<PointPrice> PointPrices { get; set; }
        public DbSet<Position> Positions { get; set; }
        //public DbSet<Role> AppRoles { get; set; }
        public DbSet<Department_Position> PositionsInDepartments { get; set; }
        public DbSet<Employee_Position> Employee_Positions { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<Domain.CustomRole> CustomRole { get; set; } = default!;
        public DbSet<Domain.Role> Role { get; set; } = default!;
        

    }
}
