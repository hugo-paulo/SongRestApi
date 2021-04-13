using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Controllers.V1.DTOS.Requests
{
    public class AlbumCreateDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Album name can't be greater than 100 characters")]
        public string AlbumName { get; set; }
        [Required]
        [DataType(DataType.Currency)]////even though it's currency need the sanitize service
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Price value can only have decimal of two places")]
        [Range(0, 99999.99, ErrorMessage = "The price can only be 5 digits long")]
        public decimal AlbumPrice { get; set; }
    }
}
