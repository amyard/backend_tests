using EventsStandards;

var builder = WebApplication.CreateBuilder();

builder.Services.AddTransient<TicketService>();
builder.Services.AddTransient<TransientService>();
builder.Services.AddHostedService<TickerBackgroundService>();

var app = builder.Build();

app.Run();