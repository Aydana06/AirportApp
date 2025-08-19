using Microsoft.AspNetCore.SignalR;

namespace Airport.Api.Hubs
{
    /// <summary>
    /// Суудлын өөрчлөлтийн бодит цагийн мэдээллийг
    /// клиентүүдэд илгээх SignalR Hub.
    /// </summary>
    public class SeatHub : Hub
    {
        // Тусгай method шаардлагагүй —
        // суудал бүртгэгдэх үед CheckInController-оос broadcast хийнэ.
    }
}
