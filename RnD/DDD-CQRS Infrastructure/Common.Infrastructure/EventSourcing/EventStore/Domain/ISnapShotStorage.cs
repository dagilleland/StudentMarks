using System;
using System.Collections.Generic;

namespace Common.Infrastructure.EventSourcing.EventStore.Domain
{
    public interface ISnapShotStorage<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        ISnapShot GetSnapShot(Guid entityId);
        void SaveShapShot(IEventProvider<TDomainEvent> entity);
    }
}
