using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.DAL.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IAlbumRepository Album { get; }
        ISongRepository Song { get; }

        Task<bool> SaveAsync();
    }
}
