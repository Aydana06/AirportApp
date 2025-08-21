using Airport.Api.Hubs;
using AirportLibrary;
using AirportLibrary.repo;
using AirportLibrary.services;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

//  SQLite database зам
var dbPath = builder.Configuration.GetConnectionString("AirportDb");

builder.Services.AddSingleton(new Database(dbPath));
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<FlightService>();
builder.Services.AddScoped<PassengerService>();

builder.Services.AddScoped<IFlightRepository, FlightRepository>();



//   Controller, Swagger, SignalR, CORS
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

//  CORS тохиргоо (Blazor WebApp 7171 портоос холбогдоход)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        //policy.WithOrigins("https://localhost:7171") // Blazor WebApp порт
        //      .AllowAnyHeader()
        //      .AllowAnyMethod()
        //      .AllowCredentials(); //  SignalR-д заавал байх ёстой
       policy.WithOrigins("https://localhost:7171", "http://localhost:7171")
      .AllowAnyHeader()
      .AllowAnyMethod()
      .AllowCredentials();

    });
});

var app = builder.Build();

//  Middleware тохиргоо
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();          //  MapHub ажиллахын тулд хэрэгтэй
app.UseCors();             //  CORS хэрэгжүүлэлт
app.UseAuthorization();

app.MapControllers();

// SignalR Hub холболт
app.MapHub<FlightHub>("/flighthub");
app.MapHub<SeatHub>("/seathub");

app.Run();