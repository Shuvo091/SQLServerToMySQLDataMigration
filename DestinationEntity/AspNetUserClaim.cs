﻿namespace FSSRC_DataMigration.DestinationEntity
{
    public class AspNetUserClaim
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }
}
