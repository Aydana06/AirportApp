using Microsoft.AspNetCore.Mvc.RazorPages;
using AirportWebApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirportWebApp.Pages
{
    public class DepartureModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public List<FlightStatus> Flights { get; set; } = new List<FlightStatus>();

        public DepartureModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("AirportAPI");
                var response = await httpClient.GetAsync("api/Flight");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Flights = JsonSerializer.Deserialize<List<FlightStatus>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<FlightStatus>();
                }
                else
                {
                    // API-аас мэдээлэл авч чадахгүй бол хоосон жагсаалт
                    Flights = new List<FlightStatus>();
                }
            }
            catch (Exception ex)
            {
                // Алдаа гарвал хоосон жагсаалт
                Flights = new List<FlightStatus>();
                // Production-д logging нэмэх хэрэгтэй
                Console.WriteLine($"API дуудах үед алдаа: {ex.Message}");
            }
        }
    }
}
