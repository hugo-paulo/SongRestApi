using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SongRestApi.DAL.Data.Repository;
using SongRestApi.DAL.Data.Repository.IRepository;
using SongRestApi.Installers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Installers
{
    public class DomainModelsInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            //This maps the iterface with the matching class, AddScoped method will give us a new instance at every request 
            //This also gives us the use of dependency injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<ISongRepository, SongRepository>();
            //add the scoped unit of work or other here...
        }
    }
}
