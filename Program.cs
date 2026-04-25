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
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace WebAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //CreateHostBuilder(args).Build().Run();
      var builder = WebApplication.CreateBuilder(args);
      var services = builder.Services;

      // Convert JSON from Camel Case to Pascal Case
      services.AddControllers().AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.PropertyNamingPolicy =
            JsonNamingPolicy.CamelCase;
      });

      services.AddDbContext<SofiLoveHoleDbContext>(options =>
      {
        options.UseSqlServer(
          builder.Configuration.GetConnectionString("DefaultConnection")
        );
      });

      services.AddControllers();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
      });
      //builder.Services.AddOpenApi();

      var app = builder.Build();
      app.UseHttpsRedirection();
      app.UseAuthorization();
      app.UseCors();
      app.MapControllers();
      app.Run();
    }

    // public static IHostBuilder CreateHostBuilder(string[] args) =>
    //     Host.CreateDefaultBuilder(args)
    //         .ConfigureWebHostDefaults(webBuilder =>
    //         {
    //             webBuilder.UseStartup<Startup>();
    //         });   
  }
}
