namespace BookStorage.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            return services.AddSwaggerGen();
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}