using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Autofac;
using FluentValidation;
using FluentValidation.AspNetCore;
using NHibernate.Cfg;
using test_api.Application.UserApplication.Commands.SaveUserCommand;
using test_api.Converters;

namespace test_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddMediatR(typeof(Startup).GetTypeInfo().Assembly)
                .AddResponseCompression(options =>
                {
                    options.Providers.Add<BrotliCompressionProvider>();
                    options.MimeTypes =
                        ResponseCompressionDefaults.MimeTypes.Concat(
                            new[]
                            {
                                "application/pdf",
                                "application/json;"
                            });
                    options.EnableForHttps = true;
                })
                .AddMvcCore()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.Converters.Add(new ByteArrayConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            serviceCollection
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UniqueUserEmailValidator>());

            serviceCollection.AddSwaggerDocument(settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Example API";
                    document.Info.Description = "REST API for example.";
                };
            });

            serviceCollection.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            serviceCollection
                .AddHttpContextAccessor()
               
                .AddOptions()
                .AddLogging()
                .AddMemoryCache()
                .AddOpenApiDocument(conf =>
                {
                    conf.SchemaType = NJsonSchema.SchemaType.OpenApi3;
                    conf.DocumentName = "OpenApi";
                    conf.RequireParametersWithoutDefault = true;
                })
                .AddCors(x => x.AddPolicy("CORS", builder => builder.AllowAnyOrigin()
                                              .AllowAnyMethod()
                                              .AllowAnyHeader()))
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseCors("CORS");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
