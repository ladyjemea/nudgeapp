namespace NudgeApp.DataManagement.Implementation
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.OracleDb;
    using NudgeApp.Data.OracleDb.Queries;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    public class NudgeLogic : INudgeLogic
    {
        private readonly INudgeRepository NudgeRepository;
        private readonly IEnvironmelntalInfoRepository EnvironmelntalInfoRepository;
        private readonly IUserRepository UserRepository;
        private readonly INudgeOracleConnection NudgeOracleConnection;
        private readonly IAnonymousNudgesRepository AnonymousNudgesRepository;

        public NudgeLogic(INudgeRepository nudgeRepository, IEnvironmelntalInfoRepository environmelntalInfoRepository, IUserRepository userRepository, INudgeOracleConnection nudgeOracleConnection, IAnonymousNudgesRepository anonymousNudgesRepository)
        {
            this.NudgeRepository = nudgeRepository;
            this.EnvironmelntalInfoRepository = environmelntalInfoRepository;
            this.UserRepository = userRepository;
            this.NudgeOracleConnection = nudgeOracleConnection;
            this.AnonymousNudgesRepository = anonymousNudgesRepository;
        }

        public void AddNudge(NudgeDto nudge, EnvironmelntalInfoDto envInfo, string userName)
        {
            var userId = this.UserRepository.GetUser(userName).Id;

            if (userId != null)
            {
                var envId = this.EnvironmelntalInfoRepository.Create(envInfo);
                this.NudgeRepository.Create(nudge, userId, envId);
            }
            else
                Console.WriteLine("Trying to add nudge. " + userName + " does not exist!");
        }

        public void Test()
        {
            var entity = new AnonymousNudgeEntity()
            {
                ActualTransportationType = TransportationType.Bike,
                Precipitation = 30,
                Result = NudgeResult.Successful,
                Road = RoadStateType.dry,
                SkyCoverage = SkyCoverageType.clear,
                Temperature = 23,
                UserPreferedTransportationType = TransportationType.Car,
                Wind = 30
            };
            this.NudgeOracleConnection.Test();
            //this.AnonymousNudgesRepository.Insert(entity);
        }
    }
}
