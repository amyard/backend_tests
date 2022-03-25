namespace EventsStandards;

public class TransientService
{
    // because it's a transient, it must create a new guid than the instance will be created 
    public Guid Id { get; } = Guid.NewGuid();
}