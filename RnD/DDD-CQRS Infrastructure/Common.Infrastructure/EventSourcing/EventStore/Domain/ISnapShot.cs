using Common.Infrastructure.EventSourcing.EventStore.Memento;
using System;

namespace Common.Infrastructure.EventSourcing.EventStore.Domain
{
    public interface ISnapShot
    {
        IMemento Memento { get; }
        Guid EventProviderId { get; }
        int Version { get; }
    }
}
