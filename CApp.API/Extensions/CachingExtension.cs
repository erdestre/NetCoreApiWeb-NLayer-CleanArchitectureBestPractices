﻿using App.Application.Contracts.Caching;
using App.Caching;

namespace CApp.API.Extensions
{
    public static class CachingExtension
    {
        public static IServiceCollection AddCachingExt(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}
