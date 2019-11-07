using KitchenRestService.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Fridge>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod() // not just GET and POST, but allow all methods
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml",
                    new MediaTypeHeaderValue("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json",
                    new MediaTypeHeaderValue("application/json"));
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kitchen API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseAuthorization();

            app.UseCors("AllowAngular");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
