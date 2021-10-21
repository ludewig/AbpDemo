using System;
using System.IO;
using AbpDemo.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace AbpDemo.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = AppConfigurations.Get(Directory.GetCurrentDirectory());
            var urls = config["App:WebSiteRootAddress"].Split('|');
            var host = new WebHostBuilder()
                .UseKestrel(opt=>opt.AddServerHeader=false)
                .UseConfiguration(config)
                .UseUrls(urls)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
