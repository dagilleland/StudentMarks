using Common.Infrastructure.EventSourcing.EventStore.Domain;
using System;
using System.Collections.Generic;

namespace Common.Infrastructure.EventSourcing.EventStore
{
    public interface IRegisterChildEntities<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        void RegisterChildEventProvider(IEntityEventProvider<TDomainEvent> entityEventProvider);
    }
}
