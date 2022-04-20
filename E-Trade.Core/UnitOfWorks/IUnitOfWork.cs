namespace E_Trade.Core.UnitOfWorks
{
    // Unit Of Work Design Pattern'ı uyguladığımız interface

    // IunitOfWork interface
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
