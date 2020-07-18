using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Legomaster.Repos;
using Legomaster.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySqlConnector;


namespace lego
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Add USER AUTH THROUGH JWT
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
        options.Audience = Configuration["Auth0:Audience"];
      });

      services.AddCors(options =>
      {
        options.AddPolicy("CorsDevPolicy", builder =>
              {
                builder
                 .WithOrigins(new string[] { "http://localhost:8080", "http://localhost:8081" })
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                .AllowCredentials();
              });
      });

      services.AddControllers();
      services.AddScoped<IDbConnection>(x => CreateDBContext());
      services.AddTransient<LegoService>();
      services.AddTransient<LegoRepo>();
      services.AddTransient<KitService>();
      services.AddTransient<KitRepo>();
      services.AddTransient<LegoKitService>();
      services.AddTransient<LegoKitRepo>();
    }
    private IDbConnection CreateDBContext()
    {
      var _connectionString = Configuration.GetSection("db").GetValue<string>("gearhost");
      var connection = new MySqlConnection(_connectionString);
      connection.Open();
      return connection;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
