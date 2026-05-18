using System.ComponentModel.DataAnnotations;
namespace Entities
{
    public class AppJob
{

    [Key]
    public Guid? JobID {get; set; }

    [StringLength(50)]
    public string? JobName {get; set; }

    [StringLength(50)]
    public String? ManagementName {get; set;}

    [StringLength(50)]
    public string? DeputyName {get; set; }

    [StringLength(50)]
    public String? PositionName {get; set;}

}
}
