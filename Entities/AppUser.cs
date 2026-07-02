using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        [MaxLength]
        public string? FirstName { get; set; }

        [MaxLength]
        public string? LastName { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public AppEmployee? Employee { get; set; }
    }
}