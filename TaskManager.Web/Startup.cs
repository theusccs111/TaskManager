using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.ResponseCompression;
using SOMA.OPEX.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Web.Filters;
using TaskManager.Service.Services;
using TaskManager.Service.Interface.Persistance;
using TaskManager.Service.Configuracao;
using Microsoft.AspNetCore.Hosting;
using Task.Manager.Domain;
using Microsoft.Extensions.Hosting;
using TaskManager.Persistance.Data;

namespace TaskManager.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TaskManagerContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<GlobalExceptionHandlerMiddleware>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("pt-BR") };
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "text/plain", "application/json" });
            });

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            InjectServices(services);
            InjectRepositories(services);
            //ConfigureJWTService(services, Configuration["JwtSecurityToken:Key"]);
            ConfigureSwaggerService(services, "OPÈX");

            services.AddScoped<RequestValidationFilter>();
            services.AddAutoMapper(c => c.AddProfile<AutoMapperConfiguration>(), typeof(Startup));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });


            ConfigureLogs(services, connectionString);
            InjetandoConfiguracoes(services);
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

        }

        public static void ConfigureLogs(IServiceCollection services, string connectionString)
        {

        }

        private static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<TaskService>();
            services.AddScoped<TaskAuditService>();
        }

        private static void InjectRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }

        private void InjetandoConfiguracoes(IServiceCollection services)
        {
            services.Configure<ConnectionStringsConfiguracao>(Configuration.GetSection("ConnectionStrings"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Adicione essas linhas ao Configure para configurar localização de mensagens
            var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Use o middleware de tratamento global de exceções em ambientes de produção
                app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            }
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.UseResponseCompression();
            app.UseCors();

            app.UseRouting();  // Adicione esta linha para configurar o roteamento de endpoints

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Barbeiro");
                //c.RoutePrefix = string.Empty;
            });

            // Adicione essas linhas ao final do método Configure para lidar com erros não tratados
            app.UseExceptionHandler("/Home/Error");
            app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

        }

        public void ConfigureSwaggerService(IServiceCollection service, string apiName)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio
            // Register the Swagger generator, defining 1 or more Swagger documents
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = apiName, Version = "v1", Description = "Management System" });
                //c.AddSecurityDefinition(
                //    "Bearer",
                //    new OpenApiSecurityScheme
                //    {
                //        Description = @"JWT Authorization header using the Bearer scheme. 
                //                        Enter 'Bearer' [space] and then your token in the text input below.
                //                        Example: 'Bearer 12345abcdef'",
                //        Name = "Authorization",
                //        In = ParameterLocation.Header,
                //        Type = SecuritySchemeType.ApiKey,
                //        Scheme = "Bearer"
                //    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                        {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                      },
                      new List<string>()
                    }
                  });
            });
        }

        

        public void ConfigureCorsService(IServiceCollection services)
        {
            services.AddCors(o =>
                   o.AddPolicy(
                       "CorsPolicy",
                       builder =>
                       {
                           builder.WithOrigins(Configuration["WebApplication:AllowOrigins"].Split(';'))
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();
                       }));
        }
    }
}
