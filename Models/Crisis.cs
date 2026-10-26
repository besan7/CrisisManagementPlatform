using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrisisManagementPlatform.Models
{
    public class Crisis
    {
        public int CrisisId { get; set; }

        [Required(ErrorMessage = "حقل عنوان الأزمة إلزامي.")]
        [StringLength(200, ErrorMessage = "عنوان الأزمة يجب ألا يتجاوز 200 حرف.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "يرجى كتابة وصف تفصيلي للأزمة.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "يرجى تحديد تاريخ بدء الأزمة.")]

        public CrisisStatus Status { get; set; } = CrisisStatus.Registered;

        [Required(ErrorMessage = "يرجى اختيار تصنيف الأزمة.")]

        public CrisisCategory? CrisisCategory { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // هذه هي الخاصية التي يبحث عنها الـ AppDbContext والـ CrisisService
        public ICollection<CrisisAffectedArea> CrisisAffectedAreas { get; set; } = new List<CrisisAffectedArea>();

        [Required(ErrorMessage = "يرجى تحديد تاريخ بدء الأزمة.")]
        public DateTime? StartDate { get; set; }  // أضفنا علامة ?

        [Required(ErrorMessage = "يرجى اختيار تصنيف الأزمة.")]
        public int? CrisisCategoryId { get; set; } // أضفنا علامة ?
    }
}