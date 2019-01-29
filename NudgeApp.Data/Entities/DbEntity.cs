namespace NudgeApp.Data.Entities
{
    using System;

    public abstract class DbEntity
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
