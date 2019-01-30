namespace NudgeApp.Common.Dtos
{
    using NudgeApp.Common.Enums;
    public class PreferemcesDto
    {
        public TransportationType PreferedTravelType { get; set; }
        public TransportationType ActualTravelType { get; set; }
        public TransportationType AimedTransportationType { get; set; }
    }
}
