using System;
using System.Collections.Generic;

namespace NudgeApp.Testing.Tests.Interfaces
{
    public interface IDatabaseTesting
    {
        void RunTestAnonymousDatabase();
        void InsertRows(List<Guid> userIds = null);
    }
}