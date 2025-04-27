using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Permission_Roles : BaseEntity
    {
        //public string? RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        //public virtual Role? Role { get; set; }
    }
}
