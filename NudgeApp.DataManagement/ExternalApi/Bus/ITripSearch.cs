namespace NudgeApp.DataManagement.ExternalApi.Bus
{
    using System;

    public interface ITripSearch
    {
        /// <summary>
        /// Searches for trips between 2 stops.
        /// </summary>
        /// <param name="from"> The starting bust stop. </param>
        /// <param name="to"> The ending bus stop. </param>
        /// <param name="dateTime"> The time for departure/arrival. Can be left unset. If unset or null DateTime.Now will be set. </param>
        /// <param name="tripSchedule"> Sets if the dateTime set is for arrival or departure. Departure is default value if not set. </param>
        /// <returns> A list of all trips and details about each one. </returns>
        TripObject SearchTrip(string from, string to, DateTime? dateTime = null, TripSchedule? tripSchedule = TripSchedule.Departure);
    }
}