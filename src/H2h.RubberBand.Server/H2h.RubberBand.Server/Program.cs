using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace H2h.RubberBand.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //   webBuilder.UseUrls("https://*:8301", "http://*:8300");
                    webBuilder.UseStartup<Startup>();
                });
    }
}