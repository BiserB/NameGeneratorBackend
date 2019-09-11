using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace NG.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("https://localhost:50000")
                .UseStartup<Startup>();
    }
}
