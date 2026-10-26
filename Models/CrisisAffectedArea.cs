using System;

namespace CrisisManagementPlatform.Models
{
    public class CrisisAffectedArea
    {
        public int CrisisId { get; set; }
        public Crisis? Crisis { get; set; }

        public int AffectedAreaId { get; set; }
        public AffectedArea? AffectedArea { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}