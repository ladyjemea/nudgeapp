namespace NudgeApp.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.InMemory;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories;

    [TestClass]
    public class DataAnalysisTest
    {
        private NudgeDbContext context;
        private NudgeRepository nudgeRepository;
        private Random random;

        [TestInitialize]
        public void Initialize()
        {
            this.random = new Random();

            var options = new DbContextOptionsBuilder<NudgeDbContext>()
                .UseInMemoryDatabase("Testing Db")
                .Options;

            this.context = new NudgeDbContext(options);

            this.nudgeRepository = new NudgeRepository(context);


        }

        [TestMethod]
        public void Test()
        {
            var userId = Guid.NewGuid();
            var nudgeId = Guid.NewGuid();

            for (var i = 0; i < 100; i++)
            {
                this.nudgeRepository.Insert(new NudgeEntity
                {
                    Id = nudgeId,
                    NudgeResult = NudgeResult.Failed,
                    Time = new DateTime(2019, 5, 6, 17 ,0,0),
                    UserId = userId
                });
            }

            var result = this.nudgeRepository.Get(nudgeId);


            Assert.AreEqual(userId, result.UserId);
        }
    }
}
