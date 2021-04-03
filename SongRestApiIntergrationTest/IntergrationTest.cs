using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SongRestApi;
using SongRestApi.Contracts.V1;
using SongRestApi.Controllers.V1.DTOS.Requests;
using SongRestApi.Controllers.V1.DTOS.Responses;
using SongRestApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SongRestApiIntergrationTest
{
    public class IntergrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntergrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        //services.RemoveAll(typeof(ApplicationDbContext)); //?why is the db not being removed and thus using db for testing?
                        //remove the local database 
                        var dbContext = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
                            );

                        if (dbContext != null)
                        {
                            services.Remove(dbContext);
                        }

                        services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("TestDb"); }); 
                        //need to install Microsoft.EntityFrameworkCore.InMemory to use UseInMemoryDatabase() method
                    });
                });

            TestClient = appFactory.CreateClient();
        }

        //Method called in the AlbumControllerTest when testing the creating of albums
        protected async Task<AlbumReadDTO> CreateAlbumAsync(AlbumCreateDTO albumCreateRequest)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.album.CreateAlbum, albumCreateRequest);
            return await response.Content.ReadAsAsync<AlbumReadDTO>();
        }        
    }
}
