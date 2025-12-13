namespace TaskManagement.Api.Common
{
    public static class Build
    {
        //add cors configuration method
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var backendUrl = configuration["Cors:BackendUrl"] ?? string.Empty;
            var frontendUrl = configuration["Cors:FrontendUrl"] ?? string.Empty;

            services.AddCors(options =>
            {
                options.AddPolicy(ApiConfiguration.CorsPolicyName,
                    builder => builder.WithOrigins([backendUrl, frontendUrl])
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
            return services;
        }
    }
}
