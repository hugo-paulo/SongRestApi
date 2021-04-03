using Microsoft.AspNetCore.Mvc.Testing;
using SongRestApi;
using SongRestApi.Contracts.V1;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SongRestApiIntergrationTest
{
    public class UnitTest1
    {
        /// <summary>
        /// This will not be used its only an explanation 
        /// 
        /// Need to install microsoft.aspnetcore.mvc.testing (note version numb must match framework numb)
        //Dont forget to ref the application project
        /// 
        /// </summary>

        private HttpClient _client;

        public UnitTest1()
        {
            //This gets the StartUp class from the application project
            var appFactory = new WebApplicationFactory<Startup>();
            //This will create and instance of the server that the application will be runnning
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task Test1() //This is async of "public void Test1()"
        {
            var response = await _client.GetAsync(ApiRoutes.album.GetSingleAlbum.Replace("{id}", "1"));
        }
    }
}
