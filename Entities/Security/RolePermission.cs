using Microsoft.AspNetCore.Identity;

namespace Entities.Security
{
    public class RolePermission
    {
        public Guid? RoleId { get; set; }
        public int PermissionId { get; set; }

        public IdentityRole? Role { get; set; }
        public Permission? Permission { get; set; }
    }
}
