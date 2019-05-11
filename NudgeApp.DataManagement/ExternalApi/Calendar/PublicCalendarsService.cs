namespace NudgeApp.DataManagement.ExternalApi.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Services;

    public class PublicCalendarsService : IPublicCalendarsService
    {
        public async Task<IList<CalendarEvent>> GetEvents()
        {
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAPb-ZpzEgSUY29r7MXKHkvHO9aI4R9FmQ",
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

        public async Task<CalendarEvent> GetTodaysEvent()
        {
            var events = await this.GetEvents();

            return events.FirstOrDefault(ev => ev.Start.Date == DateTime.Now.Date);
        }
    }
}
