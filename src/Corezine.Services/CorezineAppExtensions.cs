using Corezine.Services.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.DependencyInjection;
using Corezine.Services.Contracts;
using Corezine.Services.Core;

namespace Corezine.Services
{
    public static class CorezineAppExtensions
    {
        public static IApplicationBuilder UseFeedback(this IApplicationBuilder builder)
        {
            
            builder.UseSession();
            builder.UseMiddleware<FeedbackMiddleware>();
            return builder;
        }

        public static IServiceCollection AddFeedback(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddScoped<IFeedbackService, FeedbackService>();
            return services;
        }
    }
}
