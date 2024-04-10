﻿namespace Borkfolio.Domain.Common
{
    public class AuditableEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public string? LastModifiedDate { get; set; }
    }
}
