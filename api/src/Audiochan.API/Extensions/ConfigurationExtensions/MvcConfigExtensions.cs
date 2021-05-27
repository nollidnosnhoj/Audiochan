﻿using System.Text.Json;
using System.Text.Json.Serialization;
using Audiochan.API.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Audiochan.API.Extensions.ConfigurationExtensions
{
    public static class MvcConfigExtensions
    {
        public static IServiceCollection ConfigureControllers(this IServiceCollection services,
            JsonSerializerOptions jsonSerializerOptions)
        {
            services
                .AddControllers(configuration =>
                {
                    configuration.Filters.Add(new ProducesAttribute("application/json"));
                    configuration.Filters.Add<ValidationFilter>();
                })
                .AddJsonOptions(configuration =>
                {
                    configuration.JsonSerializerOptions.IgnoreNullValues = jsonSerializerOptions.IgnoreNullValues;
                    configuration.JsonSerializerOptions.PropertyNameCaseInsensitive =
                        jsonSerializerOptions.PropertyNameCaseInsensitive;
                    configuration.JsonSerializerOptions.PropertyNamingPolicy =
                        jsonSerializerOptions.PropertyNamingPolicy;
                    configuration.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                })
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Startup>());

            return services;
        }

        public static IServiceCollection ConfigureRouting(this IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            return services;
        }
    }
}