﻿namespace IELTSExamPlatform.CORE.Entities.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
