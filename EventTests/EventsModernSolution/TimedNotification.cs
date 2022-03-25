using MediatR;

namespace EventsModernSolution;

public class TimedNotification : INotification
{
    public TimedNotification(TimeOnly time)
    {
        Time = time;
    }

    public TimeOnly Time { get; }
}