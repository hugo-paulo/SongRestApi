using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SongRestApi.Contracts.V1;
using SongRestApi.Controllers.V1.DTOS.Requests;
using SongRestApi.Controllers.V1.DTOS.Responses;
using SongRestApi.DAL.Data.Repository.IRepository;
using SongRestApi.Models;

namespace SongRestApi.Controllers.V1
{
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IUnitOfWork _uw;
        private readonly IMapper _mapper;

        //The difference between inheriting Controller and ControllerBase is ControllerBase is used with api because it has no return views unlike the former

        public AlbumController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uw = unitOfWork;
            _mapper = mapper;
        }

        //GET api/v1/album
        //[HttpGet("api/v1/album")] //cumbersome and not cndusive to versioning
        [HttpGet(ApiRoutes.album.GetAllAlbums)] //We getting the route string from the Apiroutes class
        //public ActionResult<IEnumerable<Album>> GetAllAlbums()
        public async Task<ActionResult<IEnumerable<AlbumReadDTO>>> GetAllAlbums()
        {
            var albumData = await _uw.Album.GetAllAsync();

            if (albumData == null)
            {
                return NotFound();
            }

            //return OK(albumData);
            return Ok(_mapper.Map<IEnumerable<AlbumReadDTO>>(albumData));
        }

        //GET api/v1/album/1
        [HttpGet(ApiRoutes.album.GetSingleAlbum)]
        public async Task<ActionResult<AlbumReadDTO>> GetSingleAlbum([FromRoute] int id)
        {
            //var albumData = _uw.Album.GetSingle(id);
            var albumData = await _uw.Album.GetFirstOrDefaultAsync(a => a.AlbumID == id);

            if (albumData == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AlbumReadDTO>(albumData));
        }

        //Post  api/v1/album
        [HttpPost(ApiRoutes.album.CreateAlbum)]
        public async Task<ActionResult<AlbumReadDTO>> CreateAlbum([FromBody] AlbumCreateDTO albumCreateDto)
        {
            var albumData = _mapper.Map<Album>(albumCreateDto);
            await _uw.Album.AddItemAsync(albumData); //?need to change the additem method to bool and check the itemAdded if and return 404?

            var saved = await _uw.SaveAsync(); //need the save or the location header will be null

            if (!saved)
            {
                string saveErrMessage = "Something went wrong while saving to the database!";
                return Problem(saveErrMessage);
            }

            /*? change this to the les jackosn version ?*/
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.album.GetSingleAlbum.Replace("{id}", albumData.AlbumID.ToString());

            var albumReadDTO = _mapper.Map<AlbumReadDTO>(albumData);

            //return Created(locationUrl, album);
            var locationHeader = Created(locationUrl, albumReadDTO);
            return locationHeader;
        }

        //find out how to createAlbum  will child tables, if they are included in curret form we get err 500

        //For updating there are 2 aproaches full and partial(full uses PUT verb and partial uses PATCH verb), seems PATCH is favoured
        //PUT api/v1/album/{id}
        [HttpPut(ApiRoutes.album.UpdateAlbum)]
        public async Task<ActionResult> UpdateAlbum([FromRoute] int id, [FromBody] AlbumUpdateDTO albumUpdateDto)
        {
            //needs the id of which obj and the actual obj (? temp need to implement DTO mapping)
            //?Dont need this been extracted to the Update method?

            /** This method would need to query the db to return the data to the client **/

            //var isUpdated = _uw.Album.Update(id, album);

            //if (!isUpdated)
            //{
            //    return NotFound();
            //}
            ////When calling this method theres no need to perform null checks because its done in the Update method
            //var albumData = _uw.Album.GetSingle(id);
            ////For async code 202 is used to state request has been accpeted for processing but not commited, eg return StatusCode(202, albumData)
            //return Ok(albumData);

            /** This method will create an instance of the album and return the data from memory **/

            //Create a new object and map the request parameters to this object, 
            //This is redundent we can call the UpdateWithMappings method which will do the mapping  
            var albumDto = new AlbumUpdateDTO
            {
                AlbumID = id,
                AlbumName = albumUpdateDto.AlbumName,
                AlbumPrice = albumUpdateDto.AlbumPrice
            };

            var albumData = _mapper.Map<Album>(albumDto);

            //Unlike the previous example we only need to pass the obj and not the id too
            var isUpdated = _uw.Album.Update(albumData); //because we not checking
            //use below method only when variable albumDto is not created (will cause redudency), and dont forget to return variable albumUpdateDto
            //var isUpdated = _uw.Album.UpdateWithMapping(id, albumUpdateDto);

            if (!isUpdated)
            {
                return NotFound();
            }

            //_mapper.Map(AlbumUpdateDTO, albumData); //this is different to the create mapping
            var saved = await _uw.SaveAsync();

            //This need to be test dont know if works, this seems to get hit on success returns 200 writes to DB but makes the "return OK" un-used
            //if (_uw.SaveAsync() != null && !_uw.SaveAsync().IsCompleted)
            //{
            //    return StatusCode(202, albumDto);
            //}

            if (!saved)
            {
                string saveErrMessage = "Something went wrong while saving to the database!";
                return Problem(saveErrMessage);
            }

            //how the dto should work
            return Ok(albumDto); //argument/ parameter optional
            //could also use NoContent() which returns 204 (can use for PUT -> update and DELETE, PATCH -> update is differnent)

        }

        //?Add a patch update here see les jack vid?
        /*
         * A patch begins as an array of json objects, and has a Transaction behavior
         *To perform patch need the following packages microsoft.aspnetcore.jsonpatch 
         *and microsoft.aspnetcore.mvc.newtonsoftjson (note! cant use v5 here, keep v == project asp v)
        */
        //PATCH api/v1/album/{id}
        [HttpPatch(ApiRoutes.album.PatchAlbum)]
        public async Task<ActionResult> PatchUpdateAlbum(int id, JsonPatchDocument<AlbumUpdateDTO> patchJsonDoc) //Must be of type JsonPatchDocument
        {
            var albumData = await _uw.Album.GetSingleAsync(id);

            if (albumData == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<AlbumUpdateDTO>(albumData);
            //This is the newtonsoft json method
            patchJsonDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, albumData);

            _uw.Album.Update(albumData);

            var saved =  await _uw.SaveAsync();

            if (!saved)
            {
                string saveErrMessage = "Something went wrong while saving to the database!";
                return Problem(saveErrMessage);
            }

            return NoContent();
        }

        //The delete doesnt use DTO's
        ////POST api/v1/album/{id}
        [HttpDelete(ApiRoutes.album.DeleteAlbum)]
        public async Task<ActionResult> DeleteAlbum([FromRoute] int id)
        {
            var isDeleted = _uw.Album.Remove(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            var saved = await _uw.SaveAsync(); //dont forget obj will not be reomved from the database

            if (!saved)
            {
                string saveErrMessage = "Something went wrong while saving to the database!";
                return Problem(saveErrMessage);
            }

            //returns 204. but can also use Ok(), but former is better
            return NoContent();
        }

        //Note! with Rest api the the DTO will act like View Models in a MVC (thus we can customise what models we send to the user)

        //nick chapsas vid 8

        //les jackson tc 3:18:26

        //Also add a GET that will return album and songs (create a Read DTO that has an Ienumrable that include the song)
    }
}
