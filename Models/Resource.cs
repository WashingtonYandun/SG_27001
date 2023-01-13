using System.ComponentModel.DataAnnotations;

namespace SG.Models
{
    public class Resource
    {
        #region Atributes
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? Owner { get; set; }
        [Required]
        public string? Guardian { get; set; }

        [Range(1, 3)]
        public int Integrity { get; set; }
        [Range(1, 3)]
        public int Confidentiality { get; set; }
        [Range(1, 3)]
        public int Availavility { get; set; }

        public int Value { get; set; }
        #endregion


        #region Relationships
        public virtual ICollection<Risk>? Risks { get; set; }
        public int RelatedAreaId { get; set; }
        public virtual RelatedArea? RelatedArea { get; set; }
        public int ResourceTypeId { get; set; }
        public virtual ResourceType? ResourceType { get; set; }
        public int ResourceCategoryId { get; set; }
        public virtual ResourceCategory? ResourceCategory { get; set; }
        #endregion
    }

    public class ResourceType
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public class RelatedArea
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public class ResourceCategory
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
