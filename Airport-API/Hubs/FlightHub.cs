using Microsoft.AspNetCore.SignalR;

namespace Airport.Api.Hubs
{
    /// <summary>
    /// Нислэгийн мэдээллийн бодит цагийн шинэчлэлийг
    /// клиентүүдэд илгээх SignalR Hub.
    /// </summary>
    public class FlightHub : Hub
    {
        /// <summary>
        /// Нислэг шинэчлэгдсэн тухай бүх клиентүүдэд мэдэгдэнэ.
        /// </summary>
        public async Task NotifyFlightUpdated()
        {
            await Clients.All.SendAsync(HubEvents.FlightUpdated);
        }
    }

    public static class HubEvents
    {
        public const string FlightUpdated = "FlightUpdated";
        public const string SeatTaken = "SeatTaken";
    }

}
