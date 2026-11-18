using System.ComponentModel.DataAnnotations;

namespace CIRMS_Backend.Models
{
    public class Incident
    {
        [Key]
        public int IncidentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string IncidentType { get; set; }

        public string? FileName { get; set; }

        [Required]
        public DateTime ReportDate { get; set; }

        [Required]
        public string IpAddress { get; set; }

        [Required]
        public string Severity { get; set; }

        [Required]
        public string Status { get; set; }

        public string? AssignedTo { get; set; }

        public string? SocReport { get; set; }
    }
}