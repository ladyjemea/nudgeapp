namespace NudgeApp.Data.Entities
{
    using System;

    public interface IDbEntity
    {
        Guid Id { get; set; }

    }
}