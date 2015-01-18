namespace Common.Infrastructure.EventSourcing.EventStore
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
