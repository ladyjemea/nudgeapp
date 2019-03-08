namespace NudgeApp.DataManagement.Implementation
{
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Repositories.Interfaces;
    using System;

    public class TripLogic
    {
        private readonly ITripRepository TripRepository;
        private readonly IUserRepository UserRepository;
        private readonly INudgeRepository NudgeRepository;
        private readonly IEnvironmelntalInfoRepository EnvironmelntalInfoRepository;

        public TripLogic(ITripRepository tripRepository, IUserRepository userRepository,
            INudgeRepository nudgeRepository, IEnvironmelntalInfoRepository environmelntalInfoRepository)
        {
            this.TripRepository = tripRepository;
            this.UserRepository = userRepository;
            this.NudgeRepository = nudgeRepository;
            this.EnvironmelntalInfoRepository = environmelntalInfoRepository;
        }

        public void AddTrip(TripDto trip, NudgeDto nudge, EnvironmelntalInfoDto envInfo, string userName)
        {
            var userId = this.UserRepository.GetUser(userName).Id;

            throw new NotImplementedException();

            if (userId != null)
            {
               // var envInfoId = this.EnvironmelntalInfoRepository.CreateInfo(envInfo);
                //this.TripRepository.Create(trip, userId, envInfoId);
            }
        }
    }
}
