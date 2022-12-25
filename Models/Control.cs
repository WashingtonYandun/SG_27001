using Microsoft.Build.Framework;

namespace SG.Models
{
    public class Control
    {
        #region Atributes
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public DateTime ImplementationTime { get; set; }
        [Required]
        public string? Description { get; set; }
        #endregion


        #region Relathionships
        public virtual ICollection<Risk>? Risks { get; set; }
        #endregion
    }
}
