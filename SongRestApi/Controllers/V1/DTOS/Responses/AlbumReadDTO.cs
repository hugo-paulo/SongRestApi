using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Controllers.V1.DTOS.Responses
{
    public class AlbumReadDTO
    {
        //The read needs the id to be accessable because we may use it to tack the specific album we want (avoiding null ref exeptions)
        public int AlbumID { get; set; }
        
        public string AlbumName { get; set; }
        
        public decimal AlbumPrice { get; set; }
    }
}
