namespace Common.Infrastructure.EventSourcing.EventStore.Memento
{
    public interface IOrginator
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}
