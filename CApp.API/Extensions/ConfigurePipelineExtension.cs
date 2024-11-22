namespace CApp.API.Extensions
{
    public static class ConfigurePipelineExtension
    {
        public static IApplicationBuilder UseConfigurePipelineExt(this WebApplication app) //IApplicationBuilder
        {
            app.UseExceptionHandler(x => { });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExt();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            return app;
        }
    }
}
