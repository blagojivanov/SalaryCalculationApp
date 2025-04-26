using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Permission : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Permission_Roles>? RolesApplied { get; set; }
    }
}
