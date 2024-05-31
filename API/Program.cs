using Core.Models;
using Microsoft.Extensions.Hosting;
using Serverapi.Repositories;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSingleton<Iapplicationrepository, ApplicationRepository>();
            builder.Services.AddSingleton<IadminRepository, AdminRepository>();

      

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });



            var app = builder.Build();



            // Configure the HTTP request pipeline.
            app.UseCors("AllowSpecificOrigin");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("policy");

            app.MapControllers();

            app.Run();
        }
    }
}
