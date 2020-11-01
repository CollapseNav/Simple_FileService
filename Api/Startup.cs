using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Common;
using Api.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private FileServConfig FileConfig;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            FileConfig = Configuration.GetSection("FileService").Get<FileServConfig>();
            services.AddDbContext<FileDbContext>(option =>
            {
                option.UseSqlite(Configuration.GetConnectionString("Sqlite"));
                // option.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                option.AddPolicy("notany", builder =>
                {
                    builder.WithOrigins("http://*****,https://*****").AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddSingleton(FileConfig);
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("file", new OpenApiInfo { Title = "FileService API", Version = "file" });
                    options.DocInclusionPredicate((docName, description) => true);
                    DirectoryInfo d = new DirectoryInfo(AppContext.BaseDirectory);
                    FileInfo[] files = d.GetFiles("*.xml");
                    foreach (var item in files)
                    {
                        options.IncludeXmlComments(item.FullName, true);
                    }
                    // options.OperationFilter<AddHeader>();
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // app.UseHttpsRedirection();

            // app.UseCors("any");
            app.UseCors();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(FileConfig.FileStore),
                RequestPath = new PathString(FileConfig.FileServPath)
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(FileConfig.FileStore),
                RequestPath = new PathString(FileConfig.FileServPath)
            });


            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/file/swagger.json", "FileService API");
            });

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
