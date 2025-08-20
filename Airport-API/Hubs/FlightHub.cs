using Microsoft.AspNetCore.SignalR;

namespace Airport.Api.Hubs
{
    public class FlightHub : Hub
    {
        // Серверээс клиент рүү шинэчлэл push хийхэд ашиглана
        public async Task SendStatusUpdate(string flightNumber, string status)
        {
            await Clients.All.SendAsync("ReceiveStatusUpdate", flightNumber, status);
        }
    }
}
