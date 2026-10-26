using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrisisManagementPlatform.Models
{
    public class CrisisCategory
    {
        [Key]
        public int CrisisCategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Crisis> Crises { get; set; } = new List<Crisis>();
    }
}