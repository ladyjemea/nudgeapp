namespace NudgeApp.Data.Entities.Generic
{
    using System;

    public interface IDbEntity
    {
        Guid Id { get; set; }

        DateTime CreatedOn { get; set; }

        DateTime ModifiedOn { get; set; }

    }
}