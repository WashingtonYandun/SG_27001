using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public string? Owner { get; set; }
        [Required]
        public string? Guardian { get; set; }
        public string? Category { get; set; }
        public string? RelatedArea { get; set; }
        public string? Tags { get; set; }
        [Range(1, 3)]
        public int Integrity { get; set; }
        [Range(1, 3)]
        public int Confidentiality { get; set; }
        [Range(1, 3)]
        public int Availavility { get; set; }
        [Range(1, 3)]
        public int Value { get; set; }

        #region Relationships
        public virtual List<Risk>? Risks { get; set; }
        #endregion
    }
}
