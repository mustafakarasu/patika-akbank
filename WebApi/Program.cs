using WebApi.Middlewares;
using WebApi.Repositories;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Veri kaynaðýný Singleton olarak service'lere ekliyoruz.
            builder.Services.AddSingleton<DataSource>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

           builder.Logging.AddConsole();

            builder.Services.AddControllers()
                .AddNewtonsoftJson();
                
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment() )
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.CustomException();

            app.UseHttpsRedirection();

            app.CustomLogging(app.Logger);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}