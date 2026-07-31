
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Formatters;
using WebApplication3.Mappers;
using WebApplication3.Repository.Abstract;
using WebApplication3.Repository.Concrete;
using WebApplication3.Services;
using WebApplication3.Services.Abstract;
using WebApplication3.Services.Concrete;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.OutputFormatters.Add(new CarVCardOutputFormatter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(cfg => {
                cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"]!;
            }, typeof(Program).Assembly);

            var connection = builder.Configuration.GetConnectionString("MyConnection");
            builder.Services.AddDbContext<CarContext>(options => options.UseSqlServer(connection));

            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<ICarService, CarService>();

            //builder.Services.AddSingleton<ICalculateService, CalculateService>();
            // builder.Services.AddScoped<ICalculateService, CalculateService>();
            //builder.Services.AddTransient<ICalculateService, CalculateService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
