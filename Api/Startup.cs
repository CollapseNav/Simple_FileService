using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http.Features;
using Api.Model;
using Api.Common;
using Microsoft.EntityFrameworkCore;

namespace Api
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
            services.AddControllers();
            var FileConfig = Configuration.GetSection("FileService").Get<FileServConfig>();
            FileConfig.CheckAuto();

            // 添加 dbcontext 根据自己的情况可以换成其他的 数据库
            services.AddDbContext<FileDbContext>(option =>
            {
                var db = Configuration.GetSection("DB").Get<string>();
                string constr = Configuration.GetConnectionString("Default");
                switch (db)
                {
                    case "sqlite":
                        {
                            option.UseSqlite(constr);
                            break;
                        }
                    case "mssql":
                        {
                            option.UseSqlServer(constr);
                            break;
                        }
                    default:
                        {
                            option.UseSqlite(constr);
                            break;
                        }
                }
            });
            // 一般来说像文件服务这种东西 需要添加跨域设置
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                string cors = Configuration.GetSection("Cors").Get<string>();
                if (!string.IsNullOrEmpty(cors))
                {
                    option.AddPolicy("cors", builder =>
                    {
                        builder
                        .WithOrigins(cors.Split(","))
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
                }
            });

            // 限制文件(body)大小
            services.Configure<FormOptions>(option =>
            {
                option.MultipartBodyLengthLimit = FileConfig.MaxSize;
            });
            // 注册 配置
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

                    // // 可以在header中添加 全局的 token
                    // options.AddSecurityDefinition("token", new OpenApiSecurityScheme
                    // {
                    //     Description = "JWT授权(数据将在请求头中进行传输) 在下方输入Bearer {token} 即可，注意中间有空格",
                    //     Name = "Authorization",
                    //     In = ParameterLocation.Header,
                    //     Type = SecuritySchemeType.ApiKey
                    // });
                    // options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    //     {
                    //         new OpenApiSecurityScheme{
                    //             Reference = new OpenApiReference{
                    //                 Id = "token",
                    //                 Type = ReferenceType.SecurityScheme
                    //             }
                    //         },
                    //         Array.Empty<string>()
                    //     }
                    // });
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
            if (!string.IsNullOrEmpty(Configuration.GetSection("Cors").Get<string>()))
                app.UseCors("cors");
            else
                app.UseCors();

            var FileConfig = Configuration.GetSection("FileService").Get<FileServConfig>();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(FileConfig.FileStore),
                RequestPath = new PathString(FileConfig.ServeMapPath),
                ServeUnknownFileTypes = FileConfig.UseUnknowFiles,
            });

            if (FileConfig.UseDirectoryBrowser)
            {
                app.UseDirectoryBrowser(new DirectoryBrowserOptions
                {
                    FileProvider = new PhysicalFileProvider(FileConfig.FileStore),
                    RequestPath = new PathString(FileConfig.ServeMapPath),
                });
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/file/swagger.json", "FileService API");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
