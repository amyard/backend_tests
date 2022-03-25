namespace EventsStandards;

public class TickerBackgroundService : BackgroundService
{
    private readonly TicketService _ticketService;

    public TickerBackgroundService(TicketService ticketService)
    {
        _ticketService = ticketService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _ticketService.OnTick(TimeOnly.FromDateTime(DateTime.Now));
            await Task.Delay(1000, stoppingToken);
        }
    }
}