using E_Trade.Core.UnitOfWorks;

namespace E_Trade.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ETradeDbContext _context;

        public UnitOfWork(ETradeDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
