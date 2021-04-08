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
            await SeedInMemoryDBAsync();
            
            var expectation = new List<Album> {
                new Album{AlbumID = 1, AlbumName = "Test_1", AlbumPrice = 1.99m, Songs = null},
                new Album{AlbumID = 2, AlbumName = "Test_2", AlbumPrice = 2.99m, Songs = null},
                new Album{AlbumID = 3, AlbumName = "Test_3", AlbumPrice = 1.99m, Songs = null}
            };

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.album.GetAllAlbums);
            
            //Assert
            //The Should().Be() extension methods are part of the FluentAssertion package/library we installed
            //The HttpstatusCode.OK needs the System.Net library
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var albums = await response.Content.ReadAsAsync<List<Album>>();

            //Needs the System.Net.Http.Formatting (needs Microsoft.AspNet.WebApi.Client package)
            //(await response.Content.ReadAsAsync<List<Album>>()).Should().HaveCount(albums.Count);//HaveCount(3), this is better than hardcode because we can change number of albums in memory db without breaking code
            //(await response.Content.ReadAsAsync<List<Album>>()).Should().BeSameAs(albums);
            albums.Count.Should().Be(expectation.Count);
            albums.Should().BeEquivalentTo(expectation, options => options.ComparingByValue<Album>()); //?This is giving issues, the comparision is the same?
            
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
