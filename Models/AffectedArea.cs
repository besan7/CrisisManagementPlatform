using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrisisManagementPlatform.Models
{
    public class AffectedArea
    {
        [Key]
        public int AffectedAreaId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Navigation property
        public ICollection<CrisisAffectedArea> CrisisAffectedAreas { get; set; } = new List<CrisisAffectedArea>();
    }
}