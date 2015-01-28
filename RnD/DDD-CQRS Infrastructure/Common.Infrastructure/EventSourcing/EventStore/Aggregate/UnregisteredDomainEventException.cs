using Common.Infrastructure.EventSourcing.EventStore.Domain;
using System;
using System.Collections.Generic;

namespace Common.Infrastructure.EventSourcing.EventStore.Aggregate
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message) { }
    }
}
