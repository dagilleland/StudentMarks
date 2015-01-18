using Common.Infrastructure.EventSourcing.EventStore.Memento;
using System;
using System.Collections.Generic;

namespace Common.Infrastructure.EventSourcing.EventStore.Domain
{
    public interface IDomainEventStorage<TDomainEvent> : ISnapShotStorage<TDomainEvent>, ITransactional where TDomainEvent : IDomainEvent
    {
        IEnumerable<TDomainEvent> GetAllEvents(Guid eventProviderId);
        IEnumerable<TDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId);
        int GetEventCountSinceLastSnapShot(Guid eventProviderId);
        void Save(IEventProvider<TDomainEvent> eventProvider);
    }
}
