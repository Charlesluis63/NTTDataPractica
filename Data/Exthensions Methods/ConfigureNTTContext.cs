using Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exthensions_Methods
{
    
    public static class Extensions
    {
        public static void ConfigureNTTContext(this IServiceCollection services, IConfiguration config)
        {

            var connectionString = config.GetConnectionString("ConnectionStringNTT");
            //string connectionString = $"Server={config["ServerName"]};Database={config["Database"]};User Id={config["UserName"]};Password={config["Password"]};";


            services.AddDbContext<NTTContext>(o => o.UseSqlServer(connectionString));
        }
    }
    
}
