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

        //This method is the preferable because not only does it check for nulls but also checks wether obj is in the DB
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

        //this method will not search for the obj in the DB, however if the obj has already been searched for in the DB then use this method
        //eg the calling first or default in the controller
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
        public bool UpdateWithMapping(int id, Album album)
        {
            //This should be done on the end point so that we dont need to find the object twice
            var albumObj = _ctx.Album.FirstOrDefault(a => a.AlbumID == id);

            if (albumObj == null)
            {
                return false;
            }

            albumObj.AlbumName = album.AlbumName;
            albumObj.AlbumPrice = album.AlbumPrice;

            //Dont Need to call SaveChanges because it is called in the controller
            //_ctx.SaveChanges();

            return true;
        }
    }
}
