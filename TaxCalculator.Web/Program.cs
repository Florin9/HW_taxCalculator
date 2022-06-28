using TaxCalculator.Web.Extensions;

namespace TaxCalculator.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddServices();
            builder.Services.AddValidators();
            builder.Services.AddMemoryCache();

            var app = builder.Build();

            app.EnsureDbMigrationWithSeeding();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }
    }
}