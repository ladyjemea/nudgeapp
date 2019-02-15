namespace NudgeApp.Data.Entities
{
    using System;

    public interface IDbEntity
    {
        Guid Id { get; set; }

        DateTime Created { get; set; }

        DateTime Modified { get; set; }

    }
}