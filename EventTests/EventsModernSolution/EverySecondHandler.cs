using MediatR;

namespace EventsModernSolution;

public class EverySecondHandler : INotificationHandler<TimedNotification>
{
    private readonly TransientService _transientService;

    public EverySecondHandler(TransientService transientService)
    {
        _transientService = transientService;
    }

    public Task Handle(TimedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{notification.Time.ToLongTimeString()} - {_transientService.Id}");
        return Task.CompletedTask;
    }
}