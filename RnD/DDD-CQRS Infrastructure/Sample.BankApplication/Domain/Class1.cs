using Common.Infrastructure.EventSourcing.EventStore.Aggregate;
using Common.Infrastructure.EventSourcing.EventStore.Domain;
using Common.Infrastructure.EventSourcing.EventStore.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Domain
{
    public class AccountName
    {
        public string Name { get; private set; }

        public AccountName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    public class AccountNumber
    {
        public string Number { get; private set; }

        public AccountNumber(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
    public class Balance
    {
        private readonly Amount _amount;

        public Balance()
        {
            _amount = new Amount(0);
        }

        private Balance(decimal decimalAmount)
        {
            _amount = new Amount(decimalAmount);
        }

        public Balance Withdrawl(Amount amount)
        {
            return new Balance(_amount.Substract(amount));
        }

        public Balance Deposite(Amount amount)
        {
            return new Balance(_amount.Add(amount));
        }

        public bool WithdrawlWillResultInNegativeBalance(Amount amount)
        {
            return new Amount(_amount).Substract(amount).IsNegative();
        }

        public static implicit operator decimal(Balance balance)
        {
            return balance._amount;
        }

        public static implicit operator Balance(decimal decimalAmount)
        {
            return new Balance(decimalAmount);
        }
    }
    public class Amount
    {
        private readonly decimal _decimalAmount;

        public Amount(decimal decimalAmount)
        {
            _decimalAmount = decimalAmount;
        }

        public Amount Substract(Amount amount)
        {
            var newDecimalAmount = _decimalAmount - amount._decimalAmount;
            return new Amount(newDecimalAmount);
        }

        public Amount Add(Amount amount)
        {
            var newDecimalAmount = _decimalAmount + amount._decimalAmount;
            return new Amount(newDecimalAmount);
        }

        public bool IsNegative()
        {
            return _decimalAmount < 0;
        }

        public static implicit operator decimal(Amount amount)
        {
            return amount._decimalAmount;
        }

        public static implicit operator Amount(decimal decimalAmount)
        {
            return new Amount(decimalAmount);
        }
    }
    public abstract class Ledger
    {
        public Amount Amount { get; private set; }
        public AccountNumber Account { get; private set; }

        protected Ledger(Amount amount, AccountNumber account)
        {
            Amount = amount;
            Account = account;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", GetType().Name, Account.Number, (decimal)Amount);
        }
    }

    public class CreditMutation : Ledger
    {
        public CreditMutation(Amount amount, AccountNumber account) : base(amount, account) { }
    }

    public class DebitMutation : Ledger
    {
        public DebitMutation(Amount amount, AccountNumber account) : base(amount, account) { }
    }

    public class CreditTransfer : Ledger
    {
        public CreditTransfer(Amount amount, AccountNumber account) : base(amount, account) { }
    }

    public class DebitTransfer : Ledger
    {
        public DebitTransfer(Amount amount, AccountNumber account) : base(amount, account) { }
    }

    public class DebitTransferFailed : Ledger
    {
        public DebitTransferFailed(Amount amount, AccountNumber account) : base(amount, account) { }
    }

    public class ActiveAccount : BaseAggregateRoot<IDomainEvent>, IOrginator
    {
        private Guid _clientId;
        private AccountName _accountName;
        private AccountNumber _accountNumber;
        private Balance _balance;
        private readonly List<Ledger> _ledgers;
        private bool _closed;

        public ActiveAccount()
        {
            Id = Guid.Empty;
            Version = 0;
            EventVersion = 0;
            _accountName = new AccountName(string.Empty);
            _accountNumber = new AccountNumber(string.Empty);
            _balance = new Balance();
            _ledgers = new List<Ledger>();
            _closed = false;

            registerEvents();
        }

        private ActiveAccount(Guid clientId, string accountName)
            : this()
        {
            var accountNumber = SystemDateTime.Now().Ticks.ToString();
            Apply(new AccountOpenedEvent(Guid.NewGuid(), clientId, accountName, accountNumber));
        }

        public static ActiveAccount CreateNew(Guid clientId, string accountName)
        {
            return new ActiveAccount(clientId, accountName);
        }

        public void ChangeAccountName(AccountName accountName)
        {
            Guard();

            Apply(new AccountNameChangedEvent(accountName.Name));
        }

        public ClosedAccount Close()
        {
            Guard();

            IsAccountBalanceZero();

            var closedAccount = ClosedAccount.CreateNew(Id, _clientId, _ledgers, _accountName, _accountNumber);
            Apply(new AccountClosedEvent());
            return closedAccount;
        }

        public void Withdrawl(Amount amount)
        {
            Guard();

            IsBalanceHighEnough(amount);

            var newBalance = _balance.Withdrawl(amount);

            Apply(new CashWithdrawnEvent(newBalance, amount));
        }

        public void Deposite(Amount amount)
        {
            Guard();

            var newBalance = _balance.Deposite(amount);

            Apply(new CashDepositedEvent(newBalance, amount));
        }

        public void ReceiveTransferFrom(AccountNumber sourceAccountNumber, Amount amount)
        {
            Guard();

            var newBalance = _balance.Deposite(amount);

            Apply(new MoneyTransferReceivedEvent(newBalance, amount, sourceAccountNumber.Number, _accountNumber.Number));
        }

        public void SendTransferTo(AccountNumber targetAccountNumber, Amount amount)
        {
            Guard();

            IsBalanceHighEnough(amount);

            var newBalance = _balance.Withdrawl(amount);

            Apply(new MoneyTransferSendEvent(newBalance, amount, _accountNumber.Number, targetAccountNumber.Number));
        }

        public void PreviousTransferFailed(AccountNumber accountNumber, Amount amount)
        {
            Guard();

            var newBalance = _balance.Deposite(amount);

            Apply(new MoneyTransferFailedEvent(newBalance, amount, accountNumber.Number));
        }

        private void Guard()
        {
            IsAccountNotCreated();
            IsAccountClosed();
        }

        private void IsAccountNotCreated()
        {
            if (Id == Guid.Empty)
                throw new NonExitsingAccountException("The ActiveAcount is not created and no opperations can be executed on it");
        }

        private void IsAccountClosed()
        {
            if (_closed)
                throw new ClosedAccountException("The ActiveAcount is closed and no opperations can be executed on it");
        }

        private void IsBalanceHighEnough(Amount amount)
        {
            if (_balance.WithdrawlWillResultInNegativeBalance(amount))
                throw new AccountBalanceToLowException(string.Format("The amount {0:C} is larger than your current balance {1:C}", (decimal)amount, (decimal)_balance));
        }

        private void IsAccountBalanceZero()
        {
            if (_balance != 0.0M)
                throw new AccountBalanceNotZeroException(string.Format("The current balance is {0:C} this must first be transfered to an other account", (decimal)_balance));
        }

        IMemento IOrginator.CreateMemento()
        {
            return new ActiveAccountMemento(Id, Version, _clientId, _accountName.Name, _accountNumber.Number, _balance, _ledgers, _closed);
        }

        void IOrginator.SetMemento(IMemento memento)
        {
            var activeAccountMemento = (ActiveAccountMemento)memento;
            Id = activeAccountMemento.Id;
            Version = activeAccountMemento.Version;
            _clientId = activeAccountMemento.ClientId;
            _accountName = new AccountName(activeAccountMemento.AccountName);
            _accountNumber = new AccountNumber(activeAccountMemento.AccountNumber);
            _balance = activeAccountMemento.Balance;
            _closed = activeAccountMemento.Closed;

            foreach (var ledger in activeAccountMemento.Ledgers)
            {
                var split = ledger.Value.Split(new[] { '|' });
                var amount = new Amount(Convert.ToDecimal(split[0]));
                var account = new AccountNumber(split[1]);
                _ledgers.Add(InstantiateClassFromStringValue<Ledger>(ledger.Key, amount, account));
            }
        }

        private TRequestedType InstantiateClassFromStringValue<TRequestedType>(string className, params object[] constructorArguments)
        {
            var classType = GetType()
                .Assembly
                .GetExportedTypes()
                .Where(x => x.Name == className)
                .FirstOrDefault();

            return (TRequestedType)Activator.CreateInstance(classType, constructorArguments);
        }

        private void registerEvents()
        {
            RegisterEvent<AccountOpenedEvent>(onAccountCreated);
            RegisterEvent<AccountClosedEvent>(onAccountClosed);
            RegisterEvent<CashWithdrawnEvent>(onWithdrawl);
            RegisterEvent<CashDepositedEvent>(onDeposite);
            RegisterEvent<AccountNameChangedEvent>(onAccountNameGotChanged);
            RegisterEvent<MoneyTransferReceivedEvent>(onMoneyTransferedFromAnOtherAccount);
            RegisterEvent<MoneyTransferSendEvent>(onMoneyTransferedToAnOtherAccount);
            RegisterEvent<MoneyTransferFailedEvent>(onMoneyTransferFailed);
        }

        private void onMoneyTransferFailed(MoneyTransferFailedEvent moneyTransferFailedEvent)
        {
            _ledgers.Add(new DebitTransferFailed(moneyTransferFailedEvent.Amount, new AccountNumber(string.Empty)));
            _balance = moneyTransferFailedEvent.Balance;
        }

        private void onMoneyTransferedToAnOtherAccount(MoneyTransferSendEvent moneyTransferSendEvent)
        {
            _ledgers.Add(new CreditTransfer(moneyTransferSendEvent.Amount, new AccountNumber(moneyTransferSendEvent.TargetAccount)));
            _balance = moneyTransferSendEvent.Balance;
        }

        private void onMoneyTransferedFromAnOtherAccount(MoneyTransferReceivedEvent moneyTransferReceivedEvent)
        {
            _ledgers.Add(new DebitTransfer(moneyTransferReceivedEvent.Amount, new AccountNumber(moneyTransferReceivedEvent.TargetAccount)));
            _balance = moneyTransferReceivedEvent.Balance;
        }

        private void onAccountNameGotChanged(AccountNameChangedEvent accountNameChangedEvent)
        {
            _accountName = new AccountName(accountNameChangedEvent.AccountName);
        }

        private void onAccountCreated(AccountOpenedEvent accountOpenedEvent)
        {
            Id = accountOpenedEvent.AccountId;
            _clientId = accountOpenedEvent.ClientId;
            _accountName = new AccountName(accountOpenedEvent.AccountName);
            _accountNumber = new AccountNumber(accountOpenedEvent.AccountNumber);
        }

        private void onAccountClosed(AccountClosedEvent accountClosedEvent)
        {
            _closed = true;
        }

        private void onWithdrawl(CashWithdrawnEvent cashWithdrawnEvent)
        {
            _ledgers.Add(new DebitMutation(cashWithdrawnEvent.Amount, new AccountNumber(string.Empty)));
            _balance = cashWithdrawnEvent.Balance;
        }

        private void onDeposite(CashDepositedEvent cashDepositedEvent)
        {
            _ledgers.Add(new CreditMutation(cashDepositedEvent.Amount, new AccountNumber(string.Empty)));
            _balance = cashDepositedEvent.Balance;
        }
    }
}
