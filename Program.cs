using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;


namespace WebAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //CreateHostBuilder(args).Build().Run();
      var builder = WebApplication.CreateBuilder(args);
      var app = builder.Build();

      var services = builder.Services;
      // Tell this project to allow CORS
      services.AddCors();
      
      // Convert JSON from Camel Case to Pascal Case
      services.AddControllers().AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.PropertyNamingPolicy =
            JsonNamingPolicy.CamelCase;
      });

    }

    // public static IHostBuilder CreateHostBuilder(string[] args) =>
    //     Host.CreateDefaultBuilder(args)
    //         .ConfigureWebHostDefaults(webBuilder =>
    //         {
    //             webBuilder.UseStartup<Startup>();
    //         });   
  }
}
