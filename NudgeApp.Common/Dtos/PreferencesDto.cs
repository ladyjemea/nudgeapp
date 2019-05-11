namespace NudgeApp.Common.Dtos
{
    using NudgeApp.Common.Enums;
    public class PreferencesDto
    {
        public TransportationType PreferedTravelType { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
        public bool RainyTrip { get; set; }
        public bool SnowyTrip { get; set; }
    }
}
