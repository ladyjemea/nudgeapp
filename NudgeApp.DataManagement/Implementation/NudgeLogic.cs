namespace NudgeApp.DataManagement.Implementation
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    public class NudgeLogic : INudgeLogic
    {
        private readonly INudgeRepository NudgeRepository;
        private readonly IEnvironmelntalInfoRepository EnvironmelntalInfoRepository;
        private readonly IUserRepository UserRepository;

        public NudgeLogic(INudgeRepository nudgeRepository, IEnvironmelntalInfoRepository environmelntalInfoRepository, IUserRepository userRepository)
        {
            this.NudgeRepository = nudgeRepository;
            this.EnvironmelntalInfoRepository = environmelntalInfoRepository;
            this.UserRepository = userRepository;
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
    }
}
