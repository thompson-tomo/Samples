﻿
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace FortuneTellerService
{

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseCloudHosting(5000)
                    .AddCloudFoundryConfiguration()
                    .UseStartup<Startup>()
                    .Build();

    }

}
