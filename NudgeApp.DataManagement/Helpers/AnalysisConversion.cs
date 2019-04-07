using NudgeApp.Common.Enums;
using NudgeApp.DataManagement.ExternalApi.Weather;
using System;
using System.Collections.Generic;
using System.Text;

namespace NudgeApp.DataManagement.Helpers
{
    public class AnalysisConversion : IAnalysisConversion
    {
        public SkyCoverageType GetSkyCoverage(int cloudCoverPercentage)
        {
            SkyCoverageType coverage = cloudCoverPercentage <= 15 ? SkyCoverageType.Clear : SkyCoverageType.PartlyCloudy;

            if (cloudCoverPercentage > 75) coverage = SkyCoverageType.Cloudy;

            return coverage;
        }

        public RoadCondition GetRoadCondition(HourlyForecast forecast)
        {
            RoadCondition roadCondition;

            if (forecast.Temperature.Value <= 3)
            {
                if (forecast.SnowProbability < 20)
                {
                    roadCondition = RoadCondition.Ice;
                }
                else
                {
                    roadCondition = RoadCondition.Snow;
                }
            }
            else
            {
                if (forecast.RainProbability > 20)
                {
                    roadCondition = RoadCondition.Wet;
                }
                else
                {
                    roadCondition = RoadCondition.Dry;
                }
            }

            return roadCondition;
        }

        public PrecipitationCondition GetPrecipitation(HourlyForecast forecast)
        {
            PrecipitationCondition precipitation;

            if (forecast.PreciptationProbability > 40)
            {
                if (forecast.RainProbability > forecast.SnowProbability)
                {
                    precipitation = PrecipitationCondition.Rainy;
                }
                else
                {
                    precipitation = PrecipitationCondition.Snowy;
                }

            }
            else
            {
                precipitation = PrecipitationCondition.NoPrecipitation;
            }
            return precipitation;
        }

        public WeatherCondition GetWeatherCondition(HourlyForecast forecast)
        {
            WeatherCondition weather;

            if (forecast.WindGust.Speed.Value >= 25 && forecast.Wind.Speed.Value >= 20)
            {
                weather = WeatherCondition.StrongWinds;
            }
            else
            {
                if (forecast.WindGust.Speed.Value >= 10 && 24 >= forecast.WindGust.Speed.Value
                    || forecast.Wind.Speed.Value >= 10 && 24 >= forecast.Wind.Speed.Value)
                {
                    weather = WeatherCondition.LightWinds;
                }
                else
                {
                    weather = WeatherCondition.Calm;
                }
            }

            if (forecast.Rain.Value >= 20)
            {
                weather = WeatherCondition.Rain;
            }
            else
            {
                weather = WeatherCondition.NoRain;
            }

            if (forecast.Snow.Value >= 20)
            {
                weather = WeatherCondition.Snow;
            }
            else
            {
                weather = WeatherCondition.NoSnow;
            }

            if (forecast.Temperature.Value < -10)
            {
                weather = WeatherCondition.Freezing;
            }

            if (forecast.Temperature.Value >= -10 && 8 >= forecast.Temperature.Value)
            {
                weather = WeatherCondition.Cold;
            }
            else if (forecast.Temperature.Value > 8 && 14 > forecast.Temperature.Value)
            {
                weather = WeatherCondition.Cool;
            }
            else
            {
                weather = WeatherCondition.Warm;
            }

            return weather;
        }

        public WeatherCondition GetWeatherCondition(CurrentForecast forecast)
        {
            WeatherCondition weather;

            if (forecast.Wind.Speed.Value >= 20)
            {
                weather = WeatherCondition.StrongWinds;
            }
            else
            {
                if (forecast.Wind.Speed.Value >= 10 && 24 >= forecast.Wind.Speed.Value)
                {
                    weather = WeatherCondition.LightWinds;
                }
                else
                {
                    weather = WeatherCondition.Calm;
                }
            }

            if (forecast.HasPrecipitation)
            {
                weather = WeatherCondition.Rain;
            }
            else
            {
                weather = WeatherCondition.NoRain;
            }

            if (forecast.HasSnow)
            {
                weather = WeatherCondition.Snow;
            }
            else
            {
                weather = WeatherCondition.NoSnow;
            }

            if (forecast.Temperature.Metric.Value < -10)
            {
                weather = WeatherCondition.Freezing;
            }

            if (forecast.Temperature.Metric.Value >= -10 && 8 >= forecast.Temperature.Metric.Value)
            {
                weather = WeatherCondition.Cold;
            }
            else if (forecast.Temperature.Metric.Value > 8 && 14 > forecast.Temperature.Metric.Value)
            {
                weather = WeatherCondition.Cool;
            }
            else
            {
                weather = WeatherCondition.Warm;
            }

            return weather;
        }

        public Probabilities GetProbabilities(HourlyForecast forecast)
        {
            Probabilities probabilities;

            if (forecast.RainProbability > 40)
            {
                probabilities = Probabilities.Rain;
            }
            if (forecast.SnowProbability > 40)
            {
                probabilities = Probabilities.Snow;
            }
            if (forecast.IceProbability > 40)
            {
                probabilities = Probabilities.Ice;
            }
            if (forecast.RainProbability > 40 && 10 < forecast.RainProbability && forecast.Temperature.Value > -3 && 3 < forecast.Temperature.Value)
            {
                probabilities = Probabilities.Slippery;
            }
            else
            {
                probabilities = Probabilities.NotEvaluated;
            }
            return probabilities;
        }

        public Probabilities GetProbabilities(CurrentForecast forecast)
        {
            Probabilities probabilities;

            if (forecast.HasPrecipitation)
            {
                probabilities = Probabilities.Rain;
            }
            else
            {
                probabilities = Probabilities.NotEvaluated;
            }
            return probabilities;
        }

        //public Others GetOthers(CurrentForecast forecast)
        //{
        //    Others others;
        //    if (forecast.RealFeelTemperature.Metric.Value >= 15
        //        && forecast.IsDayTime == true
        //        && forecast.HasPrecipitation
        //        && forecast.Wind.Equals)
        //}

        public Others GetOthers(HourlyForecast forecast)
        {
            Others others;

            if (forecast.RealFeelTemperature.Value >= 15
                && forecast.IsDaylight == true
                && forecast.Visibility.Value > 8
                && forecast.PreciptationProbability < 30
                && forecast.Wind.Speed.Value < 9
                && forecast.WindGust.Speed.Value < 9)
            {
                others = Others.ADayAtThePark;
            }
            else
            {
                others = Others.NotEvaluated;
            }

            if (forecast.Rain.Value > 10
                && forecast.Temperature.Value > -3 && 3 < forecast.Temperature.Value
                && forecast.Visibility.Value < 5)
            {
                others = Others.SlipperyForDriving;
            }
            else
            {
                others = Others.NotEvaluated;
            }

            if (forecast.Snow.Value > 10
                && forecast.Temperature.Value == 0
                && forecast.Wind.Speed.Value > 15
                && forecast.Wind.Speed.Value > 15
                && forecast.Visibility.Value < 5)
            {
                others = Others.PoorDrivingConditions;
            }
            else
            {
                others = Others.NotEvaluated;
            }

            if (forecast.Temperature.Value < -10
                || forecast.Snow.Value > 10
                && forecast.Temperature.Value < -10)
            {
                others = Others.PreferableToDrive;
            }
            else
            {
                others = Others.NotEvaluated;
            }

            if (forecast.Temperature.Value > -6 && -1 > forecast.Temperature.Value // check if the preceipitation is snow and if the snow level is betweem 1 and 6
                && forecast.Snow.Value > 1 && 6 < forecast.Snow.Value)
            {
                others = Others.GoodForSki;
            }
            else
            {
                others = Others.NotEvaluated;
            }
            return others;
        }


    }
}
