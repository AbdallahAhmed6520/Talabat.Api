namespace Talabat.APIs.Extensions
{
    public static class AddSwaggerMiddleware
    {
        public static WebApplication UseSwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
