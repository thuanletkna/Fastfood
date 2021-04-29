using KetnoiCSDL.EF;
using System;

namespace KetnoiCSDL.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        BunyStoreDbContext Init();
    }
}