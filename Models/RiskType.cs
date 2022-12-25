using Microsoft.Build.Framework;

namespace SG.Models
{
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
