namespace NudgeApp.Testing.Tests.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IDatabaseTesting
    {
        void RunTestAnonymousDatabase();
        void InsertRows(int entryCount, List<Guid> userIds = null);
        void Run();
    }
}