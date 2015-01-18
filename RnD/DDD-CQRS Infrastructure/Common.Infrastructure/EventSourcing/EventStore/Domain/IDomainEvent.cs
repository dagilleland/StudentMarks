using System;

namespace Common.Infrastructure.EventSourcing.EventStore.Domain
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        Guid AggregateId { get; set; }
        int Version { get; set; }
    }
}
