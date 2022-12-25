using System.ComponentModel.DataAnnotations;

namespace SG.Models
{
    public class Risk
    {
        #region Atributes
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Code
        {
            get { return Code; }
            set
            {
                Code = Resource?.Name?.Substring(0, 1) + Id.ToString();
            }
        }
        [Required]
        public string? Danger { get; set; }
        [Required]
        public string? Vulnerability { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Type { get; set; }
        public string? Origin { get; set; }
        public bool Priority { get; set; }


        [Required]
        public int CID { get; set; }
        [Required]
        public int DangerLevel { get; set; }
        [Required]
        public int VulnerabilityLevel { get; set; }
        public int Level
        {
            get { return Level; }
            set
            {
                Level = CID * DangerLevel * VulnerabilityLevel;
            }
        }
        public string? LevelRange
        {
            get
            {
                return LevelRange;
            }
            set
            {
                if (Level >= 1 && Level <= 3)
                {
                    LevelRange = RiskRange.LOW.ToString();
                }

                if (Level >= 4 && Level <= 8)
                {
                    LevelRange = RiskRange.MEDIUM.ToString();
                }

                if (Level >= 9 && Level <= 27)
                {
                    LevelRange = RiskRange.HIGH.ToString();
                    Priority = true;
                }
            }
        }
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
}
