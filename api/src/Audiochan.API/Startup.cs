using System.Reflection;
using System.Text.Json;
using Audiochan.API.Extensions.ConfigurationExtensions;
using Audiochan.API.Middlewares;
using Audiochan.API.Middlewares.Pipelines;
using Audiochan.API.Services;
using Audiochan.Core.Services;
using Audiochan.Core.Settings;
using Audiochan.Infrastructure;
using Audiochan.Infrastructure.Storage.Options;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Audiochan.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AmazonS3Settings>(Configuration.GetSection(nameof(AmazonS3Settings)));
            services.Configure<MediaStorageSettings>(Configuration.GetSection(nameof(MediaStorageSettings)));
            services.Configure<JwtSettings>(Configuration.GetSection(nameof(JwtSettings)));
            services.Configure<IdentitySettings>(Configuration.GetSection(nameof(IdentitySettings)));

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = false
            };

            services
                .AddMemoryCache()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(DbTransactionPipelineBehavior<,>))
                .AddInfrastructure(Configuration, Environment.IsDevelopment())
                .Configure<JsonSerializerOptions>(options =>
                {
                    options.IgnoreNullValues = jsonSerializerOptions.IgnoreNullValues;
                    options.PropertyNamingPolicy = jsonSerializerOptions.PropertyNamingPolicy;
                    options.PropertyNameCaseInsensitive = jsonSerializerOptions.PropertyNameCaseInsensitive;
                })
                .ConfigureIdentity(Configuration)
                .ConfigureAuthentication(Configuration)
                .ConfigureAuthorization()
                .AddHttpContextAccessor()
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .ConfigureControllers(jsonSerializerOptions)
                .ConfigureRouting()
                .ConfigureRateLimiting(Configuration)
                .ConfigureCors()
                .ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorsConfig();
            app.UseRateLimiting();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwaggerConfig();
        }
    }
}