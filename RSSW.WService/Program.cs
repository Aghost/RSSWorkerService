using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

using RSSW.Data;

namespace RSSW.WService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) => {
                    services.AddHostedService<RSSReaderService>();
                    services.AddDbContext<RSSWDbContext>(options =>
                            options.UseNpgsql(hostContext.Configuration.GetConnectionString("Default")));
                });
    }
}
