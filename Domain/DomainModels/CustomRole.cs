using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CustomRole : IdentityRole
    {
        public virtual ICollection<Permission_Roles>? AppliedPermissions { get; set; }

    }
}
