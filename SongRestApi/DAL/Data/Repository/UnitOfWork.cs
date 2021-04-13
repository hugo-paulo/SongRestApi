using SongRestApi.DAL.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.DAL.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _ctx;
        private bool disposed;

        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
            Album = new AlbumRepository(_ctx);
            Song = new SongRepository(_ctx);
        }

        public IAlbumRepository Album { get; private set; }

        public ISongRepository Song { get; private set; }

        public async Task<bool> SaveAsync()
        {
            return (await _ctx.SaveChangesAsync()) > 0;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                 _ctx.Dispose();
                disposed = true;
            }
        }

        //Place the rollback and commit when using transactions, here...
    }
}
