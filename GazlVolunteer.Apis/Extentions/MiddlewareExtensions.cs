namespace GazlVolunteer.Apis.Extentions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GazlVolunteer.Apis v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("Open");
            app.MapControllers();
        }
    }
}
