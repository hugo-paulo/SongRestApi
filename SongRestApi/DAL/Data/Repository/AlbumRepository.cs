using SongRestApi.DAL.Data.Repository.IRepository;
using SongRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.DAL.Data.Repository
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        private readonly ApplicationDbContext _ctx;

        public AlbumRepository(ApplicationDbContext ctx): base(ctx)
        {
            _ctx = ctx;
        }

        //This is not optimised 
        public bool Update(Album album)
        {
            var albumObj = _ctx.Album.FirstOrDefault(a => a.AlbumID == album.AlbumID);

            if (albumObj == null)
            {
                return false;
            }

            albumObj.AlbumName = album.AlbumName;
            albumObj.AlbumPrice = album.AlbumPrice;

            //The SaveChanges method is best called from the controller so that the UnitOfWork becomes decoupled from the repository
            //(thus have only one unit of work and not one for each repository)
            //_ctx.SaveChanges(); 

            return true;
        }

        //this method will not search for the album instace as the above method but seems redudent (but above may be safer against nullref ?)
        public bool BasicUpdate(Album album)
        {
            if (album == null)
            {
                return false;
            }

            _dbContext.Update(album);
            return true;

        }

        //?Need to refactor, maybe have the update and check null by methods seperaete that is called from here?
        //Don't need to use the method with an id argument/ parameter
        public bool Update(int id, Album album)
        {
            //This should be done on the end point so that we dont need to find the object twice
            var albumObj = _ctx.Album.FirstOrDefault(a => a.AlbumID == id);

            if (albumObj == null)
            {
                return false;
            }

            albumObj.AlbumName = album.AlbumName;
            albumObj.AlbumPrice = album.AlbumPrice;

            _ctx.SaveChanges();

            return true;
        }
    }
}
