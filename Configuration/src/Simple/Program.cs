using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Steeltoe;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Discovery.Client;

namespace Simple
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);
            builder.AddConfigServer();
            return builder.UseStartup<Startup>()
                    .Build();
        }
    }
}
