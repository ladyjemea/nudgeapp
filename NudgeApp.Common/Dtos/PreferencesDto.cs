namespace NudgeApp.Common.Dtos
{
    using NudgeApp.Common.Enums;
    public class PreferemcesDto
    {
        public TravelTypes PreferedTravelType { get; set; }
        public TravelTypes ActualTravelType { get; set; }
        public TravelTypes AimedTransportationType { get; set; }
    }
}
