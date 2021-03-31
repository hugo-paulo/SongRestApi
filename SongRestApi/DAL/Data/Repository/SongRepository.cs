using SongRestApi.DAL.Data.Repository.IRepository;
using SongRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.DAL.Data.Repository
{
    public class SongRepository : Repository<Song>, ISongRepository
    {
        private readonly ApplicationDbContext _ctx;

        public SongRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public void Update(Song song)
        {
            var songObj = _ctx.Song.FirstOrDefault(s => s.SongID == song.SongID);

            songObj.SongName = song.SongName;
            songObj.AlbumID = song.AlbumID;
            songObj.SongDuration = song.SongDuration;
        }
    }
}
