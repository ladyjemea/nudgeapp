namespace NudgeApp.DataManagement.ExternalApi.Calendar
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPublicCalendarsService
    {
        Task<IList<CalendarEvent>> GetEvents();
    }
}