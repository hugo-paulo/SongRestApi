using Newtonsoft.Json;
using SongRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Controllers.V1.DTOS.Responses
{
    public class AlbumReadWithSongsDTO
    {
        [JsonProperty("Id")]
        public int AlbumID { get; set; }
        [JsonProperty("Name")]
        public string AlbumName { get; set; }
        [JsonProperty("Price")]
        public decimal AlbumPrice { get; set; }
        public virtual IEnumerable<Song> Songs { get; set; }
    }
}
