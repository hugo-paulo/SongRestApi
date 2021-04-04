using SongRestApi.Contracts.V1;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using SongRestApi.Models;
using SongRestApi.Controllers.V1.DTOS.Requests;
using SongRestApi.DAL;

namespace SongRestApiIntergrationTest
{
    public class AlbumControllerTest : IntergrationTest
    {
        [Fact]
        public async Task GetAllAlbumsTest()
        {
            //Still need to add login testing

            //Arrange
            await SeedInMemoryDBAsync(); //because we using in memory database we dont need a moc repository
            //Act
            var response = await TestClient.GetAsync(ApiRoutes.album.GetAllAlbums);

            //Assert
            //The Should().Be() extension methods are part of the FluentAssertion package/library we installed
            //The HttpstatusCode.OK needs the System.Net library
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            //Needs the System.Net.Http.Formatting (needs Microsoft.AspNet.WebApi.Client package)
            (await response.Content.ReadAsAsync<List<Album>>()).Should().HaveCount(3); //?20 on local db server and 3 in the in-memory-database

        }

        [Fact]
        public async Task CreateAlbumTest()
        {
            //Arrange
            var createdAlbumObj = await CreateAlbumAsync(new AlbumCreateDTO { AlbumName = "Album Test 1", AlbumPrice = 6.88m });            

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.album.GetSingleAlbum.Replace("{id}", createdAlbumObj.AlbumID.ToString()));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedAlbumObj = await response.Content.ReadAsAsync<Album>();

            returnedAlbumObj.AlbumID.Should().Be(createdAlbumObj.AlbumID);
            returnedAlbumObj.AlbumName.Should().Be(createdAlbumObj.AlbumName);
            returnedAlbumObj.AlbumPrice.Should().Be(createdAlbumObj.AlbumPrice);
        }
    }
}
