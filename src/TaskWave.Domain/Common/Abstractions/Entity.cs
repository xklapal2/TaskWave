namespace TaskWave.Domain.Common.Abstractions;

public abstract class Entity(Ulid id)
{
    public Ulid Id { get; private init; } = id;

    protected readonly List<IDomainEvent> _domainEvents = [];

    public List<IDomainEvent> PopDomainEvents()
    {
        List<IDomainEvent> copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }
}