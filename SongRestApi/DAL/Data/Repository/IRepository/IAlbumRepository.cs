using SongRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.DAL.Data.Repository.IRepository
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<bool> Update(Album album);
        //Dont need this method with an id argument/parameter

        //?temp?
        bool BasicUpdate(Album album);

        Task<bool> UpdateWithMapping(int id, Album album);

        Task<List<Album>> GetAlbumsWithSongs();
    }
}
