namespace Common.Infrastructure.EventSourcing.EventStore
{
    public interface ITransactional
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
