using System.ComponentModel.DataAnnotations;

namespace SG.Models
{
    public class Risk
    {
        #region Atributes
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Code { get; set; }
        [Required]
        public string? Danger { get; set; }
        [Required]
        public string? Vulnerability { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? Origin { get; set; }
        public bool Priority { get; set; }


        [Required, Range(1,3)]
        public int CID { get; set; }
        [Required, Range(1, 3)]
        public int DangerLevel { get; set; }
        [Required, Range(1, 3)]
        public int VulnerabilityLevel { get; set; }
        public int Level { get; set; }
        public string? LevelRange { get; set; }
        #endregion


        #region Relationships
        public int ResourceId { get; set; }
        public virtual Resource? Resource { get; set; }
        public int RiskTypeId { get; set; }
        public virtual RiskType? RiskType { get; set; }
        public int ResidualRiskId { get; set; }
        public virtual ResidualRisk? ResidualRisk { get; set; }
        public virtual ICollection<Control>? Controls { get; set; }
        #endregion
    }

    public class RiskType
    {
        #region Atributes
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        #endregion

        #region Relationships
        public virtual ICollection<Risk>? Risks { get; set; }
        #endregion
    }
}
