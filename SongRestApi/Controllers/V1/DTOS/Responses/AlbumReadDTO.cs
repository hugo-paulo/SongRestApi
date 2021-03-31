using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Controllers.V1.DTOS.Responses
{
    public class AlbumReadDTO
    {
        public string AlbumName { get; set; }
        public decimal AlbumPrice { get; set; }
    }
}
