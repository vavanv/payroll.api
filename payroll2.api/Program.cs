using System;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Payroll2.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).UseDefaultServiceProvider(options => options.ValidateScopes = false).Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}