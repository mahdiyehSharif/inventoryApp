using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class AppJob
{

    [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int JobID {get; set; }

    [MaxLength]
    public string? JobName {get; set; }

    [MaxLength]
    public String? ManagementName {get; set;}

    [MaxLength]
    public string? DeputyName {get; set; }

    [MaxLength]
    public String? PositionName {get; set;}

}
}
