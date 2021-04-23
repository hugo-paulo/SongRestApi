using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SongRestApi.DAL.Data.Repository.IRepository;
using SongRestApi.Models;

namespace SongRestApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly IUnitOfWork _uw;

        public SongController(IUnitOfWork unitOfWork)
        {
            _uw = unitOfWork;
        }

        [HttpGet("api/v1/songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetAllSongs()
        {
            var allSongs = await _uw.Song.GetAllAsync();

            if (allSongs == null)
            {
                NotFound();
            }

            return Ok(allSongs);
        }

    }
}
