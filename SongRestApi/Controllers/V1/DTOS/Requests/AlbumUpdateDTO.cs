using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Controllers.V1.DTOS.Requests
{
    public class AlbumUpdateDTO
    {
        //(Normally) Update like create doesnt need id, because we not updating the id
        //The reason we have a update class thats identical to create and not leveraging create is that we can change how we update with out changing create
        //If we inherit we can leverage the AlbumCreateDTO, but would be able to call getters and setter of the parent class even though these methods should not be called in this class
        [Required]
        [JsonProperty("Id")]
        public int AlbumID { get; set; } //This case need the id because the obj we create in the controller has an id this the mapping need is (see line 103 and 118)
        [Required]
        [MaxLength(100, ErrorMessage = "Album name can't be greater than 100 characters")]
        [JsonProperty("Name")]
        public string AlbumName { get; set; }
        [Required]
        //[DataType(DataType.Currency)] //even though it's currency need the sanitize service
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Price value can only have decimal of two places")]
        [Range(0, 99999.99, ErrorMessage = "The price can only be 5 digits long")]
        [JsonProperty("Price")]
        public decimal AlbumPrice { get; set; }

    }
}
