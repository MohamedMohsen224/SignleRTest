using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SignleRTest.Context;
using SignleRTest.Hubs;

namespace SignleRTest
{
    public class Program
    {
        public  static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SignleRContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));});
            builder.Services.AddSignalR();
            var app = builder.Build();

            var Scoped = app.Services.CreateScope();
            var DataBase = Scoped.ServiceProvider;
            var Context = DataBase.GetRequiredService<SignleRContext>();
            var Logger = DataBase.GetRequiredService<ILoggerFactory>();
            try
            {
               await Context.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                var log =Logger.CreateLogger<Program>();
                log.LogError(e, "an error has been occured during apply the migration");

            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/Chat");
            });
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
