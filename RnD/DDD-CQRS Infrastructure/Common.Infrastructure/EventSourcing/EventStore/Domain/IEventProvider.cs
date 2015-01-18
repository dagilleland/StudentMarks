using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.EventSourcing.EventStore.Domain
{
    public interface IEventProvider<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        void Clear();
        void LoadFromHistory(IEnumerable<TDomainEvent> domainEvents);
        void UpdateVersion(int version);
        Guid Id { get; }
        int Version { get; }
        IEnumerable<TDomainEvent> GetChanges();
    }
}
