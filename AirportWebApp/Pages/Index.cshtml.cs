using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AirportWebApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirportWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        
        public List<FlightStatus> Flights { get; set; } = new List<FlightStatus>();

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
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
                    Flights = new List<FlightStatus>();
                }
            }
            catch (Exception ex)
            {
                Flights = new List<FlightStatus>();
                _logger.LogError(ex, "API дуудах үед алдаа гарлаа");
            }
        }
    }
}
