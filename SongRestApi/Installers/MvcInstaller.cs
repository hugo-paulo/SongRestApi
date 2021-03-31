using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SongRestApi.Installers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

        }
    }
}
