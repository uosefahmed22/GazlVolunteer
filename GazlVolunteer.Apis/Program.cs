
using GazlVolunteer.Apis.Extentions;

namespace GazlVolunteer.Apis
{
    // Program.cs
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureServices(builder.Configuration);

            var app = builder.Build();

            app.UseAppMiddleware();

            app.Run();
        }
    }
}
