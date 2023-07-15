namespace EventBus.Messages.Events;

public abstract class IntegrationBaseEvent
{
    public IntegrationBaseEvent()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }

    public IntegrationBaseEvent(Guid id, DateTime createdDate)
    {
        Id = id;
        CreatedDate = createdDate;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedDate { get; private set; }
}