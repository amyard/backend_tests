namespace EventsStandards;

public class TicketService
{
    public event EventHandler<TicketEventArgs> Ticked;
    private readonly TransientService _transientService;
    
    public TicketService(TransientService transientService)
    {
        _transientService = transientService;
        Ticked += OnEverySecond;
        Ticked += OnEveryFiveSeconds;
    }

    public void OnEverySecond(object? sender, TicketEventArgs args)
    {
        Console.WriteLine($"{args.Time.ToLongTimeString()} - {_transientService.Id}");
    }
    
    public void OnEveryFiveSeconds(object? sender, TicketEventArgs args)
    {
        if (args.Time.Second % 5 == 0)
            Console.WriteLine(args.Time.ToLongTimeString());
    }
    
    public void OnTick(TimeOnly time)
    {
        Ticked?.Invoke(this, new TicketEventArgs(time));
    }
}

public class TicketEventArgs
{
    public TicketEventArgs(TimeOnly time)
    {
        Time = time;
    }
    public TimeOnly Time { get; }
}