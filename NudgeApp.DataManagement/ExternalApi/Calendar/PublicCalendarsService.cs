namespace NudgeApp.DataManagement.ExternalApi.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Services;

    public class PublicCalendarsService : IPublicCalendarsService
    {
        public async Task<IList<CalendarEvent>> GetEvents()
        {
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBMwd9dDJOx6DxD9ho9OMQ-egbU74vDtBw",
                ApplicationName = "Nudge App"
            });

            var eventList = new List<CalendarEvent>();

            var events = await service.Events.List("en.norwegian#holiday@group.v.calendar.google.com").ExecuteAsync();

            foreach (var @event in events.Items)
            {
                eventList.Add(new CalendarEvent
                {
                    Summary = @event.Summary,
                    Start = Convert.ToDateTime(@event.Start.Date),
                    End = Convert.ToDateTime(@event.End.Date)
                });
            }

            return eventList;
        }
    }
}
