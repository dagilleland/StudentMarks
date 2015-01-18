using Common.Infrastructure.EventSourcing.EventStore.Memento;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Infrastructure.EventSourcing.EventStore.Domain
{
    public interface IDomainRepository<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        TAggregate GetById<TAggregate>(Guid id)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new();

        void Add<TAggregate>(TAggregate aggregateRoot)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new();
    }
}
