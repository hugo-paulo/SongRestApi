using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Models
{
    [Table("Albums", Schema = "Music")]
    public class Album
    {
        //Best practise is to use guid, because we get true unique identifiers (even across databases thus we can union)
        [Key]
        [Column("AlbumId", TypeName = "int")]
        public int AlbumID { get; set; }
        [Required]
        [Column("AlbumName", TypeName = "nvarchar(100)")]
        [MaxLength(100, ErrorMessage = "Album name can't be greater than 100 characters")]
        public string AlbumName { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage ="Price value can only have decimal of two places")] //the regex pattern is a digit and has 2 decimal places
        [Range(0, 99999.99, ErrorMessage = "The price can only be 5 digits long")]
        [Column("AlbumPrice", TypeName = "decimal(5,2)")]
        public decimal AlbumPrice { get; set; }

        //This for enabling concurreny in EF model first
        //RowVersion column is a standard name in concurrency, time stamp will use optimistic concurrency
        //[Timestamp]
        //public byte RowVersion { get; set; }
        //Note when using views this field should be hidden

        public virtual IEnumerable<Song> Songs { get; set; }
    }
}
