using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Models
{
    [Table("Songs", Schema = "Music")]
    public class Song
    {
        [Key]
        [Column("SongId", TypeName = "int")]
        [JsonProperty("Id")]
        public int SongID { get; set; }
        [Required]
        [Column("SongName", TypeName = "nvarchar(100)")]
        [MaxLength(100, ErrorMessage = "The Song name can't be greater than 100 characters")]
        [JsonProperty("Name")]
        public string SongName { get; set; }
        [Required]
        [Column("SongDuration", TypeName = "decimal(4,2)")]
        [JsonProperty("Duration")]
        public decimal SongDuration { get; set; }

        //FK to the parent table
        [Column("AlbumId", TypeName = "int")]
        [ForeignKey("Album")]
        public int AlbumID { get; set; }

        //This attribute is needed so dont get self referention loop err when lazy loading (.include())
        [JsonIgnore]
        public virtual Album Album { get; set; }
    }
}
