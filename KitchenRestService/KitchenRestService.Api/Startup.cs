using System;
using KitchenRestService.Api.Filters;
using KitchenRestService.Api.Services;
using KitchenRestService.Data;
using KitchenRestService.Logic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace KitchenRestService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string providerType = Configuration["ProviderType"];
            Action<DbContextOptionsBuilder> optionsAction = providerType switch
            {
                "SqlServer" => options => options.UseSqlServer(Configuration.GetConnectionString("KitchenDb")),
                "PostgreSql" => options => options.UseNpgsql(Configuration.GetConnectionString("KitchenDb")),
                _ => throw new NotImplementedException($"Unsupported provider type \"{providerType}\"")
            };

            services.AddDbContext<KitchenContext>(optionsAction);

            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<IKitchenRepo, KitchenRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IFridgeService, FridgeService>();

            services.AddHttpClient<IAuthInfoService, AuthInfoService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200",
                                        "http://localhost:5000",
                                        "http://192.168.99.100:5000",
                                        "https://1909nickproject2angular.azurewebsites.net")
                        .AllowAnyMethod() // not just GET and POST, but allow all methods
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            });

            // this block copied from Auth0 ASP.NET Core quickstart.
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://escalonn.auth0.com/";
                options.Audience = "https://1909nickproject2api.azurewebsites.net";
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kitchen API", Version = "v1" });

                // should be "http" type with "bearer" scheme, but swagger-ui
                // doesn't handle that correctly.
                c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Bearer authentication scheme with JWT, e.g. \"Bearer eyJhbGciOiJIUzI1NiJ9.e30\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kitchen API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            // this line added based on Auth0 ASP.NET Core quickstart
            app.UseAuthorization();

            app.UseCors("AllowAngular");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
