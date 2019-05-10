namespace NudgeApp.Data.Entities.Generic
{
    using System;

    public abstract class DbEntity : IDbEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DbEntity()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }
    }
}
