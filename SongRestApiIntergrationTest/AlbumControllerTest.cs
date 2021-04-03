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

namespace SongRestApiIntergrationTest
{
    public class AlbumControllerTest : IntergrationTest
    {
        [Fact]
        public async Task GetAllAlbumsTest()
        {
            //Still need to add login testing

            //Arrange
            //var albumListTest = new List<Album> { new Album { AlbumID = 1, AlbumName = "Test_1", AlbumPrice = 5.99m }, new Album { AlbumID = 2, AlbumName = "Test_2", AlbumPrice = 6.99m } };

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.album.GetAllAlbums);

            //Assert
            //The Should().Be() extension methods are part of the FluentAssertion package/library we installed
            //The HttpstatusCode.OK needs the System.Net library
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            //Needs the System.Net.Http.Formatting (needs Microsoft.AspNet.WebApi.Client package)
            (await response.Content.ReadAsAsync<List<Album>>()).Should().HaveCount(0); //?20 on local db server //?problem with this test is will break if table is updated, thus not automated?
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
