using AutoMapper;
using SongRestApi.Controllers.V1.DTOS.Requests;
using SongRestApi.Controllers.V1.DTOS.Responses;
using SongRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Profiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            //Creates a map between source object and destination object
            //Maps these two objects
            //Mapping for reads
            CreateMap<Album, AlbumReadDTO>();
            //Mapping for writes (source and target are inverted from writes to reads)
            CreateMap<AlbumCreateDTO, Album>();
            CreateMap<AlbumUpdateDTO, Album>();
            CreateMap<Album, AlbumUpdateDTO>();
        }
    }
}
