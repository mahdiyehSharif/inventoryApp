using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public AppEmployee? Employee { get; set; }
    }
}