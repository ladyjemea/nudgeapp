namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Dtos;

    public interface IEnvironmelntalInfoRepository
    {
        Guid Create(EnvironmelntalInfoDto info);
        EnvironmelntalInfoDto Get(Guid id);
    }
}