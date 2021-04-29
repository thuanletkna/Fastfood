using KetnoiCSDL.EF;

namespace KetnoiCSDL.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private BunyStoreDbContext dbContext;

        public BunyStoreDbContext Init()
        {
            return dbContext ?? (dbContext = new BunyStoreDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}