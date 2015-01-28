using Common.Infrastructure.EventSourcing.EventStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Events
{
    [Serializable]
    public class AccountOpenedEvent : DomainEvent
    {
        public Guid AccountId { get; private set; }
        public Guid ClientId { get; private set; }
        public string AccountName { get; private set; }
        public string AccountNumber { get; private set; }

        public AccountOpenedEvent(Guid accountId, Guid clientId, string accountName, string accountNumber)
        {
            AccountId = accountId;
            ClientId = clientId;
            AccountName = accountName;
            AccountNumber = accountNumber;
        }
    }
}
