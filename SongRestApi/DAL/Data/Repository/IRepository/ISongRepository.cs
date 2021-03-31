using SongRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.DAL.Data.Repository.IRepository
{
    public interface ISongRepository : IRepository<Song>
    {
        void Update(Song song);
    }
}
