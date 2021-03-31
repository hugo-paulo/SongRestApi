using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Controllers.V1.DTOS.Requests
{
    public class AlbumUpdateDTO
    {
        //(Normally) Update like create doesnt need id, because we not updating the id
        //The reason we have a update class thats identical to create and not leveraging create is that we can change how we update with out changing create
        //If we inherit we can leverage the AlbumCreateDTO, but would be able to call getters and setter of the parent class even though these methods should not be called in this class
        public int AlbumID { get; set; } //This case need the id because the obj we create in the controller has an id this the mapping need is (see line 103 and 118)
        public string AlbumName { get; set; }
        public decimal AlbumPrice { get; set; }


    }
}
