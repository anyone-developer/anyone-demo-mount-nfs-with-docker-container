using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ApplicationWithNfsClient
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JsonOptions>(o =>
            {
                o.SerializerOptions.WriteIndented = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var path = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "c:/mnt/exports" : "/mnt/exports";

                    await context.Response.WriteAsJsonAsync(new
                    {
                        OS = Environment.OSVersion,
                        Directory = Directory.Exists(path) ? Directory.EnumerateFileSystemEntries(path) : null,
                        Error = Directory.Exists(path) ? null : path + " Not Exist!"
                    });
                });
            });
        }
    }
}
