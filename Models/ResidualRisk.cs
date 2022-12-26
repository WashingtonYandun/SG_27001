using System.ComponentModel.DataAnnotations;

namespace SG.Models
{
    public class ResidualRisk
    {
        #region Atributes
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Danger { get; set; }
        [Required]
        public string? Vulnerability { get; set; }

        [Required]
        public string? Type { get; set; }
        public string? Origin { get; set; }
        public string? Dimension { get; set; }
        public bool Priority { get; set; }

        [Required]
        public int CID { get; set; }
        [Required]
        public int DangerLevel { get; set; }
        [Required]
        public int VulnerabilityLevel { get; set; }
        public int Level { get; set; }
        public string? LevelRange { get; set; }
        #endregion
    }
}