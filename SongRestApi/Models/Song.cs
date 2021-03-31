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
        public int SongID { get; set; }
        [Required]
        [Column("SongName", TypeName = "nvarchar(100)")]
        [MaxLength(100, ErrorMessage = "The Song name can't be greater than 100 characters")]
        public string SongName { get; set; }
        [Required]
        [Column("SongDuration", TypeName = "decimal(4,2)")]
        public decimal SongDuration { get; set; }

        //FK to the parent table
        [Column("AlbumId", TypeName = "int")]
        public int AlbumID { get; set; }

        //Navigation prop
        public virtual Album Album { get; set; }
    }
}
