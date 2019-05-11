using Microsoft.VisualStudio.TestTools.UnitTesting;
using NudgeApp.Common.Enums;
using NudgeApp.DataManagement.ExternalApi.Weather;
using NudgeApp.DataManagement.Helpers;

namespace NudgeApp.DataManagement.Test
{
    [TestClass]
    public class UnitTest1
    {
        public DataAgregator SuT { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            this.SuT = new DataAgregator();
        }

        [TestMethod]
        public void GetSkyCoverage_ReturnsCloudy()
        {
            var result = this.SuT.GetSkyCoverage(80);

            Assert.AreEqual(SkyCoverageType.Cloudy, result);
        }

        [TestMethod]
        public void GetWeatherCondition_ReturnsCool()
        {
            HourlyForecast forecast = new HourlyForecast {
                Temperature = new UnitInfo
                {
                    Value = 2
                },
                WindGust = new WindInfo
                {
                    Speed = new UnitInfo
                    {
                        Value = 10
                    }
                },
                Wind = new WindInfo
                {
                    Speed = new UnitInfo
                    {

                        Value = 10
                    }
                },
                Rain = new UnitInfo
                {
                    Value = 15
                },
                Snow = new UnitInfo
                {
                    Value = 15
                }

            };
            var result = this.SuT.GetWeatherCondition(forecast);

            Assert.AreEqual(WeatherCondition.Cold, result);
        }
    }
}
